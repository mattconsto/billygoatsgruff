using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class GameController : MonoBehaviour {
	public enum State {MENU, GAME, PAUSE, DIALOGUE, END}
	public State state = State.MENU;

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
	public int _pointer = 0;

	private bool _canceled = false;

	public Text hintText;
	public Text messageText; 
	public AudioSource narrationSource;

	public Message dialogue;
	public float _dialogueTimer = 0;
	public bool _dialogueTimed = false;

	public RawImage vingette;
	private float _vingetteTarget = 0;

	private bool[] _autofollow;

	public void Start() {
		// Just in case
		titleHud.SetActive(true);
		gameHud.SetActive(false);

		_autofollow = new bool[players.Length];

		_switchTimer = 0.25f;
		// foreach(GameObject player in players) {
		// 	// if(player.activeSelf) {
		// 		player.GetComponent<NavMeshAgent>().enabled = true;
		// 		player.GetComponent<NavMeshAgent>().isStopped = true;
		// 		player.GetComponent<PlayerController>().enabled = false;
		// 	// }
		// }
		// players[_pointer].GetComponent<NavMeshAgent>().enabled = false;
		// players[_pointer].GetComponent<PlayerController>().enabled = true;
	}

	public void FixedUpdate() {
		_pathfindTimer -= Time.deltaTime;
		_switchTimer -= Time.deltaTime;
		_hintTimer -= Time.deltaTime;
		_messageTimer -= Time.deltaTime;

		vingette.color = new Color(1f, 1f, 1f, Mathf.Lerp(vingette.color.a, _vingetteTarget, 0.1f));

		if(_hintVisible && _hintTimer <= 0) {
			_hintVisible = false;
			hintText.text = "";
		}

		if(_messageVisible && _messageTimer <= 0) {
			_messageVisible = false;
			messageText.text = "";
		}

		if(_dialogueTimed) {
			if(_dialogueTimer <= 0) {
				_dialogueTimed = false;
				SetDialogue(dialogue.children.Length > 0 ? dialogue.children[0] : null);
			} else {
				_dialogueTimer -= Time.deltaTime;
			}
		}

		if(state == State.GAME && Input.GetButton("Fire1") && _switchTimer <= 0) {
			_switchTimer = 0.25f;
			int i = 0;
			foreach(GameObject player in players) {
				if(player.activeSelf && (player.GetComponent<PlayerController>().enabled || player.GetComponent<NavMeshAgent>().enabled)) {
					player.GetComponent<NavMeshAgent>().enabled = true;
					player.GetComponent<NavMeshAgent>().isStopped = true;
					player.GetComponent<PlayerController>().enabled = false;
				}
				_autofollow[i++] = false;
			}

			for(int j = 0; j < players.Length; j++) {
				int k = (_pointer + 1 + j + players.Length) % players.Length;
				if(players[k].activeSelf && (players[k].GetComponent<PlayerController>().enabled || players[k].GetComponent<NavMeshAgent>().enabled)) {
					_pointer = k;
					players[_pointer].GetComponent<NavMeshAgent>().enabled = false;
					players[_pointer].GetComponent<PlayerController>().enabled = true;
				}
			}
		}

		if(state == State.GAME && Input.GetButton("Fire2") && _switchTimer <= 0) {
			_switchTimer = 0.25f;
			int i = 0;
			foreach(GameObject player in players) {
				if(player.activeSelf && (player.GetComponent<PlayerController>().enabled || player.GetComponent<NavMeshAgent>().enabled)) {
					player.GetComponent<NavMeshAgent>().enabled = true;
					player.GetComponent<NavMeshAgent>().isStopped = true;
					player.GetComponent<PlayerController>().enabled = false;
				}
				_autofollow[i++] = false;
			}

			for(int j = 0; j < players.Length; j++) {
				int k = (_pointer - 1 - j + players.Length) % players.Length;
				if(players[j].activeSelf && (players[k].GetComponent<PlayerController>().enabled || players[k].GetComponent<NavMeshAgent>().enabled)) {
					_pointer = k;
					players[_pointer].GetComponent<NavMeshAgent>().enabled = false;
					players[_pointer].GetComponent<PlayerController>().enabled = true;
				}
			}
		}

		if(state == State.GAME && _pathfindTimer <= 0 && Input.GetButton("Fire3")) {
			_pathfindTimer = 0.5f; // Twice a second is enough
			for(int i = 0; i < players.Length; i++) {
				if(i == _pointer || players[i].activeSelf && !players[i].GetComponent<NavMeshAgent>().enabled) continue;
				_autofollow[i] = true;

				NavMeshAgent agent = players[i].GetComponent<NavMeshAgent>();
				agent.SetDestination(players[_pointer].transform.position);
				agent.isStopped = false;
			}
		}

		if(state != State.MENU && Input.GetButton("Cancel")) {
			if(!_canceled) {
				Time.timeScale = Time.timeScale > 0 ? 0 : 1;
				titleHud.transform.Find("Menu/Begin").GetComponent<Selectable>().Select();
				titleHud.SetActive(!titleHud.activeSelf);
				gameHud.SetActive(!gameHud.activeSelf);
				_vingetteTarget = gameHud.activeSelf ? 0 : 1;
				Cursor.visible = !Cursor.visible;
				// state = State.PAUSE;
			} else {
				// state = State.GAME;
			}
			_canceled = true;
		} else {
			_canceled = false;
		}

		// Autofollow
		if(state == State.GAME && _pathfindTimer <= 0) {
			for(int i = 0; i < players.Length; i++) {
				if(i == _pointer || (!players[i].GetComponent<PlayerController>().enabled && !players[i].GetComponent<NavMeshAgent>().enabled)) continue;

				float distance = Vector3.Distance(players[_pointer].transform.position, players[i].transform.position);

				if(distance > 50 && !_autofollow[i]) {
					_pathfindTimer = 0.5f; // Twice a second is enough
					_autofollow[i] = true;

					NavMeshAgent agent = players[i].GetComponent<NavMeshAgent>();
					agent.SetDestination(players[_pointer].transform.position);
					agent.isStopped = false;
				}

				if(distance < 30 && _autofollow[i]) {
					_autofollow[i] = false;
					players[i].GetComponent<NavMeshAgent>().isStopped = true;
				}
			}
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

	public void BeginDialogue(Message message) {
		_vingetteTarget = 1;
		// dynamicCamera.GetComponent<CameraController>().cameraLocked = false;
		// dynamicCamera.GetComponent<CameraController>().cameraTarget.transform.Translate(Vector3.forward * 25);
		_dialogueTimed = true;

		state = State.DIALOGUE;

		SetDialogue(message);
	}

	public void SetDialogue(Message message) {
		this.dialogue = message;

		if(this.dialogue == null) {
			SetHint("", 0);

			_vingetteTarget = 0;
			dynamicCamera.GetComponent<CameraController>().cameraLocked = true;
			_dialogueTimed = false;
			state = State.GAME;

			Cursor.visible = false;
		} else {
			_dialogueTimer = this.dialogue.clip == null ? 5 : this.dialogue.clip.length*2;
			_dialogueTimed = true;
			SetHint(this.dialogue.text, this.dialogue.clip == null ? 5 : this.dialogue.clip.length*2);
			if(this.dialogue.clip == null) narrationSource.PlayOneShot(this.dialogue.clip, 1);

			switch(this.dialogue.action) {
				case MessageAction.NONE:
					gameHud.transform.Find("Panel").gameObject.SetActive(false);
					Cursor.visible = false;
				break;
				case MessageAction.CHOICE:
					_dialogueTimer = Mathf.Infinity;
					_hintTimer = Mathf.Infinity;
					gameHud.transform.Find("Panel").gameObject.SetActive(true);
					gameHud.transform.Find("Panel/Option 1").GetComponent<Text>().text = this.dialogue.children[0].text;
					gameHud.transform.Find("Panel/Option 2").GetComponent<Text>().text = this.dialogue.children[1].text;
					gameHud.transform.Find("Panel/Option 1").GetComponent<Selectable>().Select();
					Cursor.visible = true;
					break;
				case MessageAction.END:
					gameHud.transform.Find("Panel").gameObject.SetActive(false);
					Cursor.visible = false;
					break;
				case MessageAction.KILL:
					gameHud.transform.Find("Panel").gameObject.SetActive(false);
					players[_pointer].SetActive(false);
					Cursor.visible = false;
					break;
				case MessageAction.FINISH:
					gameHud.transform.Find("Panel").gameObject.SetActive(false);
					Cursor.visible = false;
					End();
					break;
				case MessageAction.SUICIDE:
					gameHud.transform.Find("Panel").gameObject.SetActive(false);
					if(this.dialogue.target != null) this.dialogue.target.SetActive(false);
					Cursor.visible = false;
					break;
				case MessageAction.JOIN:
					gameHud.transform.Find("Panel").gameObject.SetActive(false);
					Cursor.visible = false;
					if(this.dialogue.target != null) {
						this.dialogue.target.GetComponent<Dialogue>().enabled = false;
						this.dialogue.target.GetComponent<NavMeshAgent>().enabled = true;
					}
					break;
			}
		}
	}

	public void PlayNaration(AudioClip clip) {
		narrationSource.PlayOneShot(clip, 1);
	}

	public void DialogueOption(int option) {
		SetDialogue(option >= 0 && option < dialogue.children.Length ? dialogue.children[option] : null);
	}

	public void Begin() {
		state = State.GAME;

		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;
		titleHud.SetActive(false);
		gameHud.SetActive(true);
		_vingetteTarget = 0;
		dynamicCamera.GetComponent<CameraController>().cameraLocked = true;
		titleHud.transform.Find("Menu/Begin").GetComponent<Text>().text = "Resume";
	}

	public void End() {
		state = State.END;
		_vingetteTarget = 1;
		hint = "The End";
		hintText.text = hint;
		time = Mathf.Infinity;
		_hintTimer = time;
		titleHud.transform.Find("Menu/Begin").GetComponent<Text>().text = "Restart";
	}
}
