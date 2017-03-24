using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrigger : MonoBehaviour {
	public float time;
	public float timer = 0;
	public Vector3 degrees;

	private bool moved = false;

	public void FixedUpdate() {
		if(timer > 0) {
			timer -= Time.deltaTime;
			transform.Rotate(Time.deltaTime / time * degrees);
		}
	}

	public void OnCollisionEnter(Collision col) {
		// Start rotating
		if(!moved) {
			timer = time;
			moved = true;
		}
	}
}
