using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject buttonPause;
    [SerializeField] private GameObject pauseMenu;
    private bool pausedGame = false;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(pausedGame) {
                Resume();
            }
            else {
                Pause();
            }
        }
    }

    public void Pause() {
        pausedGame = true;
        Time.timeScale = 0f;
        buttonPause.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void Resume() {
        pausedGame = false;
        Time.timeScale = 1f;
        buttonPause.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Close() {
        Debug.Log("Cerrando juego");
        Application.Quit();
    } 
}
