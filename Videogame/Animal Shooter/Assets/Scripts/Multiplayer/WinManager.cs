using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinManager : MonoBehaviour
{
    public string nextScene;
    bool scenechanged = false;
    TimeManager timeManager;

    // Start is called before the first frame update
    void Start()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if ((int)timeManager.CurrentTime() <= 0 && !scenechanged)
        {
            Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"]);
            Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"]);

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"])
            {

                var hash = PhotonNetwork.CurrentRoom.CustomProperties;
                hash["BlueScore"] = 0;
                hash["RedScore"] = 0;
                hash["BlueTeamRounds"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"] + 1;

                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
                scenechanged = true;


            }
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"])
            {

                var hash = PhotonNetwork.CurrentRoom.CustomProperties;
                hash["BlueScore"] = 0;
                hash["RedScore"] = 0;
                hash["RedTeamRounds"] = (int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"] + 1;

                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
                scenechanged = true;


            }
            else {

                /*Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);
                Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"]);
                Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"]);
                Debug.Log((int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"]);*/
                Debug.Log("Empate");

                //Restart();

            }

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"] >= 2 || (int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"] >= 2) {

                PhotonNetwork.LoadLevel("End");
                scenechanged = true;

            }


            //Restart();



        }

        

    }

    void Restart() {

        PhotonNetwork.LoadLevel(nextScene);
        scenechanged = true;

    }
}
