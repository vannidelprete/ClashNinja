﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script che gestisce il movimento e le collisioni della freccia

public class EnemyArrow : MonoBehaviour {
	private float arrowSpeed = 10f;
	private Vector2 arrowDirection;
	private Vector2 arrowPosition;
	private Vector3 arrowRotation;
	private Vector2 upToView;
	private Rigidbody2D rbArrow;

	void Awake(){
		//prendo il punto della visuale in alto a destra
		upToView = Camera.main.ViewportToWorldPoint (new Vector2(1, 1)); 
		rbArrow = gameObject.GetComponent<Rigidbody2D> ();
	}

	//costruzione del versore e degli angoli di rotazione
	public void setArrowDirection(Vector2 direction){
		arrowDirection = direction.normalized;
		arrowRotation = new Vector3 (0, 0, Mathf.Atan2 (direction.x, -direction.y)*Mathf.Rad2Deg);
	}
	

	void Update () {
		transform.rotation = Quaternion.Euler (arrowRotation);
		rbArrow.velocity = arrowDirection * arrowSpeed;
		if (transform.position.y > upToView.y) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		//se colpisce il giocatore lo danneggia e poi si distrugge
		if(!other.isTrigger && other.CompareTag("Player")){
			other.SendMessageUpwards ("Damage", 1);
		}
		Destroy (gameObject);
	}
}
