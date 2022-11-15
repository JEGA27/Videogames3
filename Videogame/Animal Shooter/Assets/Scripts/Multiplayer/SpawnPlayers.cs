using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject raccoonPrefab;
    public GameObject ratPrefab;
    public GameObject catPrefab;
    

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public List<GameObject> redteam;
    public List<GameObject> blueteam;

    bool mapReady;

    // Start is called before the first frame update
    void Start()
    {
        redteam = new List<GameObject>();
        blueteam = new List<GameObject>();

        Spawn();

        // Set the map
        // CHECAR
        // if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("MapReady"))
        // {
        //     if(!(bool)PhotonNetwork.CurrentRoom.CustomProperties["MapReady"])
        //     {
        //         GameObject mapManager = PhotonNetwork.Instantiate("MapManager", Vector3.zero, Quaternion.identity);
        //         mapManager.name = "MapManager";

        //         mapReady = true;
        //         var hash = PhotonNetwork.CurrentRoom.CustomProperties;
        //         hash["MapReady"] = mapReady;
        //         PhotonNetwork.CurrentRoom.SetCustomProperties(hash);
        //     }
        // }
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            GameObject mapManager = PhotonNetwork.Instantiate("MapManager", Vector3.zero, Quaternion.identity);
            mapManager.name = "MapManager";
        }
    }

    public void Spawn() {
   

        int team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        
        //instantiate the blue player if team is 0 and red if it is not
        if (team == 0)
        {
            //get a spawn for the correct team
            Vector3 spawnblue = new Vector3(1, 0, 50.4f);
            //PhotonNetwork.Instantiate(playerPrefab.name, blueteam[0].transform.position, Quaternion.identity);

            if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "raccoon") PhotonNetwork.Instantiate(raccoonPrefab.name, spawnblue, Quaternion.Euler(0, 180, 0));
            else if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "rat") PhotonNetwork.Instantiate(ratPrefab.name, spawnblue, Quaternion.Euler(0, 180, 0));
            else if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "cat") PhotonNetwork.Instantiate(catPrefab.name, spawnblue, Quaternion.Euler(0, 180, 0));
        }
        else
        {
            //now for the red team
            Vector3 spawnred = new Vector3(1, 0, -9.2f);
            //PhotonNetwork.Instantiate(playerPrefab.name, redteam[0].transform.position, Quaternion.identity);
            //PhotonNetwork.Instantiate(raccoonPrefab.name, spawnred, Quaternion.identity);

            if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "raccoon") PhotonNetwork.Instantiate(raccoonPrefab.name, spawnred, Quaternion.identity);
            else if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "rat") PhotonNetwork.Instantiate(ratPrefab.name, spawnred, Quaternion.identity);
            else if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == "cat") PhotonNetwork.Instantiate(catPrefab.name, spawnred, Quaternion.identity);
        }
    }

}
