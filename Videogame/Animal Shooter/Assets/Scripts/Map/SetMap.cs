using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SetMap : MonoBehaviour
{
    // TRASH SPAWNERS
    [Header("Trash Spawners")]
    public GameObject trashSpawnerPrefab;
    public List<Vector3> trashSpawnersPositions;
    // Default Positions
    // Vector3(-8.87,18.73,20.875)
    // Vector3(8.62,18.73,20.875)

    // AGENTS
    [Header("Agents")]
    public GameObject agentPrefab;
    public List<Vector3> agentPositions;
    // Default Positions
    // Vector3(-14.61f, 0.224f, -2.706f)
    public List<Quaternion> agentRotations;
    // Default Rotations
    // Quaternion(0, 90, 0)
    public GameObject navPointPrefab;
    public List<Vector3> navPointsPositions;
    // Default Positions
    // Vector3(-20.1,0.15,-3.16)  1
    // Vector3(21.6,0.15,-3.25)   2
    // Vector3(21.24,0.15,45.02)  3
    // Vector3(-0.92,0.15,44.17)  4
    // Vector3(-0.99,0.15,-0.41)  5
    // Vector3(-17.53,0.15,0.39)  6
    // Vector3(-16.84,0.15,41.73) 7
    // Vector3(-20.92,0.15,44.02) 8
    public List<Transform> navPointsTransform;
    bool navPointsSet;

    // DUMPSTERS
    [Header("Dumpsters")]
    public GameObject dumpsterPrefab;
    public List<Vector3> dumpsterPositions;
    // Default Positions
    // Vector3(-13.34,0.16,12.47)
    // Vector3(13.89,0.16,38.77)
    public List<Quaternion> dumpsterRotations;
    // Default Rotations
    // Quaternion(0, -90, 0)
    // Quaternion(0, 90, 0)

    // DUMMIES
    [Header("Dummies")]
    public GameObject dummyPrefab;
    public List<Vector3> dummyPositions;
    // Default Positions
    // Vector3(12.36,0.021,32.52) 1
    // Vector3(8.37,0.021,33.13)  2
    // Vector3(11.98,0.021,35.81) 3
    // Vector3(8.69,0.021,35.90)  4
    // Vector3(11.76,-0.16,38.85) 5
    // Vector3(9.22,-0.16,38.94)  6
    public List<Quaternion> dummyRotations;
    // Default Rotations
    // Quaternion(0, 0, 0) All

    // CITY OBJECTS
    [Header("City Objects")]
    public GameObject redTruck;
    public GameObject orangeTruck;
    public GameObject car;
    public GameObject statue;

    void Awake()
    {
        // Trash Spawners
        trashSpawnersPositions = new List<Vector3>();
        // Agents
        navPointsPositions = new List<Vector3>();
        agentPositions = new List<Vector3>();
        agentRotations = new List<Quaternion>();
        navPointsTransform = new List<Transform>();
        navPointsSet = false;
        // Dumpsters
        dumpsterPositions = new List<Vector3>();
        dumpsterRotations = new List<Quaternion>();
        // Dummies
        dummyPositions = new List<Vector3>();
        dummyRotations = new List<Quaternion>();

        // Set default values
        SetDefaultProp();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("IdPlayer"))
        {
            byte id = 1;
            if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] == id) 
            {
                Set();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Set Propierties to the objects
    void SetDefaultProp()
    {
        // Trash Spawners
        trashSpawnersPositions.Add(new Vector3(-8.87f, 18.73f, 20.875f));
        trashSpawnersPositions.Add(new Vector3(8.62f, 18.73f, 20.875f));
        // Agents
        navPointsPositions.Add(new Vector3(-20.1f, 0.15f, -3.16f));
        navPointsPositions.Add(new Vector3(21.6f, 0.15f, -3.25f));
        navPointsPositions.Add(new Vector3(21.24f, 0.15f, 45.02f));
        navPointsPositions.Add(new Vector3(-0.92f, 0.15f, 44.17f));
        navPointsPositions.Add(new Vector3(-0.99f, 0.15f, -0.41f));
        navPointsPositions.Add(new Vector3(-17.53f, 0.15f, 0.39f));
        navPointsPositions.Add(new Vector3(-16.84f, 0.15f, 41.73f));
        navPointsPositions.Add(new Vector3(-20.92f, 0.15f, 44.02f));
        agentPositions.Add(new Vector3(-14.61f, 0.224f, -2.706f));
        agentRotations.Add(Quaternion.Euler(0, 90, 0));
        // Dumpsters
        dumpsterPositions.Add(new Vector3(-13.34f, 0.16f, 12.47f));
        dumpsterPositions.Add(new Vector3(13.89f, 0.16f, 38.77f));
        dumpsterRotations.Add(Quaternion.Euler(0, -90, 0));
        dumpsterRotations.Add(Quaternion.Euler(0, 90, 0));
        // Dummies
        dummyPositions.Add(new Vector3(12.36f, 0.021f, 32.52f));
        dummyPositions.Add(new Vector3(8.37f, 0.021f, 33.13f));
        dummyPositions.Add(new Vector3(11.98f, 0.021f, 35.81f));
        dummyPositions.Add(new Vector3(8.69f, 0.021f, 35.90f));
        dummyPositions.Add(new Vector3(11.76f, -0.16f, 38.85f));
        dummyPositions.Add(new Vector3(9.22f, -0.16f, 38.94f));
        dummyRotations.Add(Quaternion.Euler(0, 0, 0));
        dummyRotations.Add(Quaternion.Euler(0, 0, 0));
        dummyRotations.Add(Quaternion.Euler(0, 0, 0));
        dummyRotations.Add(Quaternion.Euler(0, 0, 0));
        dummyRotations.Add(Quaternion.Euler(0, 0, 0));
        dummyRotations.Add(Quaternion.Euler(0, 0, 0));
    }

    // Set the map
    void Set()
    {
        TrashSpawners();
        Agents();
        Dumpsters();
        Dummies();
        CityObjects();
    }

    // Destroy the objects and set the map again
    public void Reset()
    {
        // Trash Spawners
        GameObject[] trashSpawners = GameObject.FindGameObjectsWithTag("TrashSpawner");
        foreach (GameObject trashSpawner in trashSpawners)
        {
            PhotonNetwork.Destroy(trashSpawner);
        }
        // Agents
        GameObject[] agents = GameObject.FindGameObjectsWithTag("Agent");
        foreach (GameObject agent in agents)
        {
            PhotonNetwork.Destroy(agent);
        }
        // Dumpsters
        GameObject[] dumpsters = GameObject.FindGameObjectsWithTag("Dumpster");
        foreach (GameObject dumpster in dumpsters)
        {
            PhotonNetwork.Destroy(dumpster);
        }
        // Dummies
        GameObject[] dummies = GameObject.FindGameObjectsWithTag("Dummy");
        foreach (GameObject dummy in dummies)
        {
            PhotonNetwork.Destroy(dummy);
        }
        // Trash
        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        foreach (GameObject trashObject in trash)
        {
            PhotonNetwork.Destroy(trashObject);
        }
        // City Objects
        GameObject[] cityObjects = GameObject.FindGameObjectsWithTag("CityObject");
        foreach (GameObject cityObject in cityObjects)
        {
            PhotonNetwork.Destroy(cityObject);
        }
        // Set the map
        Set();
    }
    
    // Instantiate Trash Spawners
    void TrashSpawners()
    {
        foreach (Vector3 trashSpawnerPos in trashSpawnersPositions)
        {
            PhotonNetwork.Instantiate(trashSpawnerPrefab.name, trashSpawnerPos, Quaternion.identity);
        }    
    }

    // Instantiate Agents and NavPoints for the agents
    void Agents()
    {
        if(!navPointsSet)
        {
            foreach (Vector3 navPoint in navPointsPositions)
            {
                GameObject nav = PhotonNetwork.Instantiate(navPointPrefab.name, navPoint, Quaternion.identity);
                navPointsTransform.Add(nav.transform);
            }
            navPointsSet = true;
        }
        for(int i = 0; i < agentPositions.Count; i++)
        {
            PhotonNetwork.Instantiate(agentPrefab.name, agentPositions[i], agentRotations[i]);
        }
    }

    // Instantiate Dumpsters
    void Dumpsters()
    {
        for(int i = 0; i < dumpsterPositions.Count; i++)
        {
            PhotonNetwork.Instantiate(dumpsterPrefab.name, dumpsterPositions[i], dumpsterRotations[i]);
        }
    }

    // Instantiate Dummies
    void Dummies()
    {
        for(int i = 0; i < dummyPositions.Count; i++)
        {
            PhotonNetwork.Instantiate(dummyPrefab.name, dummyPositions[i], dummyRotations[i]);
        }
    }

    // Instantiate City Objects
    void CityObjects()
    {
        // Red Truck
        PhotonNetwork.Instantiate(redTruck.name, redTruck.transform.position, redTruck.transform.rotation);
        // Orane Truck
        PhotonNetwork.Instantiate(orangeTruck.name, orangeTruck.transform.position, orangeTruck.transform.rotation);
        // Blue Car
        PhotonNetwork.Instantiate(car.name, car.transform.position, car.transform.rotation);
        // Statue
        PhotonNetwork.Instantiate(statue.name, statue.transform.position, statue.transform.rotation);
    }


}
