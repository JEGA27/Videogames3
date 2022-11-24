using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class FollowPath : MonoBehaviour
{
    [SerializeField]
    private List<Transform> destinationPoints;

    private int currentObjective;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("IdPlayer"))
        {
            byte id = 1;
            if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] == id)
            {
                agent = GetComponent<NavMeshAgent>();
                currentObjective = 0;
                // Get the navpoints from set map script
                destinationPoints = GameObject.FindGameObjectWithTag("MapManager").GetComponent<SetMap>().navPointsTransform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("IdPlayer"))
        {
            byte id = 1;
            if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] == id)
            {
                agent.destination = destinationPoints[currentObjective].position;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NavPoint")
        {
            currentObjective++;
        }

        if (currentObjective == destinationPoints.Count)
        {
            currentObjective = 0;
        }
    }
}
