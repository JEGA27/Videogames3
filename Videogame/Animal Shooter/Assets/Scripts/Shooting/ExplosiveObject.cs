using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ExplosiveObject : MonoBehaviour
{   
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject objectToThrow;
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float throwUpwardForce;
    [SerializeField]
    private int totalThrows;
    [SerializeField]
    private float throwCooldown;

    private int originalThrows;

    //private GameObject projectile;
    private PhotonView PV;
    private bool readyToThrow;
    GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        
        readyToThrow = true;
        originalThrows = totalThrows;
    }

    void Update()
    {
       
    }

    public void Throw()
    {
        if(readyToThrow && totalThrows > 0)
        {
            // instantiate projectile
            projectile = PhotonNetwork.Instantiate(objectToThrow.name, attackPoint.transform.position, cam.rotation);
            //projectile = GameObject.Instantiate(objectToThrow, attackPoint.transform.position, cam.rotation);

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

            totalThrows--;
            Invoke(nameof(ResetThrow), throwCooldown);
        }
    }
    private void ResetThrow()
    {
        readyToThrow = true;
        totalThrows = originalThrows;
    }
}
