using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ActivateBlackHole : MonoBehaviour
{
    public Transform cam;
    public Transform attackPoint;
    [SerializeField]
    private GameObject objectToThrow;
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float throwUpwardForce;

    private Animator animator;

    private PhotonView PV;
    ScoreSW sSW;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            cam = gameObject.transform.Find("Cameras").transform.Find("PlayerFollowCamera");
            Debug.Log("cam: " + cam);
            attackPoint = gameObject.transform.Find("ThrowingPoint");
            Debug.Log("attackPoint: " + attackPoint);
            sSW = GetComponent<ScoreSW>();
            animator = gameObject.GetComponent<Animator>();
        }
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q) && sSW.specialWeaponReady)
            {
                Debug.Log("Throwing Black Hole");
                sSW.specialWeaponReady = false;
                PhotonNetwork.CurrentRoom.CustomProperties[sSW.idProgress] = 0;
                sSW.specialWeaponPoints = 0;
                sSW.specialWeaponProgress = 0;
                Throw();
                PlaySounds blackhole = GetComponent<PlaySounds>();
                blackhole.PlaySound(12);
            }
            animator.SetBool("Throwing", Input.GetKeyDown(KeyCode.Q));
        }
    }

    void Throw()
    {
        Debug.Log("Throwing Black Hole2");
        // instantiate projectile
        GameObject projectile = PhotonNetwork.Instantiate(objectToThrow.name, attackPoint.position, cam.rotation);
        // Set shooter tag
        projectile.GetComponent<BlackHoleSpawner>().playerTag = gameObject.tag;
        // Set shooter PhotonView
        Debug.Log("PhotonView: " + PV);
        projectile.GetComponent<BlackHoleSpawner>().PV = PV;

        // get projectiles rigidbody 
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate projectiles direction with raycast
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // throwing force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);
    }
}
