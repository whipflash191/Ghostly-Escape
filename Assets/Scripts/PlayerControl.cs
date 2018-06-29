/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    public bool canControl = true;
    [Range(0,4)]
    public float movementSpeed;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (canControl)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(new Vector3(0, 1, 0) * (Time.deltaTime * movementSpeed));
            } else if(Input.GetKey(KeyCode.S))
            {
                transform.Translate(new Vector3(0, -1, 0) * (Time.deltaTime * movementSpeed));
            } else if(Input.GetKey(KeyCode.A))
            {
                transform.Translate(new Vector3(-1, 0, 0) * (Time.deltaTime * movementSpeed));
            } else if(Input.GetKey(KeyCode.D))
            {
                transform.Translate(new Vector3(1, 0, 0) * (Time.deltaTime * movementSpeed));
            }
        }
	}
}
