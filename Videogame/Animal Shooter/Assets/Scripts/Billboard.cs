using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    Camera mMainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        mMainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = mMainCamera.transform.rotation;
    }
}
