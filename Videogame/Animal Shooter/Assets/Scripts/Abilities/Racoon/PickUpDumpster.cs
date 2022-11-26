using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PickUpDumpster : MonoBehaviour
{
    public GameObject dumpsterPos;

    private bool canPickUp;
    private GameObject Dumpster;
    private GameObject UI_Dumpster;
    private bool hasDumpster;
    private StarterAssetsInputs starterAssetsInputs;

    PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            canPickUp = false;
            hasDumpster = false;
            starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (PV.IsMine)
        {
            if (starterAssetsInputs.interact && canPickUp)
            {
                if (!hasDumpster)
                {
                    hasDumpster = true;
                    Dumpster.GetComponent<Rigidbody>().isKinematic = true;
                    Dumpster.transform.parent = gameObject.transform;

                    Dumpster.transform.localPosition = dumpsterPos.transform.localPosition;
                    Dumpster.transform.localRotation = Quaternion.Euler(0f, -90f, 0f);
                    PlaySounds pickdumpster = GetComponent<PlaySounds>();
                    pickdumpster.PlaySound(14);
                }
                else
                {
                    hasDumpster = false;
                    Dumpster.GetComponent<Rigidbody>().isKinematic = false;
                    Dumpster.transform.parent = null;
                }
                starterAssetsInputs.interact = false;
            }

            GetComponent<ThirdPersonShooterController>().enabled = !hasDumpster;

            if (hasDumpster)
            {
                GetComponent<ThirdPersonController>().SetRotateOnMove(false);
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, Camera.main.transform.eulerAngles.y, transform.rotation.eulerAngles.z);
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Dumpster"))
            {
                canPickUp = true;
                Dumpster = other.gameObject;
                UI_Dumpster = Dumpster.transform.GetChild(0).gameObject;
                UI_Dumpster.SetActive(true);
            }
    }
    void OnTriggerExit(Collider other)
    {
            if (other.gameObject.CompareTag("Dumpster"))
            {
                canPickUp = false;
                UI_Dumpster.SetActive(false);
            }
    }
}