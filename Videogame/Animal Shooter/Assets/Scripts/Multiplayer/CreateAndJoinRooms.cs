﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createInput;
 
    public InputField joinInput;
    private byte maxPlayers = 6;

    public static CreateAndJoinRooms instance;

    void Awake()
    {
        instance = this;
    }

    public void JoinRoomName(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
    }

    public void CreateRoomName() {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, null);
        

    }
    public void CreateRoom()
    {

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(null, roomOptions, null);
        
    }

    public void QuickMatch()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnJoinedRoom()
    {
        int team = 0;

        Debug.Log(team);


        if (PhotonNetwork.PlayerList.Length % 2 < 1) team = 1;
        else team = 0;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            //we already have a team- so switch teams
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }
        else
        {

            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("Team", team);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        }


        PhotonNetwork.LoadLevel("City");
    }



    
}

/*
for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
{
    var team = PhotonNetwork.PlayerList[i].CustomProperties[teamPropKey];
}

if (PhotonNetwork.CurrentRoom.PlayerCount == 1) 
*/
