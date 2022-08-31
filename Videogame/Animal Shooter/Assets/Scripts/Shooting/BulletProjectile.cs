using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField]
    private Transform vfxHitGreen;
    [SerializeField]
    private Transform vfxHitRed;
    [SerializeField]
    private float speed;

    private Rigidbody bulletRigidbody;
    private Health healthSystem;
    private float bulletDamage;

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
            Instantiate(vfxHitGreen, transform.position , Quaternion.identity);
            healthSystem = other.GetComponent<Health>();
            healthSystem.TakeDamage(bulletDamage);
        }
        else
        {
            Instantiate(vfxHitRed, transform.position , Quaternion.identity);
        }
        Destroy(gameObject);
    }

    public void SetBulletDamage(float damage)
    {
        bulletDamage = damage;
    }
}
