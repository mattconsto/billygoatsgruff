using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateTrigger : MonoBehaviour {
	public WorldStateMachine machine;
	public int state = 0;

	public void OnTriggerEnter() {
		machine.Change(state);
	}
}
