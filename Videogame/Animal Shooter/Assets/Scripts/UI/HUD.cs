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
    public Slider hpBar;

    public Text trashTxt;
    private PickUpTrash pickUpTrash;

    //public Text timerTxt;
    //public float timeInSec;

    public Text blueScoreTxt;
    public Text redScoreTxt;

    public Text ammo;

    public Image specialWeaponProgressCircle;
    private ThirdPersonShooterController tpsc;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<Health>();
        pickUpTrash = player.GetComponent<PickUpTrash>();
        tpsc = player.GetComponent<ThirdPersonShooterController>();
    }

    // Update is called once per frame
    void Update()
    {
        specialWeaponProgressCircle.fillAmount = (player.GetComponent<ScoreSW>().specialWeaponPoints) / 100.0f;

        hpBar.value = health.hp / GameManager.maxRaccoonHealth;

        trashTxt.text = pickUpTrash.currentTrash.ToString();

        /*timeInSec -= Time.deltaTime;
        if (timeInSec >= 0)
        {
            TimeSpan ts = TimeSpan.FromSeconds(timeInSec);
            timerTxt.text = string.Format("{0}", new DateTime(ts.Ticks).ToString("mm:ss"));
        }
        else
        {
            Debug.Log("Game Over!");
        }
        */

        blueScoreTxt.text = GameManager.blueTeamTrash.ToString();
        redScoreTxt.text = GameManager.redTeamTrash.ToString();

        ammo.text = tpsc.bulletsLeft.ToString() + "/" + tpsc.magazine.ToString();
    }


}