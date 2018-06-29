/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGameObject : MonoBehaviour {
    public NewGhostCharacter ghost;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    public void MakePlayer()
    {
        gameObject.AddComponent(typeof(PlayerControl));
    }
}
