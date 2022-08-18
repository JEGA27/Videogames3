using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float hp;

    public float maxFallVel;
    public float hpFallDamage;
    private float lstFrameYVel;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            hp -= FallDamage(lstFrameYVel, maxFallVel, hpFallDamage);
            lstFrameYVel = 0;
        }
    }

    public float FallDamage(float yVel, float maxVel, float hpLoss)
    {
        return (((-yVel) - (maxVel)) * hpLoss);
    }
}
