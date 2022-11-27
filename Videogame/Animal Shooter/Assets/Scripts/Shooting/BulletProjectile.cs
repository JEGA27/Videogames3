using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    public string ShooterId;

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


            if(other.GetComponent<DummyHealth>() != null && other.tag != this.tag)
            {
                dummyHealth = other.GetComponent<DummyHealth>();
                dummyHealth.TakeDamageD(bulletDamage);
                dummyHealth.lastShooterId = ShooterId;
            }
            else if (other.tag != this.tag)
            {
                healthSystem = other.GetComponent<Health>();
                healthSystem.TakeDamage(bulletDamage);
                healthSystem.SetShooterId(ShooterId);
            }

        }

            
        if(other.gameObject.tag != this.tag)
        {
            PhotonNetwork.Instantiate(hitEffect.name, transform.position , Quaternion.identity);
            PhotonNetwork.Destroy(gameObject);
        }
    
        
    }

}
