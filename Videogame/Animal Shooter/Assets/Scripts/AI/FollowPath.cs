using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private List<Transform> destinationPoints;

    private int currentObjective;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentObjective = 0;
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = destinationPoints[currentObjective].position;

          Debug.Log(currentObjective);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NavPoint")
        {
            currentObjective++;
        }

        if(currentObjective == 8)
        {
            currentObjective = 0;
        }


    }
}
