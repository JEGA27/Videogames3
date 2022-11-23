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

    public Color blueColor;
    public Color redColor;

    public Text trashTxt;
    private PickUpTrash pickUpTrash;

    //public Text timerTxt;
    //public float timeInSec;

    public Text ownTeamTxt;
    public Image ownTeamImg;
    private string ownTeamScore;
    public Text enemyTeamTxt;
    public Image enemyTeamImg;
    private string enemyTeamScore;

    public Text ammo;

    public Image specialWeaponProgressCircle;
    public Sprite swBlueCircle;
    public Sprite swRedCircle;

    private ThirdPersonShooterController tpsc;

    // Start is called before the first frame update
    void Start()
    {
        health = player.GetComponent<Health>();
        pickUpTrash = player.GetComponent<PickUpTrash>();
        tpsc = player.GetComponent<ThirdPersonShooterController>();

        int team = (int)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        if (team == 0)
        {
            ammo.color = blueColor;
            specialWeaponProgressCircle.sprite = swBlueCircle;
            ownTeamImg.color = blueColor;
            enemyTeamImg.color = redColor;
            ownTeamScore = "BlueScore";
            enemyTeamScore = "RedScore";
        }
        else
        {
            ammo.color = redColor;
            specialWeaponProgressCircle.sprite = swRedCircle;
            ownTeamImg.color = redColor;
            enemyTeamImg.color = blueColor;
            ownTeamScore = "RedScore";
            enemyTeamScore = "BlueScore";
        }
    }

    // Update is called once per frame
    void Update()
    {
        specialWeaponProgressCircle.fillAmount = (player.GetComponent<ScoreSW>().specialWeaponProgress) / 100.0f;

        hpBar.value = health.hp / GameManager.maxRaccoonHealth;

        trashTxt.text = pickUpTrash.currentTrash.ToString();

        ownTeamTxt.text = ((int)PhotonNetwork.CurrentRoom.CustomProperties[ownTeamScore]).ToString();
        enemyTeamTxt.text = ((int)PhotonNetwork.CurrentRoom.CustomProperties[enemyTeamScore]).ToString();

        ammo.text = tpsc.bulletsLeft.ToString() + "/" + tpsc.magazine.ToString();
    }


}