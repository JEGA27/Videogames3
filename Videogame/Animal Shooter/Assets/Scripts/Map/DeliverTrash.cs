using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverTrash : MonoBehaviour
{
    public PickUpTrash PickUpTrash;
    public GameManager GameManager;

    private string playerTeam;
    // Start is called before the first frame update
    void Start()
    {
        playerTeam = this.gameObject.tag;
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
                if (Input.GetKeyDown("g") || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    GameManager.blueTeamTrash += PickUpTrash.currentTrash;
                    PickUpTrash.currentTrash = 0;   
                }
            }

        }
        if (other.gameObject.CompareTag("RedTrashBase"))
        {

            if (playerTeam == "RedPlayer")
            {
                if (Input.GetKeyDown("g") || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    GameManager.redTeamTrash += PickUpTrash.currentTrash;
                    PickUpTrash.currentTrash = 0;   
                }
            }

        }
    }
}
