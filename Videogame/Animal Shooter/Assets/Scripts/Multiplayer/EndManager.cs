using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{

    public Text end;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cam.clearFlags = CameraClearFlags.SolidColor;

        if ((int)PhotonNetwork.LocalPlayer.CustomProperties["Team"] > 0)
        {

            cam.backgroundColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"])
            {

                end.text = "Victory";

            }
            else
            {
                end.text = "Defeat";
            }

        }
        else
        {

            cam.backgroundColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"])
            {

                end.text = "Victory";

            }
            else
            {
                end.text = "Defeat";
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
