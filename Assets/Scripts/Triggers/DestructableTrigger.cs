using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableTrigger : MonoBehaviour {
	public GameObject[] objects;
	public float impulse;

	public void OnCollisionEnter(Collision col) {
		if((objects != null || System.Array.IndexOf(objects, col.gameObject) != -1) && col.impulse.magnitude >= impulse) {
			gameObject.SetActive(false);
		}
	}
}
