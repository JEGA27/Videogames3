using Photon.Pun;
using Photon.Pun.Demo.Cockpit.Forms;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public Image victory;
    public Image defeat;
    int localTeam;

    public GameObject resultTeam;
    public GameObject resultEnemy;

    Color localColor;
    Color enemyColor;

    // Start is called before the first frame update
    void Start()
    {
        localTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (localTeam > 0)
        {
            localColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);
            enemyColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);

            resultTeam.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties["RedScore"].ToString();
            resultTeam.GetComponent<Text>().color = localColor;
            resultEnemy.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"].ToString();
            resultEnemy.GetComponent<Text>().color = enemyColor;

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"])
            {

                victory.gameObject.SetActive(true);
                defeat.gameObject.SetActive(false);
                PlaySounds w = GetComponent<PlaySounds>();
                w.PlaySound(10);
            }
            else {

                victory.gameObject.SetActive(false);
                defeat.gameObject.SetActive(true);
                PlaySounds loser = GetComponent<PlaySounds>();
                loser.PlaySound(11);

            }

        }
        else 
        {
            localColor = new Color(112f / 255f, 171f / 255f, 202f / 255f);
            enemyColor = new Color(229f / 255f, 61f / 255f, 77f / 255f);

            resultTeam.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"].ToString();
            resultTeam.GetComponent<Text>().color = localColor;
            resultEnemy.GetComponent<Text>().text = PhotonNetwork.CurrentRoom.CustomProperties["RedScore"].ToString();
            resultEnemy.GetComponent<Text>().color = enemyColor;

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"])
            {

                victory.gameObject.SetActive(true);
                defeat.gameObject.SetActive(false);
                PlaySounds w = GetComponent<PlaySounds>();
                w.PlaySound(10);
            }
            else
            {

                victory.gameObject.SetActive(false);
                defeat.gameObject.SetActive(true);
                PlaySounds loser = GetComponent<PlaySounds>();
                loser.PlaySound(11);

            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        // if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("BlueScore"))
        // {
        //     var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        //     hash.Add("BlueScore", 0);
        //     PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        //     PlaySounds winner = GetComponent<PlaySounds>();
        //     winner.PlaySound(10);
        //     //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);
        // }

        // if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("RedScore"))
        // {
        //     var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        //     hash.Add("RedScore", 0);
        //     PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        //     PlaySounds loser = GetComponent<PlaySounds>();
        //     loser.PlaySound(11);
        //     //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);
        // }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Leave();
        }
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Main Menu");
    }
}
