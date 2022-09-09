using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeFilter;
    public Toggle mute;
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject levelselectPanel;

    public void OpenPanel( GameObject panel) {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelselectPanel.SetActive(false);

        panel.SetActive(true);
    }

    public void PlayLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
