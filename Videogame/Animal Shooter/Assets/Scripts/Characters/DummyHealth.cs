using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DummyHealth : MonoBehaviour
{
    public float hp;
    private float maxHp;
    public int limitTrash;  //Variable for the trash limit
    public float hpIncrease;
    public float timeToRecover;
    //public GameObject healthUI;
    //public Slider slider;
    private float timer;
    //Vehicle Variables
    public int objectsToSpawn;
    public List<GameObject> objectsPrefabs = new List<GameObject>();
    public bool ActiveTrashDrop;

    public string lastShooterId;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {   
        PV = GetComponent<PhotonView>();
        maxHp = hp;
        ActiveTrashDrop = true;
        //slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
      //slider.value = CalculateHealth();
      //GameObject[] trashLimit;
      //trashLimit = GameObject.FindGameObjectsWithTag("Trash");

      //if(trashLimit.Length <= limitTrash) {
        //trashLimit = null;
      //}


      if (hp < maxHp)
      {
          //healthUI.SetActive(true);
          if(this.gameObject.GetComponent<Animator>() != null)
          {
            this.gameObject.GetComponent<Animator>().SetTrigger("setStagger");
          }
          if (hp > 0)
          {
              Recover();
          }
          else
          {
              if(ActiveTrashDrop) {
                for(int i = 0; i < objectsToSpawn; i++) {
                    PhotonNetwork.Instantiate(objectsPrefabs[Random.Range(0, objectsPrefabs.Count)].name, transform.position, Random.rotation);
                }
              }
              Eliminate();
          }
      }
    }

    public void TakeDamageD(float damage)
    {
        PV.RPC("TakeDamageDRPC", RpcTarget.All, damage);
    }

    [PunRPC]
    public void TakeDamageDRPC(float damage)
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
        PhotonNetwork.Destroy(this.gameObject);
        // Update shooter's kills
        // PhotonNetwork.CurrentRoom.CustomProperties[lastShooterId + "Kills"] = (int)PhotonNetwork.CurrentRoom.CustomProperties[lastShooterId + "Kills"] + 1;
    }

    float CalculateHealth()
    {
        return hp / maxHp;
    }
}
