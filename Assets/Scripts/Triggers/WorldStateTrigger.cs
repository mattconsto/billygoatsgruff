using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateTrigger : MonoBehaviour {
	public WorldStateMachine machine;
	public int state = 0;

	public void OnTriggerEnter(Collider col) {
		// Only trigger if it's the current player
		if(col.gameObject.tag == "Player" && !col.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled) machine.Change(state);
	}
}
