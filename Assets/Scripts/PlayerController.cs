using System.Collections;
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
		// Rotate smoothly to the desired angle.
		transform.rotation = Quaternion.Slerp(
			transform.rotation,
			Quaternion.Euler(new Vector3(0, (Mathf.PI + Mathf.Atan2(-_rotation.x, _rotation.y)) * Mathf.Rad2Deg, 0)),
			rotation
		);

		_rb.AddForce(Vector3.right * -_rotation.x * speed);
		_rb.AddForce(Vector3.forward * _rotation.y * speed);
	}

	public void OnMoveHorizontal(float value) {
		if(value != 0) _rotation.x = value;
	}

	public void OnMoveVertical(float value) {
		if(value != 0) _rotation.y = value;
	}

	public void OnJump(float value) {
		if (value > 0 && _canJump > 0) _rb.AddForce(transform.up * jump);
	}

	public void OnCollisionEnter (Collision col) {
		if(col.gameObject.tag == "Jumpable") _canJump++;
	}

	public void OnCollisionExit (Collision col) {
		if(col.gameObject.tag == "Jumpable") _canJump--;
	}
}
