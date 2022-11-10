using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public List<GameObject> redteam;
    public List<GameObject> blueteam;

    // Start is called before the first frame update
    void Start()
    {
        redteam = new List<GameObject>();
        blueteam = new List<GameObject>();

        Spawn();


    }

    public void Spawn() {

        int team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        Debug.Log($"Team number {team} is being instantiated");
        //instantiate the blue player if team is 0 and red if it is not
        if (team == 0)
        {
            //get a spawn for the correct team
            Vector3 spawnblue = new Vector3(1, 0, 50.4f);
            //PhotonNetwork.Instantiate(playerPrefab.name, blueteam[0].transform.position, Quaternion.identity);
            PhotonNetwork.Instantiate(playerPrefab.name, spawnblue, Quaternion.Euler(0, 180, 0));
        }
        else
        {
            //now for the red team
            Vector3 spawnred = new Vector3(1, 0, -9.2f);
            //PhotonNetwork.Instantiate(playerPrefab.name, redteam[0].transform.position, Quaternion.identity);
            PhotonNetwork.Instantiate(playerPrefab.name, spawnred, Quaternion.identity);
        }
    }

}
