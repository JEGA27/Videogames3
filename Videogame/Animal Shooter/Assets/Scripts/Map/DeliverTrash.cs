using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class DeliverTrash : MonoBehaviour
{
    public PickUpTrash PickUpTrash;
    public GameManager GameManager;
    private StarterAssetsInputs starterAssetsInputs;

    private int playerTeam;

    // Script to handle the delivery of trash to the base
    ScoreSW scoreSW;

    // Start is called before the first frame update
    void Start()
    {
        playerTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        scoreSW = GetComponent<ScoreSW>();

        var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        hash.Add("BlueScore", 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);

        hash.Add("RedScore", 0);
        PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("BlueTrashBase") && playerTeam == 0)
        {
            if (starterAssetsInputs.interact)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] + PickUpTrash.currentTrash;
                // Update trash delivered
                scoreSW.trashDelivered += PickUpTrash.currentTrash; 
                PickUpTrash.currentTrash = 0;
                starterAssetsInputs.interact = false;
            }
        }
        if (other.gameObject.CompareTag("RedTrashBase") && playerTeam == 1)
        {
            if (starterAssetsInputs.interact)
            {
                PhotonNetwork.CurrentRoom.CustomProperties["RedScore"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"] + PickUpTrash.currentTrash;
                // Update trash delivered
                scoreSW.trashDelivered += PickUpTrash.currentTrash;
                PickUpTrash.currentTrash = 0;
                starterAssetsInputs.interact = false;
            }
        }
    }
}
