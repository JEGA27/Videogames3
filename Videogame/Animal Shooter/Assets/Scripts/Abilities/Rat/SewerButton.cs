using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SewerButton : MonoBehaviour
{
    [SerializeField]
    public GameObject rat;

    public Vector3 sewerCoordinates;
    private SewerTeleport sewerTeleport;

    // Start is called before the first frame update
    void Start()
    {
        sewerTeleport = rat.GetComponent<SewerTeleport>();
        //gameObject.GetComponent<Button>().onClick.AddListener(() => Teleport());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Teleport()
    {
        sewerTeleport.TeleportToSewer(sewerCoordinates);
        Debug.Log(sewerCoordinates);
    }

    public void ButtonClicked()
    {
        Debug.Log("Clicked");
    }


}
