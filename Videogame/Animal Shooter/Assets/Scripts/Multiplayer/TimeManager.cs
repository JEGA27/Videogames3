using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class TimeManager : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;

    ExitGames.Client.Photon.Hashtable CustomeValue;

    public double decTimer;

    public double roundTime = 180.0;

    int time;

    public string nextScene;

    bool scenechanged = false;

    public SetMap MapManager;

    public bool menu;

    // Start is called before the first frame update
    void Start()
    {
        time = (int)roundTime;

        /*if (!menu) {

            byte id = 1;
            if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] == id)  //(GameObject.FindGameObjectWithTag("MapManager") == null) //(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                GameObject.FindWithTag("MapManager").GetComponent<SetMap>();
            }

        } //if (PhotonNetwork.IsMasterClient) MapManager = GameObject.FindWithTag("MapManager").GetComponent<SetMap>();
        //PhotonNetwork.AutomaticallySyncScene = true;

        */

        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            //CustomeValue.Add("StartTime", startTime);
            //PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
            {
                //we already have a team- so switch teams
                //PhotonNetwork.CurrentRoom.CustomProperties["StartTime"] = startTime;

                var hash = PhotonNetwork.CurrentRoom.CustomProperties;
                hash["StartTime"] = startTime;
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

            }
            else
            {
                var hash = PhotonNetwork.CurrentRoom.CustomProperties;
                hash.Add("StartTime", startTime);
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

            }
        }
        else
        {
            startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            startTimer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!startTimer) return;
        timerIncrementValue = PhotonNetwork.Time - startTime;


        decTimer = roundTime - timerIncrementValue;

        if ((int)decTimer <= 0 && !scenechanged)
        {
            
            Debug.Log((int)decTimer);

            if (menu)
            {
                Debug.Log((int)decTimer);
                PhotonNetwork.LoadLevel(nextScene);

            }
            else {
                

                /*byte id = 1;
                if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] == id)  //(GameObject.FindGameObjectWithTag("MapManager") == null) //(PhotonNetwork.CurrentRoom.PlayerCount == 1)
                {
                    MapManager.Reset();
                }*/

                roundTime += time;

                if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"] >= 2 || (int)PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"] >= 2)
                {

                    //PhotonNetwork.LoadLevel("End");
                    menu = true;

                }
            }
            scenechanged = true;

            /*if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel(nextScene);
            }*/

        }
        

    }

    public int CurrentTime() {

        return (int)decTimer;
    }

}
