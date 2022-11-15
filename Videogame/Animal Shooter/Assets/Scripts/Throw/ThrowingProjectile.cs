using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingProjectile : MonoBehaviour
{
    [SerializeField]
    private Transform cam;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject objectToThrow;

    [SerializeField]
    private int totalThrows;
    [SerializeField]
    private float throwCooldown;

    
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private float throwUpwardForce;


    private KeyCode throwKey = KeyCode.Q;
    private bool readyToThrow;
    private int updatedThrows;

    private void Start()
    {
        readyToThrow = true;
        updatedThrows = totalThrows;
    }

    private void Update()
    {
        
        if(Input.GetKeyDown(throwKey) && readyToThrow && updatedThrows > 0)
        {
            Throw();
        }

    }

    private void Throw()
    {
        readyToThrow = false;

        // instantiate projectile
        GameObject projectile = Instantiate(objectToThrow, attackPoint.position, cam.rotation);

        // get projectiles rigidbody 
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        // calculate projectiles direction with raycast
        Vector3 forceDirection = cam.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(cam.position, cam.forward, out hit, 500f))
        {
            forceDirection = (hit.point - attackPoint.position).normalized;
        }

        // throwing force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        // reduce throws left
        updatedThrows--;

        // throwing Cooldown
        Invoke(nameof(ResetThrow), throwCooldown);
    }

    // Resets throwing cooldown 
    private void ResetThrow()
    {
        updatedThrows = totalThrows;
        readyToThrow = true;
        Debug.Log("Charged");
    }
}
