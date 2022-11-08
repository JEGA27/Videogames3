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
   



    private AudioSource audioSource;
    private Rigidbody bulletRigidbody;
    private Health healthSystem;
    private DummyHealth dummyHealth;
    private bool isShotgun;

    float x;
    float y;
    Vector3 direction;
    public float spread;


    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        x = Random.Range(-spread, spread);
        y = Random.Range(-spread, spread);
        direction = bulletRigidbody.transform.forward + new Vector3(x, y, 0);
        bulletRigidbody.velocity = direction * speed;
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BulletTarget>() != null)
        {


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

            
            if(other.gameObject.tag != "Bullet")
            {
                Instantiate(hitEffect, transform.position , Quaternion.identity);
                Destroy(gameObject);
            }
     
        
    }

}
