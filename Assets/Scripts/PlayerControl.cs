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
    public GameManager gm;
    public bool gotKey = false;
    public bool canControl = true;
    public Rigidbody2D rb;
    float h;
    float v;
    [Range(0,4)]
    public float movementSpeed = 4;
	// Use this for initialization
	void Start () 
	{
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

    // Update is called once per frame
    void Update()
    {
        h = CrossPlatformInputManager.GetAxis("Horizontal");
        v = CrossPlatformInputManager.GetAxis("Vertical");

        if (canControl)
        {
            rb.velocity = new Vector2(Mathf.Lerp(0, (h * movementSpeed), 0.8f), Mathf.Lerp(0, (v * movementSpeed), 0.8f));
        }
        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            gm.GameOver();
        }

        if(col.gameObject.tag == "Key")
        {
            gm.keyUi.enabled = true;
            gotKey = true;
            Destroy(col.gameObject);
        }

        if(col.gameObject.tag == "Door" && gotKey == true)
        {
            gm.NextLevel();
        }
    }
}
