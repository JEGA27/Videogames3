using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using StarterAssets;
using Photon.Pun;

public class MenuPause : MonoBehaviour
{
    public Text textoPausa;
    public GameObject botonResume;
    public GameObject botonReturn;
    public GameObject crosshair;
    private StarterAssetsInputs starterAssetsInputs;

    PhotonView PV;

    void Awake()
    {
        PV = gameObject.GetComponentInParent<PhotonView>();
        if(PV.IsMine)
        {
            starterAssetsInputs = this.GetComponent<StarterAssetsInputs>();
        }
    }

    void Update()
    {
        if (starterAssetsInputs.pause)
        {
            textoPausa.enabled = true;
            botonResume.SetActive(true);
            botonReturn.SetActive(true);
            crosshair.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        textoPausa.enabled = false;
        botonResume.SetActive(false);
        botonReturn.SetActive(false);
        crosshair.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
