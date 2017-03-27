using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueOption : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GameController controller;
	public int option = 0;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		controller.DialogueOption(option);
	}
}
