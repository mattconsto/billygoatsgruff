using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum MessageAction {
	NONE = 0, CHOICE = 1, END = 2, KILL = 3, FINISH = 4
}

[System.Serializable]
public class Message {
	public string text = "";
	public AudioClip clip;
	public MessageAction action;
	public Message[] children;
}

public class Dialogue : MonoBehaviour {
	public GameController controller;
	public Message root;

	public void OnTriggerEnter(Collider col) {
		if(col.gameObject.tag == "Player" && !col.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled) controller.BeginDialogue(root);
	}
}
