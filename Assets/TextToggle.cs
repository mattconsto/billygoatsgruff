using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextToggle : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public int pointer = 0;
	public string[] text;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		if(text.Length > 0) {
			pointer = (pointer + 1) % text.Length;
			GetComponent<Text>().text = text[pointer];
		}
	}
}
