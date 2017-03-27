using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsController : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GameObject target;
	public GameObject hidden;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		// Toggle active, and select the first child selected.
		target.SetActive(!target.activeSelf);
		hidden.SetActive(!target.activeSelf);
		
		EventSystem.current.SetSelectedGameObject(null);
		foreach(TextHover sel in GetComponentsInChildren<TextHover>()) sel.OnDeselect(null);
		(target.activeSelf ? target : hidden).GetComponentsInChildren<Selectable>()[0].Select();
	}
}
