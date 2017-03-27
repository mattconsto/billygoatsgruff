using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GlobalSoundSource : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public AudioSource source;
	public AudioClip   clip;
	public float       volume = 1f;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		if(clip != null) source.PlayOneShot(clip, Mathf.Clamp(volume, 0, 1));
	}
}
