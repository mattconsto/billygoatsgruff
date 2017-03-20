using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public string message;
	public float time;

	public GameObject titleHud;
	public GameObject gameHud;

	public GameObject titleCamera;
	public GameObject gameCamera;
	
	public void Update () {
		
	}

	public void SetMessage(string message, float time) {
		message = message;
		time = time;
	}
}
