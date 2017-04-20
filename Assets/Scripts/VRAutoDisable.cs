using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRAutoDisable : MonoBehaviour {
	public void Update () {
		GetComponent<GvrViewer>().VRModeEnabled = false;
		enabled = false;
	}
}
