using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ScoreboardSync : MonoBehaviour
{
    public List<GameObject> allies;
    List<GameObject> enemies;

    Color teamColor;
    Color enemyColor;

    int localTeam;

    Dictionary<GameObject, string> Ids = new Dictionary<GameObject, string>();


    // Start is called before the first frame update
    void Start()
    {
        allies = new List<GameObject>();
        allies.Add(gameObject.transform.Find("BlueP1").gameObject);
        allies.Add(gameObject.transform.Find("BlueP2").gameObject);


        enemies = new List<GameObject>();
        enemies.Add(gameObject.transform.Find("RedP1").gameObject);
        enemies.Add(gameObject.transform.Find("RedP2").gameObject);

        foreach (GameObject ally in allies)
        {
            ally.SetActive(false);
        }
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }

        localTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (localTeam > 0)
        {
            teamColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);
            enemyColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);
        }
        else
        {
            teamColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);
            enemyColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);
        }

        SetValues(allies[0], PhotonNetwork.LocalPlayer.UserId, PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString(), teamColor);

        CreateOtherValues();
    }

    // Update is called once per frame
    void Update()
    {   
        CreateOtherValues();
        UpdateValues();
    }

    void SetValues(GameObject line, string userId, string character, Color textColor)
    {
        Debug.Log("Setting values for " + userId);
        Debug.Log("Character: " + character);
        Debug.Log("Text color: " + textColor);
        Debug.Log("Line: " + line.name);

        line.SetActive(true);
        line.transform.Find("Player").GetComponent<Text>().text = character;
        line.transform.Find("Score").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Score"].ToString();
        line.transform.Find("Trash").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Trash"].ToString();
        line.transform.Find("Eliminations").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Kills"].ToString();
        line.transform.Find("Deaths").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Deaths"].ToString();
        Text[] texts = line.transform.GetComponentsInChildren<Text>();
        foreach (Text text in texts)
        {
            text.color = textColor;
        }

        if (userId == PhotonNetwork.LocalPlayer.UserId)
        {
            line.transform.Find("Player").GetComponent<Text>().text += " (me)";
        }


        Ids.Add(line, userId);
    }

    void CreateOtherValues()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            if (!Ids.ContainsValue(PhotonNetwork.PlayerList[i].UserId))
            {

                if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] == localTeam && PhotonNetwork.PlayerList[i].UserId != PhotonNetwork.LocalPlayer.UserId)
                {
                    SetValues(allies[1], PhotonNetwork.PlayerList[i].UserId, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString(), teamColor);
                }
                else if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] != localTeam)
                {
                    int index = Ids.ContainsKey(enemies[0]) ? 1 : 0;
                    SetValues(enemies[index], PhotonNetwork.PlayerList[i].UserId, PhotonNetwork.PlayerList[i].CustomProperties["Character"].ToString(), enemyColor);
                }
            }
        }
    }

    void UpdateValues()
    {
        List<GameObject> keyList = new List<GameObject>(Ids.Keys);

        for (int i = 0; i < Ids.Count; i++)
        {
            GameObject line = keyList[i];
            string userId = Ids[line];

            line.transform.Find("Score").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Score"].ToString();
            line.transform.Find("Trash").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Trash"].ToString();
            line.transform.Find("Eliminations").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Kills"].ToString();
            line.transform.Find("Deaths").GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties[userId + "Deaths"].ToString();
        }
    }
}
