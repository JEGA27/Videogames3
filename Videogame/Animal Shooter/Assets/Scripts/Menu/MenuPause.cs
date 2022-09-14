using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public Text textoPausa;
    public GameObject botonResume;
    public GameObject botonReturn;
    
    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            textoPausa.enabled = true;
            botonResume.SetActive(true);
            botonReturn.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        textoPausa.enabled = false;
        botonResume.SetActive(false);
        botonReturn.SetActive(false);
    }

    public void ExitGame() {
        SceneManager.LoadScene(0);
    }
}
