using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float speed = 5;

	private Rigidbody _rb;

	public void Start () {
		_rb = GetComponent<Rigidbody>();
	}

	public void OnMoveHorizontal(float value) {
		_rb.AddForce(-transform.right * value * speed);
	}

	public void OnMoveVertical(float value) {
		_rb.AddForce(transform.forward * value * speed);
	}
}
