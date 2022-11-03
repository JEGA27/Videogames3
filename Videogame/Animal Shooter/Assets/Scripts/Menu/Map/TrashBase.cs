using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBase : MonoBehaviour
{
    public GameManager GameManager;
    public static int teamTrash;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (this.gameObject.tag == "BlueTrashBase")
        {
            teamTrash = GameManager.blueTeamTrash;
        }

        if (this.gameObject.tag == "RedTrashBase")
        {
            teamTrash = GameManager.redTeamTrash;
        }
        */
    }
}
