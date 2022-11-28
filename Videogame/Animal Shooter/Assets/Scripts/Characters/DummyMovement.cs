using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class DummyMovement : MonoBehaviour
{

    private enum FSMStates
    {
        Evade, Idle
    }

    private FSMStates currentState = FSMStates.Idle;
    public Vector3 escapePoint;
    public List<GameObject> players;
    
    public int curPlayers;

    public float curSpeed = 20f;
    public float rotSpeed = 150.0f;

    public float AttackRadius = 1.0f;
    public float distance = 10000f;
    public GameObject curPlayer;

    // Start is called before the first frame update
    void Start()
    {
        curPlayers = PhotonNetwork.PlayerList.Length;
        players = new List<GameObject>();
        GetCurrentPlayers();

       
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentPlayers();
        CalculateDistance();
        switch (currentState)
        {
            case FSMStates.Idle:
                UpdateIdle();
                break;

            case FSMStates.Evade:
                UpdateEvade();
                break;
        }
    }


    void UpdateIdle() 
    {

        if (distance <= AttackRadius)
        {
            currentState = FSMStates.Evade;

        }

    }

    void UpdateEvade()
    {


        if (distance <= AttackRadius)
        {

            var lookPos = curPlayer.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(-lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);


        }
        else currentState = FSMStates.Idle;

    }

    void GetCurrentPlayers() {
        //players = GameObject.FindGameObjectsWithTag("BluePlayer").ToList();
        players = GetObjectsInLayer(14);
        //players.AddRange(GameObject.FindGameObjectsWithTag("BluePlayer").ToList());
    }

    void CalculateDistance() 
    {

        for (int i = 0; i < players.Count; i++)
        {
            if (i < 1) {
                distance = Vector3.Distance(transform.position, players[i].transform.position);
                curPlayer = players[i];
            }
            

            if (distance > Vector3.Distance(transform.position, players[i].transform.position)) {

                distance = Vector3.Distance(transform.position, players[i].transform.position);
                curPlayer = players[i];
            }
        }
        

    }

    private static List<GameObject> GetObjectsInLayer(int layer)
    {
        var goArray = UnityEngine.Object.FindObjectsOfType<GameObject>();
        var goList = new List<GameObject>();
        for (var i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList;
    }
}
