using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

	private bool _started = false;
	private bool _canceled = false;

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

		if(Input.GetButton("Fire3")) {
			for(int i = 0; i < players.Length; i++) {
				if(i == _pointer) continue;

				NavMeshAgent agent = players[i].GetComponent<NavMeshAgent>();
				agent.SetDestination(players[_pointer].transform.position);
				agent.Resume();
			}
		}

		if(_started && Input.GetButton("Cancel")) {
			if(!_canceled) {
				Time.timeScale = Time.timeScale > 0 ? 0 : 1;
				titleHud.transform.Find("Menu/Begin").GetComponent<Text>().text = "Resume";
				titleHud.SetActive(!titleHud.activeSelf);
				gameHud.SetActive(!gameHud.activeSelf);
				titleHud.transform.Find("Vingette").gameObject.SetActive(!titleHud.transform.Find("Vingette").gameObject.activeSelf);
			}
			_canceled = true;
		} else {
			_canceled = false;
		}
	}

	public void SetMessage(string message, float time) {
		this.message = message;
		this.time = time;
	}

	public void Begin() {
		_started = true;

		titleHud.SetActive(false);
		gameHud.SetActive(true);
		titleHud.transform.Find("Vingette").gameObject.SetActive(false);
		dynamicCamera.GetComponent<CameraController>().cameraLocked = true;
	}
}
