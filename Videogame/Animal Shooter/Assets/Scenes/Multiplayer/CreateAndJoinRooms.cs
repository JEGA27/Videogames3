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

    // Start is called before the first frame update

    public void CreateRoom() {

        PhotonNetwork.CreateRoom(createInput.text);
    }

    public void JoinRoom()
    {

        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        Player local = PhotonNetwork.LocalPlayer;

        //int number = PhotonNetwork.PlayerList.Count;
        //PhotonNetwork.LocalPlayer.NickName = name.text;

        PhotonNetwork.LoadLevel("City");
    }
}
