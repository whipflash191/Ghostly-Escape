/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    /*
     * This Script Controls the Start Menu
     * This includes a 'Settings' menu
     */
    public Animator sceneTransition;
    public string FirstLevelToLoad;
    public Slider master;
    public Slider footstep;
    public Slider background;
    public Dropdown quality;
    public Animator pressHere;
    public GameObject settings;
    public GameObject menu;
    public GameObject credits;
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
    
    public void RevealSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void RevealMenu()
    {
        if(credits.activeInHierarchy == true )
        {
            credits.SetActive(false);
        }
        pressHere.gameObject.SetActive(false);
        menu.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(StartGameTransition());
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

    public void LoadCredits()
    {
        menu.SetActive(false);
        credits.SetActive(true);
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
        master.value = PlayerPrefs.GetFloat("MasterVol");
        mixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("MasterVol"));
        background.value = PlayerPrefs.GetFloat("BackgroudVol");
        mixer.SetFloat("bgVolume", PlayerPrefs.GetFloat("BackgroudVol"));
        footstep.value = PlayerPrefs.GetFloat("FootstepVol");
        mixer.SetFloat("footstepVolume", PlayerPrefs.GetFloat("FootstepVol"));
        quality.value = PlayerPrefs.GetInt("Quality");
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("Quality"));
    }

    IEnumerator StartGameTransition()
    {
        sceneTransition.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(FirstLevelToLoad);
    }
}
