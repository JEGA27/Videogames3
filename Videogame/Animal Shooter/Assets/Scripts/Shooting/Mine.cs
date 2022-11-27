using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float force;
    [SerializeField]
    private float up_force;

    private bool hasExploded;

    private bool hitGround;
    // Start is called before the first frame update
    void Start()
    {
        hasExploded = false;
        hitGround = false;
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
                nearbyObject.GetComponent<Health>().TakeDamage(force / 10f + up_force / 2f);
            }
        }

        Destroy(this.gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null && hitGround)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision other) 
    {
        hitGround = true;
    }
}
