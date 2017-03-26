using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueOption : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GameController controller;
	public int option = 0;

	public void OnPointerClick(PointerEventData ed) {
		controller.DialogueOption(option);
	}

	public void OnSubmit(BaseEventData ed) {
		controller.DialogueOption(option);
	}
}
