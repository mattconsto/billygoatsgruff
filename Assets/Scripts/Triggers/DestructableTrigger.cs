﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableTrigger : MonoBehaviour {
	public AudioClip clip;
	public AudioSource source;

	public GameObject[] objects;
	public float impulse;

	public void OnCollisionEnter(Collision col) {
		if((objects.Length == 0 || System.Array.IndexOf(objects, col.gameObject) != -1) && col.impulse.magnitude >= impulse) {
			if(source != null && clip != null) source.PlayOneShot(clip, 1);
			gameObject.SetActive(false);
		}
	}
}
