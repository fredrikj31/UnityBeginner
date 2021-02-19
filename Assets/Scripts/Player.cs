﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private Transform groundCheckTransform = null;
	[SerializeField] private LayerMask playerMask;

	private bool jumpKeyWasPressed;
	private float horizontalInput;
	private Rigidbody rigidBodyComponent;
	private int superJumpsRemaining;

	// Start is called before the first frame update
	void Start() {
		rigidBodyComponent = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {
		// Check if space key is pressed down
		if (Input.GetKeyDown(key: KeyCode.Space) == true) {
			jumpKeyWasPressed = true;
		}

		horizontalInput = Input.GetAxis("Horizontal");
	}

	// FixedUpdate is called once every physic update
	void FixedUpdate() {
		rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0);

		if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
			return;
		}

		if (jumpKeyWasPressed == true) {
			float jumpPower = 5f;
			if (superJumpsRemaining > 0) {
				jumpPower *= 2;
				superJumpsRemaining--;
			}

			rigidBodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
			jumpKeyWasPressed = false;
		}
	}

	private void OnTriggerEnter(Collider other) {

		if (other.gameObject.layer == 9) {
			Destroy(other.gameObject);
			superJumpsRemaining++;
		}
	}
}
