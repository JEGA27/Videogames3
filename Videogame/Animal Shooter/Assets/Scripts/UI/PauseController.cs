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
    
    [Space]
    public GameObject OptionsPanel;
    public GameObject OptionsBtnsPanel;
    public GameObject OptionsSettsPanel;
    public GameObject ScoreboardPanel;
    public GameObject MapPanel;

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

            currentCanvasTab = 0;

            OptionsPanel.SetActive(false);
            ScoreboardPanel.SetActive(false);
            MapPanel.SetActive(false);

            OptionsBtnsPanel.SetActive(true);
            OptionsSettsPanel.SetActive(false);
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
                if (Input.GetKeyDown("right"))
                {
                    if (currentCanvasTab == 2)
                    {
                        currentCanvasTab = 0;
                    }
                    else
                    {
                        currentCanvasTab += 1;
                    }
                }

                if (Input.GetKeyDown("left"))
                {
                    if (currentCanvasTab == 0)
                    {
                        currentCanvasTab = 2;
                    }
                    else
                    {
                        currentCanvasTab -= 1;
                    }
                }
            }

            switch(currentCanvasTab)
            {
                case 0:
                    OptionsPanel.SetActive(true);
                    ScoreboardPanel.SetActive(false);
                    MapPanel.SetActive(false);
                    break;
                case 1:
                    OptionsPanel.SetActive(false);
                    ScoreboardPanel.SetActive(true);
                    MapPanel.SetActive(false);

                    OptionsBtnsPanel.SetActive(true);
                    OptionsSettsPanel.SetActive(false);
                    break;
                case 2:
                    OptionsPanel.SetActive(false);
                    ScoreboardPanel.SetActive(false);
                    MapPanel.SetActive(true);

                    OptionsBtnsPanel.SetActive(true);
                    OptionsSettsPanel.SetActive(false);
                    break;
                default:
                    OptionsPanel.SetActive(true);
                    ScoreboardPanel.SetActive(false);
                    MapPanel.SetActive(false);
                    break;
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
        OptionsBtnsPanel.SetActive(false);
        OptionsSettsPanel.SetActive(true);
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
