using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickUpTrash : MonoBehaviour
{
    public int currentTrash = 0;

    // Script to handle the picking up of trash
    ScoreSW scoreSW;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        if (gameObject.tag != "Dummy") scoreSW = GetComponent<ScoreSW>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Trash"))
        {
            currentTrash++;
            // Update trash collected
            if (gameObject.tag != "Dummy") scoreSW.trashPicked++;
            other.gameObject.GetComponent<Trash>().EraseTrash();
            // PhotonNetwork.Destroy(other.gameObject);
            PlaySounds collect = GetComponent<PlaySounds>();
            collect.PlaySound(0);
        }
    }
}
