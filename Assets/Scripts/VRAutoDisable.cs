using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAutoDisable : MonoBehaviour {
	public void Update () {
		if(GetComponent<GvrViewer>() != null)
			GetComponent<GvrViewer>().VRModeEnabled = false;
		enabled = false;
	}
}
