using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
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

    NavMeshAgent navMeshAgent;
    public float timerForNewPath;
    bool inCoRoutine;
    Vector3 target;
    NavMeshPath path;
    bool validPath;
    float speed;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        curPlayers = PhotonNetwork.PlayerList.Length;
        players = new List<GameObject>();
        GetCurrentPlayers();

        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        speed = Random.Range(1, 4);
        navMeshAgent.speed = speed;

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
        _animator.SetBool("Running",false);
        if (distance <= AttackRadius)
        {
            currentState = FSMStates.Evade;

        }
        else {
            if (!inCoRoutine)
            {
                StartCoroutine(DoSomething());
            }
        }

    }

    void UpdateEvade()
    {

        _animator.SetBool("Running", true);
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

    IEnumerator DoSomething()
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timerForNewPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        if (!validPath) Debug.Log("Found an invalid path");
        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }

    Vector3 getNewRandomPosition()
    {
        float x = Random.Range(-40, 40);
        float z = Random.Range(-40, 40);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }
}
