using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpDumpster : MonoBehaviour
{
    public GameObject dumpsterPos;

    private bool canPickUp;
    private GameObject Dumpster; 
    private bool hasDumpster; 

    // Start is called before the first frame update
    void Start()
    {
        canPickUp = false; 
        hasDumpster = false;
    }
 
    // Update is called once per frame
    void Update()
    {
        if(canPickUp && Input.GetKeyDown("g") || Input.GetKeyDown(KeyCode.JoystickButton1)) 
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
