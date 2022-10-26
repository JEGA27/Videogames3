using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SewerTeleport : MonoBehaviour
{
    [SerializeField]
    List<GameObject> sewers;

    [SerializeField]
    List<GameObject> buttons;
    [SerializeField]
    GameObject sewersCanvas;
    [SerializeField]
    GameObject abilityMessage;

    List<Vector3> sewersPositions;
    ThirdPersonShooterController tpsc;
    bool isOnSewer = false;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < sewers.Count; i++)
        {
            buttons[i].GetComponent<SewerButton>().sewerCoordinates = sewers[i].transform.position;
        }
        sewersCanvas.SetActive(false);
        abilityMessage.SetActive(false);
        tpsc = gameObject.GetComponent<ThirdPersonShooterController>();

    }

    // Update is called once per frame
    void Update()
    {
      if(isOnSewer && Input.GetKeyDown(KeyCode.G))
      {
         tpsc.enabled = false;
         sewersCanvas.SetActive(true);
         Cursor.lockState= CursorLockMode.None;
      }
    }

    public void TeleportToSewer(Vector3 coordinates)
    {
        gameObject.transform.position = coordinates;
        sewersCanvas.SetActive(false);
        tpsc.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sewer")
        {

            abilityMessage.SetActive(true);
            isOnSewer = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
      if(other.gameObject.tag == "Sewer")
      {

          abilityMessage.SetActive(false);
          sewersCanvas.SetActive(false);
          isOnSewer = false;
          tpsc.enabled = true;
          Cursor.lockState = CursorLockMode.Locked;

      }
    }


}