using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public string message;
	public float time;

	public GameObject titleHud;
	public GameObject gameHud;

	public GameObject camera;
	
	public void Update () {
		
	}

	public void SetMessage(string message, float time) {
		this.message = message;
		this.time = time;
	}

	public void Begin() {
		titleHud.SetActive(false);
		gameHud.SetActive(true);
		camera.GetComponent<CameraController>().cameraLocked = true;
	}
}
