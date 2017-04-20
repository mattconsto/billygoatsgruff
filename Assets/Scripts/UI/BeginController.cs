using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginController : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GameController controller;
	public WorldStateMachine machine;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		if(controller.state == GameController.State.END) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		} else {
			controller.Begin();
			machine.Change(0);
		}
	}
}
