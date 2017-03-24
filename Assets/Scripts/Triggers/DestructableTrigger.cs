using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DestructableTrigger : MonoBehaviour {
	public GameObject[] objects;
	public float impulse;

	public void OnCollisionEnter(Collision col) {
		if((objects != null || ArrayUtility.Contains(objects, col.gameObject)) && col.impulse.magnitude >= impulse) {
			gameObject.SetActive(false);
		}
	}
}
