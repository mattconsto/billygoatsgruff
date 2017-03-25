using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameController : MonoBehaviour {
	public string message;
	public string hint;
	public float time;

	public GameObject titleHud;
	public GameObject gameHud;

	public GameObject dynamicCamera;
	
	private float _switchTimer = 0;
	private float _pathfindTimer = 0;
	private float _messageTimer = 0;
	private float _hintTimer = 0;
	private bool _messageVisible = false;
	private bool _hintVisible = false;

	public GameObject[] players; 
	private int _pointer = 0;

	private bool _started = false;
	private bool _canceled = false;

	public Text hintText;
	public Text messageText; 
	public AudioSource narrationSource;

	public void Update() {
		_pathfindTimer -= Time.deltaTime;
		_switchTimer -= Time.deltaTime;
		_hintTimer -= Time.deltaTime;
		_messageTimer -= Time.deltaTime;

		if(_hintVisible && _hintTimer <= 0) {
			_hintVisible = false;
			hintText.text = "";
		}

		if(_messageVisible && _messageTimer <= 0) {
			_messageVisible = false;
			messageText.text = "";
		}

		if(Input.GetButton("Fire1") && _switchTimer <= 0) {
			_switchTimer = 0.25f;
			foreach(GameObject player in players) {
				// if(player.activeSelf) {
					player.GetComponent<NavMeshAgent>().enabled = true;
					player.GetComponent<NavMeshAgent>().Stop();
					player.GetComponent<PlayerController>().runUpdates = false;
				// }
			}
			_pointer = (_pointer + 1) % players.Length;
			players[_pointer].GetComponent<NavMeshAgent>().enabled = false;
			players[_pointer].GetComponent<PlayerController>().runUpdates = true;
		}

		if(_pathfindTimer <= 0 && Input.GetButton("Fire3")) {
			_pathfindTimer = 0.5f; // Twice a second is enough
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
		messageText.text = message;
		_messageTimer = time;
		_messageVisible = true;
	}

	public void SetHint(string hint, float time) {
		this.hint = hint;
		this.time = time;
		hintText.text = hint;
		_hintTimer = time;
		_hintVisible = true;
	}

	public void PlayNaration(AudioClip clip) {
		narrationSource.PlayOneShot(clip, 1);
	}

	public void Begin() {
		_started = true;

		titleHud.SetActive(false);
		gameHud.SetActive(true);
		titleHud.transform.Find("Vingette").gameObject.SetActive(false);
		dynamicCamera.GetComponent<CameraController>().cameraLocked = true;
	}
}
