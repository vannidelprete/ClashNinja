﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Gestione delle tempistiche di attacco dell'arciere scheletro

public class EnemyFiring : MonoBehaviour {
	private Vector2 fireDirection;
	private GameObject player;
	private EnemyBehaviour enemyBehaviour;
	public GameObject enemyArrow;
	private Vector3 arrowRotation;
	private float timePassedSinceLast;
	private float delayFire = 0.933f;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemyBehaviour = GetComponentInParent<EnemyBehaviour> ();
	}

	void Start () {
		timePassedSinceLast = delayFire;
	}

	void Update () {
		if (player != null) {
			if (!enemyBehaviour.isDead) {
				//sincronizzazione dell'attacco con la clip di attacco del mecanim
				if (enemyBehaviour.isEnemyAttacking && timePassedSinceLast >= delayFire) {
					GameObject arrow = GameObject.Instantiate (enemyArrow);
					arrow.transform.position = transform.position;
					Vector2 direction = player.transform.position - arrow.transform.position;
					arrow.GetComponent<EnemyArrow> ().setArrowDirection (direction);
					timePassedSinceLast = 0f;
				}	
				if (!enemyBehaviour.isEnemyAttacking) {
					timePassedSinceLast = 0f;
				}
				if (timePassedSinceLast < delayFire) {
					timePassedSinceLast += Time.deltaTime;
				}
			}
		}
	}
}
