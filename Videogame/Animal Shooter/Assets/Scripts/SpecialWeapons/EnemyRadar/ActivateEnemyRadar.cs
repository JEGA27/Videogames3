using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ActivateEnemyRadar : MonoBehaviour
{
    public GameObject marker;
    public int timer;
    GameObject[] enemies;

    private PhotonView PV;
    ScoreSW sSW;

    void Start()
    {
        PV = GetComponent<PhotonView>();
        sSW = GetComponent<ScoreSW>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Q) && sSW.specialWeaponReady)
            {
                Debug.Log("Enemy Radar Activated");
                sSW.specialWeaponReady = false;
                PhotonNetwork.CurrentRoom.CustomProperties[sSW.idProgress] = 0;
                sSW.specialWeaponPoints = 0;
                sSW.specialWeaponProgress = 0;
                MarkEnemies();
                PlaySounds enemyradar = GetComponent<PlaySounds>();
                enemyradar.PlaySound(12);
            }
        }
    }

    // Start is called before the first frame update
    void MarkEnemies()
    {
        
        Debug.Log("Marking Enemies");
        if (gameObject.tag == "RedPlayer")
        {
            enemies = GameObject.FindGameObjectsWithTag("BluePlayer");
        }
        else
        {
            enemies = GameObject.FindGameObjectsWithTag("RedPlayer");
        }

        foreach (GameObject enemy in enemies)
        {
            int layer = gameObject.tag == "RedPlayer" ? 13 : 12;

            // var enemyMarker = Instantiate(marker, enemy.transform.position, Quaternion.identity, enemy.transform);
            // enemyMarker.layer = layer;
            // enemyMarker.transform.LookAt(transform);
            // enemyMarker.tag = "EnemyMarker";

            enemy.layer = layer;
            foreach (Transform t in enemy.transform)
            {
                t.gameObject.layer = layer;
            }
        }
        StartCoroutine(EndMark());
    }

    IEnumerator EndMark()
    {
        yield return new WaitForSeconds(timer);
    //     GameObject[] markers = GameObject.FindGameObjectsWithTag("EnemyMarker");
    //     foreach (GameObject marker in markers)
    //     {
    //         Destroy(marker);
    //     }
        foreach (GameObject enemy in enemies)
        {
            enemy.layer = 0;
            foreach (Transform t in enemy.transform)
            {
                t.gameObject.layer = 0;
            }
        }
    }

}
