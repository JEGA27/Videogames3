using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            Debug.Log("Master");
            CustomeValue = new ExitGames.Client.Photon.Hashtable();
            startTime = PhotonNetwork.Time;
            startTimer = true;
            //CustomeValue.Add("StartTime", startTime);
            //PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

            if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("StartTime"))
            {

                var hash = PhotonNetwork.CurrentRoom.CustomProperties;
                startTime = PhotonNetwork.Time;
                hash["StartTime"] = startTime;
                PhotonNetwork.CurrentRoom.SetCustomProperties(hash);

                
            }
            else
            {

                CustomeValue.Add("StartTime", startTime);
                PhotonNetwork.CurrentRoom.SetCustomProperties(CustomeValue);

            }
        }
        else
        {
            //startTime = double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());
            Debug.Log("NotMaster");
            startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];
            startTimer = true;
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        //startTime = (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];

        if (!startTimer) return;
        timerIncrementValue = PhotonNetwork.Time - (double)PhotonNetwork.CurrentRoom.CustomProperties["StartTime"];

        

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
