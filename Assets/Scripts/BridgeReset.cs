using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeReset : MonoBehaviour {
	void Update () {
		transform.rotation = Quaternion.identity;
		enabled = false;
	}
}
