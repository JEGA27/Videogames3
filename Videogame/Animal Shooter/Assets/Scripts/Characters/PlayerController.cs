using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    public float speed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       float hori = Input.GetAxisRaw("Horizontal");
       float verti = Input.GetAxisRaw("Vertical");

       if(hori != 0.0f || verti != 0.0f) {
        Vector3 dir = transform.forward * verti + transform.right * hori;

        rigidbody.MovePosition(transform.position + dir * speed * Time.deltaTime);
       } 
    }
}
