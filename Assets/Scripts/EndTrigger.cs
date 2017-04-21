using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour {
	public GameController controller;

	public void OnTriggerEnter() {
		controller.End();
	}
}
