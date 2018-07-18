/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour {
    public bool canControl = true;
    public Rigidbody2D rb;
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
            rb.velocity = new Vector2(Mathf.Lerp(0, (h * movementSpeed), 0.8f), Mathf.Lerp(0, (v * movementSpeed), 0.8f));
           // transform.Translate(new Vector3(h, v, 0) * Time.deltaTime * movementSpeed);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
