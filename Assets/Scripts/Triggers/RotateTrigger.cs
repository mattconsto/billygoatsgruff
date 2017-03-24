using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RotateTrigger : MonoBehaviour {
	public float time;
	public float timer = 0;
	public Vector3 degrees;

	public GameObject[] objects;
	public float impulse;

	private bool moved = false;

	public void FixedUpdate() {
		if(timer > 0) {
			timer -= Time.deltaTime;
			transform.Rotate(Time.deltaTime / time * degrees);
		}
	}

	public void OnCollisionEnter(Collision col) {
		// Start rotating
		if(!moved && (objects != null || ArrayUtility.Contains(objects, col.gameObject)) && col.impulse.magnitude >= impulse) {
			timer = time;
			moved = true;
		}
	}
}
