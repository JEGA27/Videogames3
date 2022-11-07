using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStats : MonoBehaviour
{
    [SerializeField]
    public Transform bulletProjectilePrefab;
    [SerializeField]
    public Transform spawnBulletPosition;
    [SerializeField]
    public ParticleSystem muzzleFlash;
    [SerializeField]
    private float speed;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    private Rigidbody bulletRigidbody;
    Vector3 direction;
    float x;
    float y;

    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = bulletProjectilePrefab.gameObject.GetComponent<Rigidbody>();
        x = Random.Range(-spread, spread);
        y = Random.Range(-spread, spread);
        direction = bulletProjectilePrefab.transform.forward + new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {

        bulletRigidbody.velocity = direction * speed;
    }
}
