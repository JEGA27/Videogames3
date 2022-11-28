using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{

    [SerializeField]
    private float radius;
    [SerializeField]
    private float force;
    [SerializeField]
    private float up_force;

    private bool hasExploded;

    public string ShooterId;

    
    // Start is called before the first frame update
    void Start()
    {
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, up_force);
            }
            if (nearbyObject.GetComponent<Health>() != null)
            {
                nearbyObject.GetComponent<Health>().TakeDamage(force / 8f + up_force / 2f);
                nearbyObject.GetComponent<Health>().lastShooterId = ShooterId;
                Debug.Log("Hit " + nearbyObject.name);
            }
            else if(nearbyObject.GetComponent<DummyHealth>() != null)
            {
                nearbyObject.GetComponent<DummyHealth>().TakeDamageD(force / 8f + up_force / 2f);
                nearbyObject.GetComponent<DummyHealth>().lastShooterId = ShooterId;
                Debug.Log("Hit " + nearbyObject.name);
            }
        }

        hasExploded = true;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}
