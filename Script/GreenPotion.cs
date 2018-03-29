﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenPotion : MonoBehaviour {
	private PlayerHealth playerHealth;
	// Use this for initialization
	void Start () {
		playerHealth = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
	}

	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D playerCollider){
		if (!playerCollider.isTrigger && playerCollider.CompareTag ("Player")) {
			playerHealth.UpHealth (3);
			Destroy (gameObject);
		}
	}
}