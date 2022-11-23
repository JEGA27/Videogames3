using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlackHoleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject blackHole;
    public string playerTag;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        var bH = PhotonNetwork.Instantiate(blackHole.name, spawnPosition, transform.rotation);
        bH.GetComponent<BlackHole>().playerTag = playerTag;
        Destroy(gameObject);
    }
}
