using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuitController : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public void Start() {
		// Quitting WebGL makes no sense.
		#if UNITY_WEBGL
			gameObject.SetActive(false);
		#endif
	}

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		// Quit the game AND the editor
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}
