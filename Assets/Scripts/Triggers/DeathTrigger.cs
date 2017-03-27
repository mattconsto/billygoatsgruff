using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {
	public AudioClip clip;
	public AudioSource source;

	public void OnTriggerEnter(Collider col) {
		if(source != null && clip != null) source.PlayOneShot(clip, 1);
		col.gameObject.SetActive(false);
	}
}
