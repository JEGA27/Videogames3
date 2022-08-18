using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxHp = hp;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if ((-rb.velocity.y) > maxFallVel)
        {
            lstFrameYVel = rb.velocity.y;
        }

        if (rb.velocity.y == 0 && (-lstFrameYVel) > maxFallVel)
        {
            hp -= (((-lstFrameYVel) - (maxFallVel)) * hpFallDamage);
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

    void Recover()
    {
        timer += Time.deltaTime;
        if (timer > timeToRecover) 
        {
            timer = 0f;
            HealthRecover();
        }
    }

    void HealthRecover()
    {
        while (hp < maxHp)
        {
            hp += hpIncrease;
            if (hp > maxHp)
            {
                hp = maxHp;
            }
        }
        
    }

    void Eliminate()
    {
        Debug.Log("Dead");
        Destroy(this.gameObject);
    }

}
