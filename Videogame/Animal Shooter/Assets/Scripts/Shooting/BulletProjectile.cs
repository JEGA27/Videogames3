using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{

    [SerializeField]
    private Transform vfxHitGreen;
    [SerializeField]
    private Transform vfxHitRed;

    private Rigidbody bulletRigidbody;

    void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        float speed = 40f;
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
        }
        else
        {
            Instantiate(vfxHitRed, transform.position , Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
