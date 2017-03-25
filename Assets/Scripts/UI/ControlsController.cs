using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlsController : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GameObject target;
	public GameObject hidden;

	public void OnPointerClick(PointerEventData ed) {
		target.SetActive(!target.activeSelf);
		hidden.SetActive(!target.activeSelf);

		if(target.activeSelf) {
			target.transform.Find("Controls Image").GetComponent<Selectable>().Select();
		} else {
			hidden.transform.Find("Controls").GetComponent<Selectable>().Select();
		}
	}

	public void OnSubmit(BaseEventData ed) {
		target.SetActive(!target.activeSelf);
		hidden.SetActive(!target.activeSelf);

		if(target.activeSelf) {
			target.transform.Find("Controls Image").GetComponent<Selectable>().Select();
		} else {
			hidden.transform.Find("Controls").GetComponent<Selectable>().Select();
		}
	}
}
