using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using StarterAssets;

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

    void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (HUDCanvas.active)
            {
                PauseCanvas.SetActive(true);
                HUDCanvas.SetActive(false);
                currentCanvasTab = 0;
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

    public void ShowOptions()
    {
        currentCanvasTab = 0;
    }

    public void ShowSettingsOptions()
    {
        OptionsBtnsPanel.SetActive(false);
        OptionsSettsPanel.SetActive(true);
    }

    public void ShowScoreboard()
    {
        currentCanvasTab = 1;
    }

    public void ShowMap()
    {
        currentCanvasTab = 2;
    }

    public void Resume()
    {
        PauseCanvas.SetActive(false);
        HUDCanvas.SetActive(true);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
