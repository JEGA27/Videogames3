using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SewerTeleport : MonoBehaviour
{
    [SerializeField]
    List<string> names;

    //[SerializeField]
    public List<GameObject> sewers;

    [SerializeField]
    List<GameObject> buttons;
    [SerializeField]
    GameObject sewersCanvas;
    [SerializeField]
    GameObject abilityMessage;

    List<Vector3> sewersPositions;
    ThirdPersonShooterController tpsc;
    bool isOnSewer = false;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < names.Count; i++)
        {
            sewers.Add(GameObject.Find(names[i]));
            buttons[i].GetComponent<SewerButton>().sewerCoordinates = new Vector3(sewers[i].transform.position.x, transform.position.y, sewers[i].transform.position.z);
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
         PlaySounds teleport = GetComponent<PlaySounds>();
         teleport.PlaySound(14);
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
            other.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            isOnSewer = true;
            anim = other.gameObject.GetComponent<Animator>();
            anim.SetTrigger("characterIsClose");
        }
    }

    public void OnTriggerExit(Collider other)
    {
      if(other.gameObject.tag == "Sewer")
      {
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            sewersCanvas.SetActive(false);
            isOnSewer = false;
            anim = other.gameObject.GetComponent<Animator>();
            anim.SetTrigger("characterIsFar");
            tpsc.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;

      }
    }


}
