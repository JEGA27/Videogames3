using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDumpster : MonoBehaviour
{
    public GameObject dumpsterPos;

    private bool canPickUp;
    private GameObject Dumpster; 
    private bool hasDumpster;
    private StarterAssetsInputs starterAssetsInputs;

    // Start is called before the first frame update
    void Start()
    {
        canPickUp = false; 
        hasDumpster = false;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if(starterAssetsInputs.interact) 
        {
            if (!hasDumpster)
            {
                hasDumpster = true;
                Dumpster.GetComponent<Rigidbody>().isKinematic = true; 
                Dumpster.transform.parent = gameObject.transform;
                    
                Dumpster.transform.localPosition = dumpsterPos.transform.localPosition;
                Dumpster.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
            }
            else
            {
                hasDumpster = false;
                Dumpster.GetComponent<Rigidbody>().isKinematic = false;
                Dumpster.transform.parent = null;
            }
            starterAssetsInputs.interact = false;
        }
        GetComponent<ThirdPersonShooterController>().enabled = !hasDumpster;
    }
    void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Dumpster")) 
        {
            canPickUp = true;  
            Dumpster = other.gameObject;
        }
    }
    void OnTriggerExit(Collider other)
    {
        canPickUp = false;
    }
}
