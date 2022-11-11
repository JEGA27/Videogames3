using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class ListItem : MonoBehaviour
{   
    [SerializeField] TMP_Text text;

    RoomInfo info;


    public void SetUp(RoomInfo _info)
    {
        info = _info;
        text.text = _info.Name;
    }

    public void OnClick()
    {
        CreateAndJoinRooms.instance.JoinRoomName(info);
    }
}
