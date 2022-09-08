using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    float y;
    float x;
    float z;
    // Start is called before the first frame update
    void Start()
    {
        x = -90;
        y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        z += 10 * Time.deltaTime;
        gameObject.transform.localRotation = Quaternion.Euler(x, y, z);
    }


}
