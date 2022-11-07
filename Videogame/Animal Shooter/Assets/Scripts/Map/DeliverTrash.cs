using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverTrash : MonoBehaviour
{
    public PickUpTrash PickUpTrash;
    public GameManager GameManager;
    private StarterAssetsInputs starterAssetsInputs;

    private string playerTeam;

    // Script to handle the delivery of trash to the base
    ScoreSW scoreSW;

    // Start is called before the first frame update
    void Start()
    {
        playerTeam = this.gameObject.tag;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        scoreSW = GetComponent<ScoreSW>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("BlueTrashBase"))
        {

            if (playerTeam == "BluePlayer")
            {
                if (starterAssetsInputs.interact)
                {
                    GameManager.blueTeamTrash += PickUpTrash.currentTrash;
                    // Update trash delivered
                    scoreSW.trashDelivered += PickUpTrash.currentTrash; 
                    PickUpTrash.currentTrash = 0;
                    starterAssetsInputs.interact = false;
                }
                
            }

        }
        if (other.gameObject.CompareTag("RedTrashBase"))
        {

            if (playerTeam == "RedPlayer")
            {
                if (starterAssetsInputs.interact)
                {
                    GameManager.redTeamTrash += PickUpTrash.currentTrash;
                    // Update trash delivered
                    scoreSW.trashDelivered += PickUpTrash.currentTrash;
                    PickUpTrash.currentTrash = 0;
                    starterAssetsInputs.interact = false;
                }
                
            }

        }
    }
}
