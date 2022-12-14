using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class Timer : MonoBehaviour
{
    bool startTimer = false;
    double timerIncrementValue;
    double startTime;

    public Text timerTxt;
    public double decTimer;

    public double roundTime = 180.0;

    TimeManager timeManager;

    ExitGames.Client.Photon.Hashtable CustomeValue;

    void Start()
    {
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();

        /*
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
        }*/
    }

    void Update()
    {
        /*
        if (!startTimer) return;
        timerIncrementValue = PhotonNetwork.Time - startTime;
        */


        decTimer = timeManager.CurrentTime();

        //timerTxt.text = ((int)decTimer).ToString();

        

        if ((int)decTimer >= 0)
        {
            TimeSpan ts = TimeSpan.FromSeconds((int)decTimer);
            timerTxt.text = string.Format("{0}", new DateTime(ts.Ticks).ToString("mm:ss"));
        }
        else
        {
            Debug.Log("Game Over!");
        }

       
    }


}


