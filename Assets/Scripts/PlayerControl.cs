/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {
    public bool canControl = true;
    float h;
    float v;
    [Range(0,4)]
    public float movementSpeed = 4;
	// Use this for initialization
	void Start () 
	{
		
	}

    // Update is called once per frame
    void Update()
    {
        h = CrossPlatformInputManager.GetAxis("Horizontal");
        v = CrossPlatformInputManager.GetAxis("Vertical");

        if (canControl)
        {
            transform.Translate(new Vector3(h, v, 0) * Time.deltaTime * movementSpeed);
        }
    }
}
