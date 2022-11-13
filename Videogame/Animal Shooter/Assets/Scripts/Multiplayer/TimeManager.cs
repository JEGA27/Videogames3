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

    public string nextScene;

    bool scenechanged = false;


    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            //CustomeValue.Add("StartTime", startTime);
            //PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("StartTime"))
            {
                //we already have a team- so switch teams
                PhotonNetwork.LocalPlayer.CustomProperties["StartTime"] = startTime;
            }
            else
            {

                CustomeValue.Add("StartTime", startTime);
                PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

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
            PhotonNetwork.LoadLevel(nextScene);
            scenechanged = true;

        }
        

    }

    public int CurrentTime() {

        return (int)decTimer;
    }

}
