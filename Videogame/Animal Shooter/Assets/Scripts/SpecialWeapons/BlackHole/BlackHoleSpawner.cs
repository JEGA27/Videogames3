using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject blackHole;

    
    
    
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
        Instantiate(blackHole, spawnPosition, transform.rotation);
        Destroy(gameObject);
    }
}
