using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {
	public void OnCollisionEnter(Collision col) {
		col.gameObject.SetActive(false);
	}
}
