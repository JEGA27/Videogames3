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
    //[SerializeField]
    //private AudioClip audioClip;


    //private AudioSource audioSource;
    //private Rigidbody bulletRigidbody;
    private Health healthSystem;
    private DummyHealth dummyHealth;
   


    void Awake()
    {
        //bulletRigidbody = GetComponent<Rigidbody>();
        //audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //bulletRigidbody.velocity = transform.forward * speed;
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


            if(other.GetComponent<DummyHealth>() != null)
            {
                dummyHealth = other.GetComponent<DummyHealth>();
                dummyHealth.TakeDamage(bulletDamage);
            }
            else
            {
                healthSystem = other.GetComponent<Health>();
                healthSystem.TakeDamage(bulletDamage);
            }

        }

        Instantiate(hitEffect, transform.position , Quaternion.identity);
        //audioSource.Play();
        /*if(other.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }*/
        

    }

}
