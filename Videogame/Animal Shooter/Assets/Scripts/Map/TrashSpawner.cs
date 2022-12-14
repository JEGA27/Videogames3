using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TrashSpawner : MonoBehaviour
{
    public List<GameObject> trashPrefabs = new List<GameObject>();

    public int trashToSpawn;
    public int limitTrash;  //Variable for the trash limit
    public float rateInMiddle; //0.0 - 1.0 Percentage of which the amount will be in the middle
    public float percentageMiddleSize; //0.0 - 1.0 Percentage of the size considered for middle

    private Vector3 centerOfSpawn;
    private float distanceFromCenterMin;
    private float distanceFromCenterMax;
    private float sizeZ;

    public float time;
    public float timeToSpawn;

    private Vector3 extends;
    private Vector3 bottomLeft;
    private Vector3 bottomRight;
    private Vector3 topLeft;
    private Vector3 topRight;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("IdPlayer"))
        {
            byte id = 1;
            if ((byte)PhotonNetwork.LocalPlayer.CustomProperties["IdPlayer"] == id) 
            {
                SetTrash();
            }
        }                
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        GameObject[] trashLimit;
        trashLimit = GameObject.FindGameObjectsWithTag("Trash");

        if (time > timeToSpawn && trashLimit.Length <= limitTrash)
        {
            StartCoroutine(TimedSpawner());
            trashLimit = null;
            time = 0.0f;
        }

    }

    void SetTrash()
    {
        time = 0;

        centerOfSpawn = this.GetComponent< Renderer>().bounds.center;
        
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        extends = meshFilter.mesh.bounds.extents;
        bottomLeft = transform.TransformPoint(new Vector3(-extends.x, 0, -extends.z)); 
        bottomRight = transform.TransformPoint(new Vector3(extends.x, 0, -extends.z));
        topLeft = transform.TransformPoint(new Vector3(-extends.x, 0, extends.z));
        topRight = transform.TransformPoint(new Vector3(extends.x, 0, extends.z));
        
        sizeZ = topRight.z - bottomRight.z;

        rateInMiddle *= trashToSpawn;
        
        rateInMiddle = (int)rateInMiddle;
        
        distanceFromCenterMin = centerOfSpawn.z - ((sizeZ / 2) * percentageMiddleSize);
        distanceFromCenterMax = centerOfSpawn.z + ((sizeZ / 2) * percentageMiddleSize);

        for (int i = 0; i < rateInMiddle; i++)
        {
            float posX = Random.Range(bottomLeft.x, topRight.x);
            float posZ = Random.Range(distanceFromCenterMin, distanceFromCenterMax);
            Vector3 pos = new Vector3(posX, centerOfSpawn.y, posZ);
            PhotonNetwork.Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Count)].name, pos, Random.rotation);
        }

        trashToSpawn -= (int)rateInMiddle;
        
        for (int i = 0; i < (trashToSpawn / 2); i++)
        {
            float posX = Random.Range(bottomLeft.x, topRight.x);
            float posZ = Random.Range(bottomRight.z, distanceFromCenterMin);
            Vector3 pos = new Vector3(posX, centerOfSpawn.y, posZ);
            PhotonNetwork.Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Count)].name, pos, Random.rotation);
        }

        for (int i = 0; i < (trashToSpawn / 2); i++)
        {
            float posX = Random.Range(bottomLeft.x, topRight.x);
            float posZ = Random.Range(distanceFromCenterMax, topRight.z);
            Vector3 pos = new Vector3(posX, centerOfSpawn.y, posZ);
            PhotonNetwork.Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Count)].name, pos, Random.rotation);
        }
    }

    IEnumerator TimedSpawner()
    {
        float posZ = Random.Range(bottomLeft.z, topRight.z);
        float posX = Random.Range(bottomLeft.x, topRight.x);
        Vector3 pos = new Vector3(posX, centerOfSpawn.y, posZ);
        PhotonNetwork.Instantiate(trashPrefabs[Random.Range(0, trashPrefabs.Count)].name, pos, Random.rotation);
        yield return new WaitForSeconds(1f);
    }

}


