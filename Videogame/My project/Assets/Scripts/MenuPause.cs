using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public Text textoPausa;
    public GameObject botonResume;
    
    void Update () 
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            textoPausa.enabled = true;
            botonResume.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        textoPausa.enabled = false;
        botonResume.SetActive(false);
    }
}
