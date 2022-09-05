using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject player;
    private Health health;
    public Text hpTxt;

    public Text timerTxt;
    public float timeInSec;

    public Text blueScoreTxt;
    public Text redScoreTxt;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        hpTxt.text = "HP: " + health.hp.ToString();
        timeInSec -= Time.deltaTime;
        if (timeInSec >= 0)
        {
            TimeSpan ts = TimeSpan.FromSeconds(timeInSec);
            timerTxt.text = string.Format("{0}", new DateTime(ts.Ticks).ToString("mm:ss"));
        }
        else
        {
            Debug.Log("Game Over!");
        }
        blueScoreTxt.text = GameManager.blueTeamTrash.ToString();
        redScoreTxt.text = GameManager.redTeamTrash.ToString();
    }
}
