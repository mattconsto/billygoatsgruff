using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour {
	public void FixedUpdate() {
		transform.RotateAround(Vector3.zero, Vector3.right, 10f*Time.deltaTime);
		transform.LookAt(Vector3.zero);
	}
}
