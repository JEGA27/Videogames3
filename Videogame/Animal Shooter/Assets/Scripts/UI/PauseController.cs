using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using StarterAssets;
using Photon.Pun;

public class PauseController : MonoBehaviour
{

    public GameObject PauseCanvas;
    public GameObject HUDCanvas;

    private int currentCanvasTab;

    private StarterAssetsInputs starterAssetsInputs;

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine)
        {
            PauseCanvas.SetActive(false);
            HUDCanvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(PV.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (HUDCanvas.active)
                {
                    PauseCanvas.SetActive(true);
                    HUDCanvas.SetActive(false);
                    currentCanvasTab = 0;
                    PlaySounds menupause = GetComponent<PlaySounds>();
                    menupause.PlaySound(1);
                }
                else
                {
                    PauseCanvas.SetActive(false);
                    HUDCanvas.SetActive(true);
                }

            }

            if (PauseCanvas.active)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    LeaveGame();
                }
            }

        }
    }

    public void ShowOptions()
    {
        currentCanvasTab = 0;
        PlaySounds pause = GetComponent<PlaySounds>();
        pause.PlaySound(13);
    }

    public void ShowSettingsOptions()
    {
        //OptionsBtnsPanel.SetActive(false);
        // OptionsSettsPanel.SetActive(true);
        PlaySounds pause = GetComponent<PlaySounds>();
        pause.PlaySound(13);
    }

    public void ShowScoreboard()
    {
        currentCanvasTab = 1;
        PlaySounds pause = GetComponent<PlaySounds>();
        pause.PlaySound(13);
    }

    public void ShowMap()
    {
        currentCanvasTab = 2;
        PlaySounds pause = GetComponent<PlaySounds>();
        pause.PlaySound(13);
    }

    public void Resume()
    {
        PauseCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
        PlaySounds pause = GetComponent<PlaySounds>();
        pause.PlaySound(13);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("Main Menu");
        PlaySounds pause = GetComponent<PlaySounds>();
        pause.PlaySound(13);
    }
}
