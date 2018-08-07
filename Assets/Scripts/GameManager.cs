/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour 
{
    public static float timerTime;
    public Image keyUi;
    public Text timerText;
    float startTime;

	void Start () 
	{
        keyUi.enabled = false;
        startTime = Time.time;
	}
	
	void Update () 
	{
        LevelTimer();
	}

    public void LevelTimer()
    {
        timerTime = Time.time - startTime;
        int hours = (((int)timerTime / 60) / 60);
        int minutes = ((int)timerTime / 60);
        string hoursString;
        string minutesString;
        string secondsString = (timerTime % 60).ToString("f2");
        
        if(hours < 10)
        {
            hoursString = ("0" + hours.ToString());
        } else
        {
            hoursString = hours.ToString();
        } 
        if(minutes < 10)
        {
            minutesString = ("0" + minutes.ToString());
        } else
        {
            minutesString = minutes.ToString();
        }

        timerText.text = (hoursString + ":" + minutesString + ":" + secondsString);
    }
}
