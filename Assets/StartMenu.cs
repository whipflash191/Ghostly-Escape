/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {
    public Animator pressHere;
    public GameObject menu;
	// Use this for initialization
	void Start () 
	{
        pressHere.speed = 0.4f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

   public void RevealMenu()
    {
        menu.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
