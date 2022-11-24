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
    private byte maxPlayers = 4;

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
        roomOptions.PublishUserId = true;
        PhotonNetwork.CreateRoom(createInput.text, roomOptions, null);
        Debug.Log("Room created: " + createInput.text);
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.PublishUserId = true;
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
        

        // Scores and progress (globalscore, trash, elim, deaths, progress)
        string idPlayerScore = PhotonNetwork.LocalPlayer.UserId + "Score";
        string idPlayerTrash = PhotonNetwork.LocalPlayer.UserId + "Trash";
        string idPlayerElim = PhotonNetwork.LocalPlayer.UserId + "Kills";
        string idPlayerDeaths = PhotonNetwork.LocalPlayer.UserId + "Deaths";
        string idPlayerSWProgress = PhotonNetwork.LocalPlayer.UserId + "SWProgress";
        var hashScores = PhotonNetwork.CurrentRoom.CustomProperties;

        if(hashScores.ContainsKey("idPlayerSWProgress"))
        {
            hashScores[idPlayerSWProgress] = 0;
        }
        else
        {
            hashScores.Add(idPlayerSWProgress, 0);
        }
        if(hashScores.ContainsKey("idPlayerScore"))
        {
            hashScores[idPlayerScore] = 0;
        }
        else
        {
            hashScores.Add(idPlayerScore, 0);
        }
        if(hashScores.ContainsKey("idPlayerTrash"))
        {
            hashScores[idPlayerTrash] = 0;
        }
        else
        {
            hashScores.Add(idPlayerTrash, 0);
        }
        if(hashScores.ContainsKey("idPlayerElim"))
        {
            hashScores[idPlayerElim] = 0;
        }
        else
        {
            hashScores.Add(idPlayerElim, 0);
        }
        if(hashScores.ContainsKey("idPlayerDeaths"))
        {
            hashScores[idPlayerDeaths] = 0;
        }
        else
        {
            hashScores.Add(idPlayerDeaths, 0);
        }
        PhotonNetwork.CurrentRoom.SetCustomProperties(hashScores);


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
