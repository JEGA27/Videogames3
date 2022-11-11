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


    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            CustomeValue.Add("StartTime", startTime);
            PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);
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

        
    }

    public int CurrentTime() {

        return (int)decTimer;
    }

}
