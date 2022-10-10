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
        if(canPickUp) 
        {
            if (Input.GetKey("g"))  
            {
                Dumpster.GetComponent<Rigidbody>().isKinematic = true; 
                Dumpster.transform.parent = gameObject.transform;
                
                //Dumpster.transform.localRotation = gameObject.transform.rotation;
                Dumpster.transform.localPosition = dumpsterPos.transform.localPosition;
                Dumpster.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                //Debug.Log(gameObject.transform.forward);
                
                hasDumpster = true;
            }
        }
        if (Input.GetKey("e") && hasDumpster) 
        {
            Debug.Log("Drop");
            Dumpster.GetComponent<Rigidbody>().isKinematic = false;
            Dumpster.transform.parent = null;
            hasDumpster = false;
        }
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
