using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class HUD : MonoBehaviour
{
    public GameManager GameManager;

    public GameObject player;
    private Health health;
    public Slider hpBar;

    public Text trashTxt;
    private PickUpTrash pickUpTrash;

    //public Text timerTxt;
    public double timeInSec;

    public Text blueScoreTxt;
    public Text redScoreTxt;

    double incTimer;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<Health>();
        pickUpTrash = player.GetComponent<PickUpTrash>();
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = health.hp / GameManager.maxRaccoonHealth;

        trashTxt.text = pickUpTrash.currentTrash.ToString();

        

        //timeInSec -= Time.deltaTime;

        /*incTimer = PhotonNetwork.Time - double.Parse(PhotonNetwork.CurrentRoom.CustomProperties["StartTime"].ToString());

        timeInSec -= incTimer;


        if (timeInSec >= 0)
        {
            TimeSpan ts = TimeSpan.FromSeconds(timeInSec);
            //timerTxt.text = string.Format("{0}", new DateTime(ts.Ticks).ToString("mm:ss"));
        }
        else
        {
            Debug.Log("Game Over!");
        }*/

        blueScoreTxt.text = GameManager.blueTeamTrash.ToString();
        redScoreTxt.text = GameManager.redTeamTrash.ToString();
    }

  
}
