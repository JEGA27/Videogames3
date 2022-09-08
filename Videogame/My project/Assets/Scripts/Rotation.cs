using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    GameObject shape;
    float x;
    float y;
    float z;
    // Start is called before the first frame update
    void Start()
    {

    }

    void rotateShape()
    {
        x += 1000 * Time.deltaTime;
        y += 1000 * Time.deltaTime;
        z += 1000 * Time.deltaTime;
        shape = GameObject.FindGameObjectWithTag("Shape");
        shape.transform.localRotation = Quaternion.Euler(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        rotateShape();
    }
}
