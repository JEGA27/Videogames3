using System.Collections;
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
        Debug.Log("Room created: " + createInput.text);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, null);
        Debug.Log("Room created: " + createInput.text);
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        int roomCount = PhotonNetwork.CountOfRooms + 1;
        PhotonNetwork.CreateRoom("RandomRoom" + roomCount, roomOptions, null);   
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

        //PhotonNetwork.AutomaticallySyncScene = true;

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

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character"))
        {
            //we already have a team- so switch teams
            PhotonNetwork.LocalPlayer.CustomProperties["Character"] = "none";
        }
        else
        {

            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("Character", "none");
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        }

        // CHECAR
        // if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("MapReady"))
        // {
        //     var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        //     hash.Add("MapReady", false);
        //     PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        // }
        
        if(PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("IdPlayer"))
        {
            PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] = PhotonNetwork.CurrentRoom.PlayerCount;
        }
        
        else
        {
            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("IdPlayer", PhotonNetwork.CurrentRoom.PlayerCount);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("Rounds"))
        {
            PhotonNetwork.CurrentRoom.CustomProperties["Rounds"] = PhotonNetwork.CurrentRoom.PlayerCount;
        }

        else
        {
            var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            hash.Add("Rounds", 2);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("RedTeamRounds"))
        {
            PhotonNetwork.CurrentRoom.CustomProperties["RedTeamRounds"] = PhotonNetwork.CurrentRoom.PlayerCount;
        }

        else
        {
            var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            hash.Add("RedTeamRounds", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }

        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("BlueTeamRounds"))
        {
            PhotonNetwork.CurrentRoom.CustomProperties["BlueTeamRounds"] = PhotonNetwork.CurrentRoom.PlayerCount;
        }

        else
        {
            var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            hash.Add("BlueTeamRounds", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        }

        Debug.Log("Id" + PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"]);

        //if(PhotonNetwork.PlayerList.Length == maxPlayers) PhotonNetwork.LoadLevel("CharacterSelection");

        PhotonNetwork.LoadLevel("CharacterSelection");
        //PhotonNetwork.LoadLevel("City");
    }



    
}

/*
for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
{
    var team = PhotonNetwork.PlayerList[i].CustomProperties[teamPropKey];
}

if (PhotonNetwork.CurrentRoom.PlayerCount == 1) 
*/
