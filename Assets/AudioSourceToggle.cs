using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSourceToggle : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public bool state = true;
	public AudioSource[] sources;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		state = !state;
		foreach(AudioSource source in sources) source.mute = !state;
	}
}
