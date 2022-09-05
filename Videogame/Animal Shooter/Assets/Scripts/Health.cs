using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

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


    //private Rigidbody rb;
    private ThirdPersonController thirdPersonController;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        thirdPersonController = gameObject.GetComponent<ThirdPersonController>();
        maxHp = hp;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(hp);
        Debug.Log(thirdPersonController.GetVerticalVelocity());
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

}
