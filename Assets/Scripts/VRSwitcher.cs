using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VRSwitcher : MonoBehaviour, IPointerClickHandler, ISubmitHandler {
	public GvrViewer viewer;
	public GameObject[] canvases;
	public Selectable selectable;

	public void OnPointerClick(PointerEventData ed) {Action();}
	public void OnSubmit(BaseEventData ed) {Action();}

	private void Action() {
		print("Switching VR Mode");
		viewer.VRModeEnabled = !viewer.VRModeEnabled;

		foreach(GameObject canvas in canvases) {
			canvas.GetComponent<Canvas>().renderMode = viewer.VRModeEnabled ? RenderMode.WorldSpace : RenderMode.ScreenSpaceCamera;
			if(canvas.GetComponent<GraphicRaycaster>() != null) canvas.GetComponent<GraphicRaycaster>().enabled = !viewer.VRModeEnabled;
		}

		selectable.Select();
	}
}
