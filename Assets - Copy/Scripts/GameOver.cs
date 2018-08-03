/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class GameOver : MonoBehaviour 
{

	void Start () 
	{
		
	}
	
	void Update () 
	{
		if(CrossPlatformInputManager.GetButtonDown("Restart"))
        {
            Debug.Log("done");
            SceneManager.LoadScene("Main");
        }
	}
}
