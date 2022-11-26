using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BlackHoleSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject redBlackHole;
    [SerializeField]
    private GameObject blueBlackHole;
    public string playerTag;

    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision col)
    {
        if (PV.IsMine)
        {
            Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
            if(playerTag == "RedPlayer")
            {
                PhotonNetwork.Instantiate(redBlackHole.name, spawnPosition, Quaternion.identity);
            }
            else if(playerTag == "BluePlayer")
            {
                PhotonNetwork.Instantiate(blueBlackHole.name, spawnPosition, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}
