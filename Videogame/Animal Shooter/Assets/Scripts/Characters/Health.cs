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

    public int deaths;
    string idDeaths;
    public string lastShooterId;

    private Animator anim;
    private CharacterController charController;
    private Rigidbody[] ragBones;
    private Collider[] ragColliders;
    public float ragExplosionForce;
    public float ragExplosionRadius;
    public float ragUpForce;

    public float timeToSpawn;

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

        // Store deaths
        idDeaths = PhotonNetwork.LocalPlayer.UserId + "Deaths";
        deaths = (int)PhotonNetwork.CurrentRoom.CustomProperties[idDeaths];
        lastShooterId = "none";

        anim = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
        charController.enabled = true;
        ragBones = GetComponentsInChildren<Rigidbody>();
        ragColliders = GetComponentsInChildren<Collider>();

        ToggleRagdoll(false);
        charController.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        //charController.enabled = charEnabled;

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

    public void SetShooterId(string shooterId)
    {
        PV.RPC("SetShooterIdRPC", RpcTarget.All, shooterId);
    }

    [PunRPC]
    public void TakeDamageRPC(float damage)
    {
        if(!PV.IsMine) return;
        
        hp -= damage;
        timer = 0f;
    }

    [PunRPC]
    public void SetShooterIdRPC(string shooterId)
    {
        if(!PV.IsMine) return;
        
        lastShooterId = shooterId;
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
        ToggleRagdoll(true);
        StartCoroutine(TimeToSpawn());
        // Update deaths
        var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        hash[idDeaths] = deaths + 1;
        // PhotonNetwork.CurrentRoom.CustomProperties[idDeaths] = deaths + 1;

        // Update shooter's kills
        if (lastShooterId != PhotonNetwork.LocalPlayer.UserId && lastShooterId != "none")
        {
            Debug.Log("Contando kill a " + lastShooterId);
            // PhotonNetwork.CurrentRoom.CustomProperties[lastShooterId + "Kills"] = (int)PhotonNetwork.CurrentRoom.CustomProperties[lastShooterId + "Kills"] + 1;
            hash[lastShooterId + "Kills"] = (int)PhotonNetwork.CurrentRoom.CustomProperties[lastShooterId + "Kills"] + 1;
        }

        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

        
        
    }

    void ToggleRagdoll(bool toggle)
    {
        if (!PV.IsMine) return;
        /*
        anim.enabled = !toggle;

        foreach(Rigidbody rb in ragBones)
        {
            rb.isKinematic = !toggle;
        }

        foreach(Collider col in ragColliders)
        {
            col.enabled = toggle;
        }


        if (toggle)
        {
            charController.enabled = false;
            foreach(Rigidbody rb in ragBones)
            {
                rb.AddExplosionForce(ragExplosionForce, transform.position, ragExplosionRadius, ragUpForce, ForceMode.Impulse);
            }
        }
        */

        PV.RPC("ToggleRagdoll_RPC", RpcTarget.All, toggle);
    }

    IEnumerator TimeToSpawn()
    {
        yield return new WaitForSeconds(timeToSpawn);
        PhotonNetwork.Destroy(this.gameObject);
        PlaySounds dead = GetComponent<PlaySounds>();
        dead.PlaySound(6);
        sp.Spawn();
    }

    [PunRPC]
    public void ToggleRagdoll_RPC(bool toggle)
    {
        anim.enabled = !toggle;

        foreach(Rigidbody rb in ragBones)
        {
            rb.isKinematic = !toggle;
        }

        foreach(Collider col in ragColliders)
        {
            col.enabled = toggle;
        }


        if (toggle)
        {
            charController.enabled = false;
            foreach(Rigidbody rb in ragBones)
            {
                rb.AddExplosionForce(ragExplosionForce, transform.position, ragExplosionRadius, ragUpForce, ForceMode.Impulse);
            }
        }
    }

}
