using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyParticle : MonoBehaviour
{

    [SerializeField]
    private float secondsToDestroy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, secondsToDestroy);
    }
}
