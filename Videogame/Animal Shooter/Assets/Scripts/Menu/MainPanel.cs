using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour
{
    [Header("Options")]
    public Slider volumeFX;
    public Slider volumeFilter;
    public Toggle mute;
    public AudioMixer Mixer;
    public AudioSource FxSource;
    public AudioClip clicSound;
    private float lastVolume;
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject optionsPanel;
    public GameObject levelselectPanel;

    public void OpenPanel( GameObject panel) {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(false);
        levelselectPanel.SetActive(false);

        panel.SetActive(true);
        PlaySoundButton();
    }

    private void Awake() {
        volumeFX.onValueChanged.AddListener(ChangeVolumeFx);
        volumeFilter.onValueChanged.AddListener(ChangeVolumeMaster);
    }

    public void setMute() {
        if(mute.isOn) {
            Mixer.GetFloat("VolMaster", out lastVolume);
            Mixer.SetFloat("VolMaster", -80);
        }
        else 
            Mixer.SetFloat("VolMaster", lastVolume);
    }

    public void ChangeVolumeMaster (float x) {
        Mixer.SetFloat("VolMaster", x);
    }
    public void ChangeVolumeFx (float x) {
        Mixer.SetFloat("VolFX", x);
    }

    public void PlaySoundButton() {
        FxSource.PlayOneShot(clicSound);
    }

    public void PlayLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void ExitGame() {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }
}
