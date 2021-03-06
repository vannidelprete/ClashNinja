﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AI dell'arciere scheletro

public class EnemyBehaviour : MonoBehaviour {
	private GameObject player;
	private Animator animEnemy;
	private float speedEnemy = 2f;
	private float distToPlayer = 6f;
	private Vector3 playerPosition;
	public bool isEnemyAttacking;
	public bool isDead;
	private Rigidbody2D EnemyRb2D;

	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");
		animEnemy = GetComponent<Animator> ();
		EnemyRb2D = GetComponent<Rigidbody2D> ();
	}

	void Start () {
		isEnemyAttacking = false;
		isDead = false;
	}


	public void isDeadEnemy(bool deadEvent){
		isDead = deadEvent;
		//indica al mecanim se l'arciere è stato sconfitto
		animEnemy.SetBool ("EnemyDeads", deadEvent);
	}


	void Update () {
		if (player != null) {
			//rileva la posizione del giocatore e lo segue
			if (!isDead) {
				playerPosition = player.transform.position;
				if (!isEnemyAttacking) {
					if (gameObject.transform.position.x > playerPosition.x) {
						EnemyRb2D.velocity = new Vector2 (-speedEnemy, 0);
						gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
						animEnemy.Play ("Walk");
					} else if (gameObject.transform.position.x < playerPosition.x) {
						EnemyRb2D.velocity = new Vector2 (speedEnemy, 0);
						gameObject.transform.rotation = Quaternion.Euler (0, 180, 0);
						animEnemy.Play ("Walk");
					}
				}
				//se si avvicina ad una distanza buona per scoccare la freccia, spara
				if (Mathf.Abs (gameObject.transform.position.x - playerPosition.x) < distToPlayer &&
				   gameObject.transform.position.x - playerPosition.x > 0) {
					gameObject.transform.rotation = Quaternion.Euler (0, 0, 0);
					isEnemyAttacking = true;
				} else if (Mathf.Abs (gameObject.transform.position.x - playerPosition.x) < distToPlayer &&
				          gameObject.transform.position.x - playerPosition.x < 0) {
					gameObject.transform.rotation = Quaternion.Euler (0, 180, 0);
					isEnemyAttacking = true;
				} else {
					isEnemyAttacking = false;
				}
				//comunica al mecanim se l'arciere sta attaccando
				animEnemy.SetBool ("EnemyAttack", isEnemyAttacking);
			} else {
				//in caso di sconfitta del nemico, distrugge l'oggetto
				Destroy (gameObject, 0.517f);
			}
		}
	}
}
