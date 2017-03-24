using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class WorldState {
	public AudioClip music;
	public bool loop = false;
	public float time = 0;
}

public class WorldStateMachine : MonoBehaviour {
	public GameObject sunObject;
	public GameObject moonObject;
	public AudioSource audioObjectA;
	public AudioSource audioObjectB;

	public float currentTime;
	public float targetTime;

	public WorldState[] _machine;
	private int _pointer = -1;

	private bool audioObject = true;

	public void Start() {
		Change(0);
		currentTime = targetTime;
	}

	public void FixedUpdate() {
		// Lerp forwards towards desired time
		currentTime = Mathf.Lerp(currentTime, targetTime, 0.25f * Time.deltaTime);

		float angle = 2 * Mathf.PI * ((currentTime + 18f) % 24f) / 24f;

		sunObject.transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 500f;
		moonObject.transform.position = new Vector3(Mathf.Cos(-angle), Mathf.Sin(-angle), 0) * 500f;
		sunObject.transform.LookAt(Vector3.zero);
		moonObject.transform.LookAt(Vector3.zero);

		// Audio Crossfade
		audioObjectA.volume = Mathf.Lerp(audioObjectA.volume, audioObject ? 1 : 0, 0.25f * Time.deltaTime);
		audioObjectB.volume = Mathf.Lerp(audioObjectB.volume, audioObject ? 0 : 1, 0.25f * Time.deltaTime);
	}

	public void Change(int value) {
		if(value != _pointer) {
			_pointer = Mathf.Clamp(value, 0, _machine.Length - 1);
			targetTime = State().time > targetTime ? State().time : State().time + 24;

			audioObject = !audioObject;

			if(audioObject) {
				audioObjectA.loop = State().loop;
				audioObjectA.clip = State().music;
				audioObjectA.Play();
			} else {
				audioObjectB.loop = State().loop;
				audioObjectB.clip = State().music;
				audioObjectB.Play();
			}
		}
	}

	public WorldState State() {
		return _machine[_pointer];
	}
}
