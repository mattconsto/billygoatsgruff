using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableTrigger : MonoBehaviour {
	public void OnCollisionEnter(Collision col) {
		gameObject.SetActive(false);
	}
}
