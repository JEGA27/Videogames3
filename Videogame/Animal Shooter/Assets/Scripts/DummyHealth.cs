using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyHealth : MonoBehaviour
{
    public float hp;
    private float maxHp;
    public float hpIncrease;
    public float timeToRecover;
    public GameObject healthUI;
    public Slider slider;

    private float timer;
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
        Destroy(this.gameObject);
    }

    float CalculateHealth()
    {
        return hp / maxHp;
    }
}
