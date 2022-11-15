using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public string nextScene;
    public Font myFont;

    public Image Team1;
    public Image Team2;

    public Transform PlayersTxt;

    public GameObject CharacterPanel;

    public Button button1;
    public Button button2;
    public Button button3;
    public Button confirm;

    public List<Text> texts;
    public List<Text> textsEnemy;
    public Text test;
    public Text enemy1;

    public Text blueCounter;
    public Text redCounter;

    int blueReady;
    int redReady;

    string selection = "none";
    bool scenechanged = false;
    bool available;

    int localTeam;

    //public string selection;

    // Start is called before the first frame update
    void Start()
    {
        test.text = "Player 1";
        enemy1.text = "Enemy 1";

        texts = new List<Text>();
        textsEnemy = new List<Text>();

        texts.Add(test);
        textsEnemy.Add(enemy1);

        localTeam = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];

        if (localTeam > 0) {
            Team1.color = new Color(229f / 255f, 61f / 255f, 77f / 255f);
            Team2.color = new Color(112f / 255f, 171f / 255f, 202f / 255f);
        }

        for (int i = 0; i < (int)PhotonNetwork.CurrentRoom.MaxPlayers/2; i++) {

            CreateText(i + 2, Color.white,false);
            CreateText(i + 2, Color.white,true);
        }

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {

            if (PhotonNetwork.PlayerList[i] == PhotonNetwork.LocalPlayer)
            {

                texts[(int)i / 2].text += " (me)";
                texts[(int)i / 2].color = Color.yellow;
            }

           
        }

    }

    // Update is called once per frame
    void Update()
    {

        blueReady = 0;
        redReady = 0;

        

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {

            //Debug.Log(PhotonNetwork.PlayerList[i].ToStringFull());

            if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] != "none")
            {
                //Debug.Log((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] + i.ToString());

                if ((int)PhotonNetwork.PlayerList[i].CustomProperties["Team"] == localTeam)
                {


                    if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == "raccoon") button1.interactable = false;
                    else if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == "rat") button2.interactable = false;
                    else if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == "cat") button3.interactable = false;

                    texts[(int)i / 2].text = (string)PhotonNetwork.PlayerList[i].CustomProperties["Character"];

                }
                else {
                    textsEnemy[(int)i / 2].text = (string)PhotonNetwork.PlayerList[i].CustomProperties["Character"];
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
        CharacterPanel.SetActive(true);
    }

    public void Ardilla()
    {

        selection = "squirrel";
        CharacterPanel.SetActive(true);
    }

    public void Rata()
    {
        selection = "rat";
        CharacterPanel.SetActive(true);
    }

    public void Gato()
    {

        selection = "cat";
        CharacterPanel.SetActive(true);
    }

    public void Confirm()
    {
        available = true;

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character"))
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {

                if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == selection) {

                    available = false;
                }
                
            }

            if (available)
            {

                var hash = PhotonNetwork.LocalPlayer.CustomProperties;
                hash["Character"] = selection;
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

            }


        }
        else
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {

                if ((string)PhotonNetwork.PlayerList[i].CustomProperties["Character"] == selection)
                {

                    available = false;
                }

            }

            if (available) {

                var hash = PhotonNetwork.LocalPlayer.CustomProperties;
                hash.Add("Character", selection);
                PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            }

        }

        if (available) {
            button1.interactable = false;
            button2.interactable = false;
            button3.interactable = false;

            confirm.gameObject.SetActive(false);

        }
        
    }

    void CreateText(int player, Color text_color, bool enemy)
    {
        string name;
        if (!enemy) name = "Player " + player.ToString();
        else name = "Enemy " + player.ToString();


        GameObject UItextGO = new GameObject(name);
        UItextGO.transform.SetParent(PlayersTxt);

        RectTransform trans = UItextGO.AddComponent<RectTransform>();

        float y;
        if (!enemy) y = texts[texts.Count-1].GetComponent<RectTransform>().anchoredPosition.y - 224f;
        else y = textsEnemy[textsEnemy.Count - 1].GetComponent<RectTransform>().anchoredPosition.y - 224f;



        trans.anchoredPosition = new Vector2(224, y);
        trans.sizeDelta = new Vector2(321, 87);


        Text text = UItextGO.AddComponent<Text>();
        text.text = name;
        text.fontSize = 40;
        text.font = myFont;
        text.color = text_color;
        text.alignment = TextAnchor.UpperLeft;

        if (!enemy) texts.Add(UItextGO.GetComponent<Text>());
        else textsEnemy.Add(UItextGO.GetComponent<Text>());

    }

}
