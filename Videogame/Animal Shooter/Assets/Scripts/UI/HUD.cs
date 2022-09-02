using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public Text timerTxt;
    public float timeInSec;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
