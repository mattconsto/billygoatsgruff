using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public int pointer = 0;

	public WorldState[] _machine;

	private bool audioObject = true;

	public void Start() {
		Change(pointer);
		currentTime = targetTime;
	}

	public void FixedUpdate() {
		// Lerp forwards towards desired time
		currentTime = InterpolatedLerp(currentTime, targetTime, 0.25f * Time.deltaTime, 24);

		float angle = 2 * Mathf.PI * ((currentTime + 18f) % 24f) / 24f;

		sunObject.transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * 500f;
		moonObject.transform.position = new Vector3(Mathf.Cos(-angle), Mathf.Sin(-angle), 0) * 500f;
		sunObject.transform.LookAt(Vector3.zero);
		moonObject.transform.LookAt(Vector3.zero);

		// Audio Crossfade
		audioObjectA.volume = Mathf.Lerp(audioObjectA.volume, audioObject ? 1 : 0, 0.25f * Time.deltaTime);
		audioObjectB.volume = Mathf.Lerp(audioObjectB.volume, audioObject ? 0 : 1, 0.25f * Time.deltaTime);
	}

	public static float InterpolatedLerp(float a, float b, float t, float l) {
		float num = Mathf.Repeat(b - a, l);
		if(num > l/2f) num -= l;
		return a + num * Mathf.Clamp01(t);
	}

	public void Change(int value) {
		if(pointer != value || Time.time == 0) {
			pointer = Mathf.Clamp(value, 0, _machine.Length - 1);
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
		return _machine[pointer];
	}
}
