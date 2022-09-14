using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField]
    private ParticleSystem hitEffect;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float bulletDamage;

    private Rigidbody bulletRigidbody;
    private Health healthSystem;


    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulletTarget>() != null)
        {
            //Instantiate(vfxHitGreen, transform.position , Quaternion.identity);
            //vfxHitGreen.transform.position = transform.position;
            //vfxHitGreen.Emit(1);
            //Destroy(hitEffect);
            healthSystem = other.GetComponent<Health>();
            healthSystem.TakeDamage(bulletDamage);

        }
        else
        {
            //Instantiate(vfxHitRed, transform.position , Quaternion.identity);
            //vfxHitGreen.transform.position = transform.position;
            //vfxHitRed.Emit(1);
          //  Destroy(vfxHitGreen);
            Debug.Log("No target");
        }
        Instantiate(hitEffect, transform.position , Quaternion.identity);
        Destroy(gameObject);
    }

}
