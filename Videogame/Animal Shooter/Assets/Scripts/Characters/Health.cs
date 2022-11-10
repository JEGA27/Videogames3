using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Photon.Pun;

public class Health : MonoBehaviour
{
    public float hp;
    private float maxHp;
    public float hpIncrease;

    public float maxFallVel;
    public float hpFallDamage;
    private float lstFrameYVel;

    public float timeToRecover;
    private float timer;

    PhotonView PV;

    SpawnPlayers sp;

    //private Rigidbody rb;
    private ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        thirdPersonController = gameObject.GetComponent<ThirdPersonController>();
        maxHp = hp;
        GameManager.maxRaccoonHealth = hp;
        timer = 0.0f;

        PV = GetComponent<PhotonView>();

        sp = GameObject.Find("PlayerSpawner").GetComponent<SpawnPlayers>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((-thirdPersonController.GetVerticalVelocity()) > maxFallVel)
        {
            lstFrameYVel = thirdPersonController.GetVerticalVelocity();
        }
        if (thirdPersonController.Grounded  && (-lstFrameYVel) > maxFallVel)
        {
            float fallDamage = (((-lstFrameYVel) - (maxFallVel)) * hpFallDamage);
            TakeDamage(fallDamage);
            lstFrameYVel = 0;
        }

        if (hp < maxHp)
        {
            if (hp > 0)
            {
                Recover();
            }
            else
            {
                Eliminate();
            }
        }

    }

    public void TakeDamage(float damage)
    {
        PV.RPC("TakeDamageRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    public void TakeDamageRPC(float damage)
    {
        if(!PV.IsMine) return;
        
        hp -= damage;
        timer = 0f;
    }

    void Recover()
    {
        timer += Time.deltaTime;
        if (timer > timeToRecover)
        {
            HealthRecover();
        }
    }

    void HealthRecover()
    {
        hp += Time.deltaTime * hpIncrease;
        if (hp >= maxHp)
        {
            hp = maxHp;
            timer = 0;
        }

    }

    void Eliminate()
    {
        Debug.Log("Dead");
        //Destroy(this.gameObject);
        PhotonNetwork.Destroy(this.gameObject);
        sp.Spawn();
    }

}
