using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAutoDisable : MonoBehaviour {
	public GameObject camera;

	public void Update () {
		if(GetComponent<GvrViewer>() != null)
			GetComponent<GvrViewer>().VRModeEnabled = false;
		camera.GetComponent<GvrHead>().trackRotation = false;
		enabled = false;
	}
}
