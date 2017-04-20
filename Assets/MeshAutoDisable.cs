using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshAutoDisable : MonoBehaviour {
	public void Update () {
		if(GetComponent<MeshRenderer>() != null)
			GetComponent<MeshRenderer>().enabled = false;
		enabled = false;
	}
}
