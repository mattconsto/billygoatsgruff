using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeginController : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GameController controller;
	public WorldStateMachine machine;

	public void OnPointerClick(PointerEventData ed) {
		controller.Begin();
		machine.Change(0);
	}

	public void OnSubmit(BaseEventData ed) {
		controller.Begin();
		machine.Change(0);
	}
}
