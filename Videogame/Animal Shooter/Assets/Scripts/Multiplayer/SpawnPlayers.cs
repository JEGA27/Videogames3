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

    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX,maxX), -0.1608448f, Random.Range(minY,maxY));
        PhotonNetwork.Instantiate(playerPrefab.name,randomPosition,Quaternion.identity) ;
    }
}
