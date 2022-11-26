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
    // Start is called before the first frame update
    void Start()
    {
        localTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (localTeam > 0)
        {

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] < (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"])
            {

                victory.gameObject.SetActive(true);
                defeat.gameObject.SetActive(false);
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

            if ((int)PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"] > (int)PhotonNetwork.CurrentRoom.CustomProperties["RedScore"])
            {

                victory.gameObject.SetActive(true);
                defeat.gameObject.SetActive(false);
            }
            else
            {

                victory.gameObject.SetActive(false);
                defeat.gameObject.SetActive(true);
                PlaySounds winner = GetComponent<PlaySounds>();
                winner.PlaySound(10);

            }

        }



    }

    // Update is called once per frame
    void Update()
    {
        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("BlueScore"))
        {
            var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            hash.Add("BlueScore", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
            //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);
        }

        if (!PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("RedScore"))
        {
            var hash = PhotonNetwork.CurrentRoom.CustomProperties;
            hash.Add("RedScore", 0);
            PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
            //Debug.Log(PhotonNetwork.CurrentRoom.CustomProperties["BlueScore"]);
        }
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }
}
