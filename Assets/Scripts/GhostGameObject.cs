/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostGameObject : MonoBehaviour {
    public NewGhostCharacter ghost;
    public bool hasSetPlayer = false;
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
        if(hasSetPlayer == false)
        {
            gameObject.AddComponent(typeof(PlayerControl));
            gameObject.AddComponent(typeof(AudioListener));
            hasSetPlayer = true;
        } else
        {
            Debug.Log("Ghost is Already Player!");
        }
    }

    public void SetSprite()
    {
        SpriteRenderer attachedRenderer = GetComponent<SpriteRenderer>();
        attachedRenderer.sprite = ghost.ghostSprite;
    }
}
