/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    /* 
     * Controls the pause menu
     * Stops Gametime when "Paused"
     * Has a copy of the 'Settings menu' from the "StartMenu" Script
     */
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public string MenuScene;
    public Slider master;
    public Slider footstep;
    public Slider background;
    public Dropdown quality;
    public GameObject settings;
    public AudioMixer mixer;

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeGame()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    public void Menu()
    {
        SceneManager.LoadScene(MenuScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SettingsMenu()
    {

        if (PlayerPrefs.GetString("CustomSettings") == "true")
        {
            LoadSettings();
        }
        pauseMenuUI.SetActive(false);
        settings.SetActive(true);
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
        pauseMenuUI.SetActive(true);
        PlayerPrefs.SetString("CustomSettings", "true");
        PlayerPrefs.SetFloat("MasterVol", master.value);
        PlayerPrefs.SetFloat("BackgroudVol", background.value);
        PlayerPrefs.SetFloat("FootstepVol", footstep.value);
        PlayerPrefs.SetInt("Quality", quality.value);
    }

    public void LoadSettings()
    {
       master.value = PlayerPrefs.GetFloat("MasterVol");
       background.value = PlayerPrefs.GetFloat("BackgroudVol");
       footstep.value = PlayerPrefs.GetFloat("FootstepVol");
       quality.value =  PlayerPrefs.GetInt("Quality");
    }

}
