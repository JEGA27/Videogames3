using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{

    [SerializeField]
    private float radius;
    [SerializeField]
    private float force;

    private bool hasExploded;
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
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        hasExploded = true;
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}
