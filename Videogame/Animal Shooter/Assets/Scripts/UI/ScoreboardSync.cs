using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class ScoreboardSync : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    GameObject localPlayer;
    int localTeam;
    string localPlayerId;
    Color teamColor;
    ScoreSW localScore;
    Health localHealth;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponentInParent<PhotonView>();

        if (PV.IsMine)
        {
            localScore = GetComponentInParent<ScoreSW>();
            localHealth = GetComponentInParent<Health>();
            
            localPlayer = gameObject.transform.Find("LocalPlayer").gameObject;
            localPlayerId = PhotonNetwork.LocalPlayer.UserId;

            localTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

            if (localTeam > 0)
            {
                teamColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);
            }
            else
            {
                teamColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);
            }

            SetValues();

            // CreateOtherValues();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }

    void SetValues()
    {
        // Debug.Log("Setting values for " + userId);
        // Debug.Log("Character: " + character);
        // Debug.Log("Text color: " + textColor);
        // Debug.Log("Line: " + line.name);

        // Debug.Log("SCORE: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Score"]);
        // Debug.Log("TRASH: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Trash"]);
        // Debug.Log("KILLS: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Kills"]);
        // Debug.Log("DEATHS: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Deaths"]);


        localPlayer.SetActive(true);
        localPlayer.transform.Find("Player").GetComponent<Text>().text = PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString();
        localPlayer.transform.Find("Score").GetComponent<Text>().text = localScore.globalPoints.ToString(); //PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Score"].ToString();
        localPlayer.transform.Find("Trash").GetComponent<Text>().text = localScore.globalTrash.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Trash"].ToString();
        localPlayer.transform.Find("Eliminations").GetComponent<Text>().text = localScore.eliminations.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Kills"].ToString();
        localPlayer.transform.Find("Deaths").GetComponent<Text>().text = localHealth.deaths.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Deaths"].ToString();
        Text[] texts = localPlayer.transform.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            text.color = teamColor;
        }
    }

    // void CreateOtherValues()
    // {
    //     for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
    //     {
    //         if (!Ids.ContainsValue(PhotonNetwork.PlayerList[i].UserId))
    //         {

    //             if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] == localTeam && PhotonNetwork.PlayerList[i].UserId != PhotonNetwork.LocalPlayer.UserId)
    //             {
    //                 SetValues(allies[1], PhotonNetwork.PlayerList[i].UserId, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString(), teamColor);
    //             }
    //             else if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] != localTeam)
    //             {
    //                 int index = Ids.ContainsKey(enemies[0]) ? 1 : 0;
    //                 SetValues(enemies[index], PhotonNetwork.PlayerList[i].UserId, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString(), enemyColor);
    //             }
    //         }
    //     }
    // }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log(PhotonNetwork.CurrentRoom.ToStringFull());
        if (PV.IsMine)
        {
            // CreateOtherValues();
            UpdateValues();
        }
    }

    void UpdateValues()
    {
        // List<GameObject> keyList = new List<GameObject>(Ids.Keys);

        // Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        // foreach (var key in keyList)
        // {
        //     Debug.Log("Updating " + key.name + "   " + Ids[key]);
        // } 
        // Debug.Log("-----");

        // // foreach (GameObject key in keyList)
        // // {
        // //     string userId = Ids[key];
        // //     key.transform.Find("Score").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Score"].ToString();
        // //     key.transform.Find("Trash").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Trash"].ToString();
        // //     key.transform.Find("Eliminations").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Kills"].ToString();
        // //     key.transform.Find("Deaths").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Deaths"].ToString();
        // // }

        // for (int i = 0; i < Ids.Count; i++)
        // {
        //     GameObject line = keyList[i];
        //     string userId = Ids[line];

        //     // Debug.Log("Updating values for " + userId);
        //     // Debug.Log("SCORE: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Score"]);
        //     // Debug.Log("TRASH: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Trash"]);
        //     // Debug.Log("KILLS: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Kills"]);
        //     // Debug.Log("DEATHS: " + (int)PhotonNetwork.CurrentRoom.CustomProperties[userId + "Deaths"]);
        
        //     line.transform.Find("Score").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Score"].ToString();
        //     line.transform.Find("Trash").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Trash"].ToString();
        //     line.transform.Find("Eliminations").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Kills"].ToString();
        //     line.transform.Find("Deaths").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Deaths"].ToString();
        // }



        localPlayer.transform.Find("Score").GetComponent<Text>().text = localScore.globalPoints.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Score"].ToString();
        localPlayer.transform.Find("Trash").GetComponent<Text>().text = localScore.globalTrash.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Trash"].ToString();
        localPlayer.transform.Find("Eliminations").GetComponent<Text>().text = localScore.eliminations.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Kills"].ToString();
        localPlayer.transform.Find("Deaths").GetComponent<Text>().text = localHealth.deaths.ToString();//PhotonNetwork.CurrentRoom.CustomProperties[localPlayerId + "Deaths"].ToString();
    }
}
