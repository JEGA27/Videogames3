using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ActivateBlackHole : MonoBehaviour
{
    private Transform cam;
    private Transform attackPoint;
    [SerializeField]
    private GameObject objectToThrow;
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float throwUpwardForce;

    private PhotonView PV;
    ScoreSW sSW;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
        {
            cam = GameObject.Find("PlayerFollowCamera").transform;
            attackPoint = GameObject.Find("ThrowingPoint").transform;
            sSW = GetComponent<ScoreSW>();
        }
    }

    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q) && sSW.specialWeaponReady)
            {
                sSW.specialWeaponReady = false;
                PhotonNetwork.CurrentRoom.CustomProperties[sSW.idProgress] = 0;
                sSW.specialWeaponPoints = 0;
                sSW.specialWeaponProgress = 0;
                Throw();
                PlaySounds blackhole = GetComponent<PlaySounds>();
                blackhole.PlaySound(12);
            }
        }
    }

    void Throw()
    {
        // instantiate projectile
        GameObject projectile = PhotonNetwork.Instantiate(objectToThrow.name, attackPoint.position, cam.rotation);
        // Set shooter tag
        projectile.GetComponent<BlackHoleSpawner>().playerTag = gameObject.tag;
        // Set shooter PhotonView
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
