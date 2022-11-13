using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public string nextScene;
    public Font myFont;

    public Transform Canvas;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button confirm;

    List<Text> texts;
    public Text test;


    public Text blueCounter;
    public Text redCounter;

    int blueReady;
    int redReady;

    string selection = "none";
    bool scenechanged = false;

    //public string selection;

    // Start is called before the first frame update
    void Start()
    {
        test.text = "Player 1";

        texts = new List<Text>();
        texts.Add(test);

        for (int i = 0; i < (int)PhotonNetwork.CurrentRoom.MaxPlayers/2; i++) {

            CreateText(i+2, Color.black);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        blueReady = 0;
        redReady = 0;

        

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {

            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer) {

                texts[(int)i / 2].text += " (me)";
                texts[(int)i / 2].color = Color.yellow;
            } 

            //Debug.Log(PhotonNetwork.PlayerList[i].ToStringFull());
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] != "none")
            {
                Debug.Log((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] + i.ToString());
                
                if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] == (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"]) {
                    

                    if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == "raccoon") button1.interactable = false;
                    else if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == "rat") button2.interactable = false;
                    else if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == "cat") button3.interactable = false;

                    texts[(int)i / 2].text = (string)PhotonNetwork.PlayerList[i].CustomProperties["Character"];

                }
                if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] > 0) redReady++;
                else blueReady++;


            }
        }

        if (blueReady + redReady >= (int)PhotonNetwork.CurrentRoom.MaxPlayers && !scenechanged) {

            PhotonNetwork.LoadLevel(nextScene);
            scenechanged = true;
        }

        /*if (PhotonNetwork.PlayerList.Length < (int)PhotonNetwork.CurrentRoom.MaxPlayers && !scenechanged)
        {

            PhotonNetwork.LoadLevel("MainMenu");
            scenechanged = true;
        }*/

        blueCounter.text = blueReady.ToString();
        redCounter.text = redReady.ToString();
    }

    public void Mapache() {

        selection = "raccoon";
    }

    public void Ardilla()
    {

        selection = "squirrel";
    }

    public void Rata()
    {

        selection = "rat";
    }

    public void Gato()
    {

        selection = "cat";
    }

    public void Confirm()
    {

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character"))
        {

            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash["Character"] = selection;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
        else
        {

            var hash = PhotonNetwork.LocalPlayer.CustomProperties;
            hash.Add("Character", selection);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        }

        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;

        confirm.gameObject.SetActive(false);
    }

    void CreateText(int player, Color text_color)
    {
        string name = "Player " + player.ToString();
        GameObject UItextGO = new GameObject(name);
        UItextGO.transform.SetParent(Canvas);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();

        float y = texts[texts.Count-1].GetComponent<RectTransform>().anchoredPosition.y - 120f;

        trans.anchoredPosition = new Vector2(-697, y);
        trans.sizeDelta = new Vector2(321, 87);


        Text text = UItextGO.AddComponent<Text>();
        text.text = name;
        text.fontSize = 40;
        text.font = myFont;
        text.color = text_color;
        text.alignment = TextAnchor.UpperLeft;

        texts.Add(UItextGO.GetComponent<Text>());

    }

}
