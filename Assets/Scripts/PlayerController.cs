﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed    = 5;
	public float jump     = 100;
	public float rotation = 0.5f;

	private Vector2 _rotation = new Vector2(0, 0);
	public int _canJump = 0;
	private Rigidbody _rb;

	public void Start () {
		_rb = GetComponent<Rigidbody>();
	}

	public void FixedUpdate() {
		_rotation = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		// Rotate smoothly to the desired angle.
		if(_rotation.magnitude > 0.1) {
			transform.rotation = Quaternion.Slerp(
				transform.rotation,
				Quaternion.Euler(new Vector3(0, (Mathf.PI + Mathf.Atan2(-_rotation.x, -_rotation.y)) * Mathf.Rad2Deg, 0)),
				rotation
			);
		}

		// Minimise strafing
		_rb.AddForce(Vector3.right * _rotation.x * speed * (_canJump > 0 ? 1f : 0.5f));
		_rb.AddForce(Vector3.forward * _rotation.y * speed * (_canJump > 0 ? 1f : 0.5f));

		if(Input.GetButton("Jump") && _canJump > 0) {
			_rb.AddForce(transform.up * jump);
			_rb.AddForce(-transform.forward * jump);
		}
	}

	public void OnCollisionEnter (Collision col) {
		if(col.gameObject.tag == "Jumpable") _canJump++;
	}

	public void OnCollisionExit (Collision col) {
		if(col.gameObject.tag == "Jumpable") _canJump--;
	}
}
