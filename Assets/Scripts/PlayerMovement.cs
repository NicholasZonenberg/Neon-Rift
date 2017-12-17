﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float speed;
	public float invincibleTime = 2.0f;		// After being hit, time the player would have invincibility for, in seconds

	private Spawning spawning;
	private float invincibilityTimer;		// Amount of time player has had invincibility.
	private bool isInvincible;				// Is the player currently invincible?

	private AudioSource hurtSFX;

    public bool canShoot;

	// Use this for initialization
	void Start ()
	{
		spawning = GameObject.Find("Game Manager").GetComponent<Spawning>();
		invincibilityTimer = 0.0f;
		isInvincible = false;

		hurtSFX = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        canShoot = GetComponent<PlayerShoot>().canShoot;

        if (canShoot == false)
        {
            speed = 7.0f;
        }
        else
        {
            speed = 10.0f;
        }

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
			transform.Translate(new Vector3(0.0f, speed, 0.0f) * Time.deltaTime);

			if(Camera.main.WorldToViewportPoint(transform.position).y > 0.8)
			{
				transform.Translate(new Vector3(0.0f, -speed, 0.0f) * Time.deltaTime);
			}
        }
		else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
			transform.Translate(new Vector3(0.0f, -speed, 0.0f) * Time.deltaTime);

			if(Camera.main.WorldToViewportPoint(transform.position).y < 0.2)
			{
				transform.Translate(new Vector3(0.0f, speed, 0.0f) * Time.deltaTime);
			}
        }

		if(isInvincible)
		{
			invincibilityTimer += Time.deltaTime;
			if(invincibilityTimer >= invincibleTime)
			{
				isInvincible = false;
				invincibilityTimer = 0.0f;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.name.Substring(0, 5) == "Enemy" && !isInvincible)
		{
			// Replace this with whatever should happen when the player gets hit by an enemy!
			if(!spawning.playerHit())
			{
				SceneManager.LoadScene("Game Over");
			}
			isInvincible = true;

			hurtSFX.Play();
		}
	}
}
