using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SewerTeleport : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sewers;

    [SerializeField]
    List<GameObject> buttons;

    List<Vector3> sewersPositions;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sewers.Count; i++)
        {
            buttons[i].GetComponent<SewerButton>().sewerCoordinates = sewers[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TeleportToSewer(Vector3 coordinates)
    {
        gameObject.transform.position = coordinates;
    }
}
