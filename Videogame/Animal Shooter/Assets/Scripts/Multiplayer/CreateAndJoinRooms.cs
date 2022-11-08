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
    private byte maxPlayers = 2;
    private int minPlayers;

    // Start is called before the first frame update

    void Start()
    {
        minPlayers = (int)maxPlayers / 2;
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

        int blueteam = 0;

        Debug.Log(PhotonNetwork.PlayerList.Length);

        
        for (int i = 1; i < PhotonNetwork.PlayerList.Length; i++)
        {
            Debug.Log("Team: " + (int)PhotonNetwork.CurrentRoom.Players[i].CustomProperties["Team"]);

            if (i % 2 < 1) team = 0;
            else team = 1;


            /*if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] < 1)
            {
                blueteam++;
            }*/
            
        }

        /*if (blueteam <= (int)maxPlayers / 2) team = 0;
        else team = 1;*/

        Debug.Log(team);

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            //we already have a team- so switch teams
            PhotonNetwork.LocalPlayer.CustomProperties["Team"] = team;
        }
        else
        {
            //we dont have a team yet- create the custom property and set it
            //0 for blue, 1 for red
            //set the player properties of this client to the team they clicked



            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("Team", team);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);


            /*ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable
            {
                { "Team", team }
            };
            //set the property of Team to the value the user wants
            PhotonNetwork.SetPlayerCustomProperties(playerProps);*/
        }
        //Debug.Log(SetTeam());

        PhotonNetwork.LoadLevel("City");
    }

    int SetTeam()
    {
        int blueteam = 0;


        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++) 
        {
            Debug.Log((int)PhotonNetwork.CurrentRoom.Players[i].CustomProperties["Team"]);
            if ((int)PhotonNetwork.CurrentRoom.Players[i].CustomProperties["Team"] < 1) {
                blueteam++;
            }
        }

        if (blueteam <= (int)maxPlayers / 2) return 0;
        else return 1;
    }
}

/*
for(int i=0; i<PhotonNetwork.PlayerList.Length; i++)
{
    var team = PhotonNetwork.PlayerList[i].CustomProperties[teamPropKey];
}

if (PhotonNetwork.CurrentRoom.PlayerCount == 1) 
*/
