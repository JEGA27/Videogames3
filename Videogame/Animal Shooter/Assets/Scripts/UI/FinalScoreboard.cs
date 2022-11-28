using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class FinalScoreboard : MonoBehaviour
{
    List<GameObject> Allies;
    List<GameObject> Enemies;

    string localPlayerId;
    int localTeam;
    Color localColor;
    Color enemyColor;

    // Start is called before the first frame update
    void Start()
    {
        Allies = new List<GameObject>();
        Enemies = new List<GameObject>();

        Allies.Add(gameObject.transform.Find("BlueP1").gameObject);
        Allies.Add(gameObject.transform.Find("BlueP2").gameObject);

        Enemies.Add(gameObject.transform.Find("RedP1").gameObject);
        Enemies.Add(gameObject.transform.Find("RedP2").gameObject);

        for (int i = 0; i < 2; i++)
        {
            Allies[i].SetActive(false);
            Enemies[i].SetActive(false);
        }

        localPlayerId = PhotonNetwork.LocalPlayer.UserId;

        localTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (localTeam > 0)
        {
            localColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);
            enemyColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);
        }
        else
        {
            localColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);
            enemyColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);
        }

        SetValues(Allies[0], PhotonNetwork.LocalPlayer.UserId, localColor, PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString());



        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (PhotonNetwork.PlayerList[i].UserId != localPlayerId)
            {
                if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] == localTeam)
                {
                    SetValues(Allies[1], PhotonNetwork.PlayerList[i].UserId, localColor, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString());
                }
                else if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] != localTeam)
                {
                    if(Enemies[0].activeSelf == false)
                    {
                        SetValues(Enemies[0], PhotonNetwork.PlayerList[i].UserId, enemyColor, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString());
                    }
                    else
                    {
                        SetValues(Enemies[1], PhotonNetwork.PlayerList[i].UserId, enemyColor, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString());
                    }
                }
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetValues(GameObject player, string id, Color teamColor, string character)
    {
        player.SetActive(true);
        player.transform.Find("Player").GetComponent<Text>().text = character;
        player.transform.Find("Score").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[id + "Score"].ToString();
        player.transform.Find("Trash").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[id + "Trash"].ToString();
        player.transform.Find("Eliminations").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[id + "Kills"].ToString();
        player.transform.Find("Deaths").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[id + "Deaths"].ToString();

        Text[] texts = player.transform.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            text.color = teamColor;
        }
    }

}
