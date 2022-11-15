using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class DummyHealth : MonoBehaviour
{
    public float hp;
    private float maxHp;
    public float hpIncrease;
    public float timeToRecover;
    public GameObject healthUI;
    public Slider slider;
    private float timer;
    //Vehicle Variables
    public int objectsToSpawn;
    public List<GameObject> objectsPrefabs = new List<GameObject>();
    public bool ActiveTrashDrop = false;

    // Start is called before the first frame update
    void Start()
    {
        maxHp = hp;
        slider.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
      slider.value = CalculateHealth();


      if (hp < maxHp)
      {
          healthUI.SetActive(true);
          if (hp > 0)
          {
              Recover();
          }
          else
          {
              Eliminate();
          }

          if (hp == 0 && ActiveTrashDrop)
          {
              for (int i = 0; i < objectsToSpawn; i++)
              {
                PhotonNetwork.Instantiate(objectsPrefabs[Random.Range(0, objectsPrefabs.Count)].name, transform.position, Random.rotation);
              }
              PhotonNetwork.Destroy(this.gameObject);
          }
      }
    }

    public void TakeDamage(float damage)
    {
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
        PhotonNetwork.Destroy(this.gameObject);
    }

    float CalculateHealth()
    {
        return hp / maxHp;
    }
}
