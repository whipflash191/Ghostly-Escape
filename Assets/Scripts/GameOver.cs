/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour 
{
    /*
     * Controls the GameOver Scene
     * Loads Different Pannels depending on new highscore
     */
    public GameObject gameOver;
    public GameObject newHighScore;
    public Text gameOverText;
    public Text newHighScoreText;
    public Text initialsText;
    public InputField initialsBox;
	void Start () 
	{
		if(GameManager.newHighscore == true)
        {
            newHighScore.SetActive(true);
            FormatTime(newHighScoreText);
        } else
        {
            gameOver.SetActive(true);
            FormatTime(gameOverText);
            initialsText.text = PlayerPrefs.GetString("HighScoreInitial");
        }
	}
	
	void Update () 
	{
        
	}

    public void FormatTime(Text timerText)
    {
        /*
         * Used to Format Highscore Float into a time format of 00:00:00
         */
        float temp = PlayerPrefs.GetFloat("Highscore");
        int hours = (((int)temp / 60) / 60);
        int minutes = ((int)temp / 60);
        string hoursString;
        string minutesString;
        string secondsString = (temp % 60).ToString("f2");

        if (hours < 10)
        {
            hoursString = ("0" + hours.ToString());
        }
        else
        {
            hoursString = hours.ToString();
        }
        if (minutes < 10)
        {
            minutesString = ("0" + minutes.ToString());
        }
        else
        {
            minutesString = minutes.ToString();
        }

        timerText.text = (hoursString + ":" + minutesString + ":" + secondsString);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("StartMenu");
        GameManager.newHighscore = false;
    }

    public void SaveInitials()
    {
        PlayerPrefs.SetString("HighScoreInitial", initialsBox.text);
        newHighScore.SetActive(false);
        gameOver.SetActive(true);
    }
}
