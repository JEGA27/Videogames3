using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

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
    private Transform bulletProjectilePrefab;
    [SerializeField]
    private Transform spawnBulletPosition;


    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    Vector3 mouseWorldPosition;


    //Other code
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    //public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy;

    //Graphics
    //public CamShake camShake;
    //public float camShakeMagnitude, camShakeDuration;


    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        readyToShoot = true;

    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen .height /2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if(Physics.Raycast(ray, out RaycastHit raycastHit, 40f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if(starterAssetsInputs.aim)
        {
            animVirtualCamera.gameObject.SetActive(true);
            thirdPersonController.SetSensitivity(aimSensitivity);
            thirdPersonController.SetRotateOnMove(false);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

        }
        else
        {
            animVirtualCamera.gameObject.SetActive(false);
            thirdPersonController.SetSensitivity(normalSensitivity);
            thirdPersonController.SetRotateOnMove(true);
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));

        }

        MyInput();

    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = starterAssetsInputs.shoot;

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();

        //Shoot
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0){
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    private void Shoot()
    {
      Vector3 aimDir = (mouseWorldPosition - spawnBulletPosition.position).normalized;
      Instantiate(bulletProjectilePrefab, spawnBulletPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
      starterAssetsInputs.shoot = false;
      readyToShoot = false;
      float x = Random.Range(-spread, spread);
      float y = Random.Range(-spread, spread);

      //Calculate Direction with Spread
      //Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

      //camShake.Shake(camShakeDuration, camShakeMagnitude);
      bulletsLeft--;
      bulletsShot--;

      Invoke("ResetShot", timeBetweenShooting);

      if(bulletsShot > 0 && bulletsLeft > 0)
      Invoke("Shoot", timeBetweenShots);

      CineMachineShake.Instance.ShakeCamera(5f, 0.1f);
      AimCinemachineShake.Instance.ShakeCamera(2f, 0.1f);


    }

    private void ResetShot()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
