using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameController : MonoBehaviour {
	public string message;
	public float time;

	public GameObject titleHud;
	public GameObject gameHud;

	public GameObject dynamicCamera;
	
	private float _switchTimer = 0;

	public GameObject[] players; 
	private int _pointer = 0;

	public void Update () {
		_switchTimer -= Time.deltaTime;

		if(Input.GetButton("Fire1") && _switchTimer <= 0) {
			_switchTimer = 0.25f;
			foreach(GameObject player in players) {
				player.GetComponent<NavMeshAgent>().Stop();
				player.GetComponent<PlayerController>().enabled = false;
			}
			_pointer = (_pointer + 1) % players.Length;
			players[_pointer].GetComponent<PlayerController>().enabled = true;
		}

		if(Input.GetButton("Cancel")) {
			for(int i = 0; i < players.Length; i++) {
				if(i == _pointer) continue;

				NavMeshAgent agent = players[i].GetComponent<NavMeshAgent>();
				agent.SetDestination(players[_pointer].transform.position);
				agent.Resume();
			}
		}
	}

	public void SetMessage(string message, float time) {
		this.message = message;
		this.time = time;
	}

	public void Begin() {
		titleHud.SetActive(false);
		gameHud.SetActive(true);
		dynamicCamera.GetComponent<CameraController>().cameraLocked = true;
	}
}
