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
    //[SerializeField]
    //private float speed;

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, bulletsLeft;
    public bool allowButtonHold;

    private BulletProjectile bulletProjectile;
   

    // Start is called before the first frame update
    void Start()
    {
        bulletProjectile = bulletProjectilePrefab.GetComponent<BulletProjectile>();
        bulletProjectile.spread = spread;
        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {

       
    }
}
