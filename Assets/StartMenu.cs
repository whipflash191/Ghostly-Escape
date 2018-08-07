/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public Slider master;
    public Slider footstep;
    public Slider background;
    public Dropdown quality;
    public Animator pressHere;
    public GameObject settings;
    public GameObject menu;
    public AudioMixer mixer;
	// Use this for initialization
	void Start () 
	{
        if(PlayerPrefs.GetString("CustomSettings") == "true")
        {
            LoadSettings();
        }
        pressHere.speed = 0.4f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
    
    public void RevealSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
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

    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("masterVolume", volume);
    }

    public void SetFootstepVolume(float volume)
    {
        mixer.SetFloat("footstepVolume", volume);
    }

    public void SetBackgroundVolume(float volume)
    {
        mixer.SetFloat("bgVolume", volume);
    }

    public void SaveSettings()
    {
        settings.SetActive(false);
        menu.SetActive(true);
        PlayerPrefs.SetString("CustomSettings", "true");
        PlayerPrefs.SetFloat("MasterVol", master.value);
        PlayerPrefs.SetFloat("BackgroudVol", background.value);
        PlayerPrefs.SetFloat("FootstepVol", footstep.value);
        PlayerPrefs.SetInt("Quality", quality.value);
    }

    public void LoadSettings()
    {
        PlayerPrefs.GetString("CustomSettings");
        PlayerPrefs.GetFloat("MasterVol");
        PlayerPrefs.GetFloat("BackgroudVol");
        PlayerPrefs.GetFloat("FootstepVol");
        PlayerPrefs.GetInt("Quality");
    }

}
