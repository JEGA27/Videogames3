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
    // Start is called before the first frame update
    void Start()
    {
        playerTeam = this.gameObject.tag;
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
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
                    PickUpTrash.currentTrash = 0;
                    starterAssetsInputs.interact = false;
                }
                
            }

        }
    }
}
