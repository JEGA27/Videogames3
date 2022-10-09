using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobTrash : MonoBehaviour
{
    public PickUpTrash PickUpTrash;
    public GameManager GameManager;
    public float rateOfRobbing;
    private float robbedTrash;
    private string playerTeam;

    private float timer;
    private float timeForRobbery;

    // Start is called before the first frame update
    void Start()
    {
        playerTeam = this.gameObject.tag;
        robbedTrash = 0.0f;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("BlueTrashBase"))
        {
            timeForRobbery = (float) GameManager.blueTeamTrash / rateOfRobbing;
            if (playerTeam == "RedPlayer")
            {
                if (Input.GetKey("g") || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    timer += Time.deltaTime;
                    if (timer > timeForRobbery) 
                    {
                        PickUpTrash.currentTrash += GameManager.blueTeamTrash;
                        GameManager.blueTeamTrash = 0;
                    }
                     
                }
            }

        }

        if (other.gameObject.CompareTag("RedTrashBase"))
        {
            timeForRobbery = (float) GameManager.redTeamTrash / rateOfRobbing;
            if (playerTeam == "BluePlayer")
            {
                if (Input.GetKey("g") || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    timer += Time.deltaTime;
                    if (timer > timeForRobbery) 
                    {
                        PickUpTrash.currentTrash += GameManager.redTeamTrash;
                        GameManager.redTeamTrash = 0;
                    }
                     
                }
            }

        }
    }
}
