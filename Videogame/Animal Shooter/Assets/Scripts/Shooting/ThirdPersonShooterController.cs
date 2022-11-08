using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using Photon.Pun;
//using UnityEditor.Experimental.GraphView;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera animVirtualCamera;
    [SerializeField]
    private float normalSensitivity;
    [SerializeField]
    private float aimSensitivity;
    [SerializeField]
    private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField]
    private GameObject weapon1;
    [SerializeField]
    private GameObject weapon2;
    [SerializeField]
    private float swapWeaponTime;


    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    Vector3 mouseWorldPosition;
    private WeaponStats weaponStats;
    private ParticleSystem muzzleFlash;
    private float weapon = 1f;
    private bool swap = true;

    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        if(!PV.IsMine){
            animVirtualCamera.gameObject.SetActive(false);
            gameObject.transform.Find("HUD").gameObject.SetActive(false);
            gameObject.transform.Find("Cameras").gameObject.SetActive(false);
        }
            
    }

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        weaponStats = weapon1.GetComponent<WeaponStats>();
        bulletsLeft = weaponStats.magazineSize;
        readyToShoot = true;
        weapon1.SetActive(true);
        weapon2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (starterAssetsInputs.swapWeapon) {
            starterAssetsInputs.swapWeapon = false;
            weapon = -weapon;
            swap = true;

        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            weapon = Input.GetAxis("Mouse ScrollWheel");
            swap = true;
        }

        StartCoroutine(SwapWeapon());

        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen .height /2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 40f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        Vector3 worldLookTarget = mouseWorldPosition;
        worldLookTarget.y = transform.position.y;
        Vector3 lookDirection = (worldLookTarget - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, lookDirection, Time.deltaTime * 20f);
        thirdPersonController.SetRotateOnMove(false);

        if(starterAssetsInputs.aim && PV.IsMine)
        {
            animVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

        }
        else
        {
            animVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

        }

        MyInput();

    }

    private void MyInput()
    {
        if (weaponStats.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = starterAssetsInputs.shoot;

        if (starterAssetsInputs.reload && bulletsLeft < weaponStats.magazineSize && !reloading) {
            Reload();
            starterAssetsInputs.reload = false;
        }
        

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = weaponStats.bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
      Vector3 aimDir = (mouseWorldPosition - weaponStats.spawnBulletPosition.position).normalized;
      float x = Random.Range(-weaponStats.spread, weaponStats.spread);
      float y = Random.Range(-weaponStats.spread, weaponStats.spread);
      Instantiate(weaponStats.bulletProjectilePrefab, weaponStats.spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
      starterAssetsInputs.shoot = false;
      readyToShoot = false;
      

      //Calculate Direction with Spread
      //Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

      //camShake.Shake(camShakeDuration, camShakeMagnitude);
      bulletsLeft--;
      bulletsShot--;

      Invoke("ResetShot", weaponStats.timeBetweenShooting);

      if(bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", weaponStats.timeBetweenShots);

      CineMachineShake.Instance.ShakeCamera(5f, 0.1f);
      AimCinemachineShake.Instance.ShakeCamera(2f, 0.1f);

      weaponStats.muzzleFlash.Emit(1);


    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", weaponStats.reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = weaponStats.magazineSize;
        reloading = false;
    }

    private IEnumerator SwapWeapon()
    {
      //Swap weapon
      //if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.JoystickButton3) || weapon > 0)

      if (swap && weapon > 0)
      {
          swap = false;
          readyToShoot = false;
          weapon2.SetActive(false);
          yield return new WaitForSeconds(swapWeaponTime);
          readyToShoot = true;
          weapon1.SetActive(true);
          weaponStats = weapon1.GetComponent<WeaponStats>();
      }
      //else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.JoystickButton1) || weapon < 0)
      else if(swap && weapon < 0)
      {
          swap = false;
          readyToShoot = false;
          weapon1.SetActive(false);
          yield return new WaitForSeconds(swapWeaponTime);
          readyToShoot = true;
          weapon2.SetActive(true);
          weaponStats = weapon2.GetComponent<WeaponStats>();
      }
    }
}
