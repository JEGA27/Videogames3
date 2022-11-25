using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickUpTrash : MonoBehaviour
{
    public int currentTrash = 0;

    // Script to handle the picking up of trash
    ScoreSW scoreSW;

    // Start is called before the first frame update
    void Start()
    {
        scoreSW = GetComponent<ScoreSW>();    
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
            scoreSW.trashPicked++;
            PhotonNetwork.Destroy(other.gameObject);
            PlaySounds collect = GetComponent<PlaySounds>();
            collect.PlaySound(0);
        }
    }
}
