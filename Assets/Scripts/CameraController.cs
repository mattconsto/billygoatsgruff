using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameController controller;

	public float cameraDistance = 1;
	public float cameraAngle   = 0;

	public bool cameraLocked = true;
	public Transform cameraTarget;

	public Vector3 offset = new Vector3(0, 0, 0);
	public Vector3 offsetMultiplier = Vector3.one;

	private Quaternion _originalRotation = Quaternion.Euler(90, 0, 0);

	public void Start() {
		// Needs to run twice, not sure why.
		Update(); LerpTransform(transform, cameraTarget, 1);
		Update(); LerpTransform(transform, cameraTarget, 1);
	}

	public static void LerpTransform(Transform t1, Transform t2, float t) {
		t1.position   = Vector3.Lerp(t1.position, t2.position, t);
		t1.rotation   = Quaternion.Lerp(t1.rotation, t2.rotation, t);
		t1.localScale = Vector3.Lerp(t1.localScale, t2.localScale, t);
	}

	public void Update() {
		// Average the players position, to calculate the distance between the players.
		if(cameraLocked) {
			// Position and rotate the camera
			cameraTarget.position = controller.players[controller._pointer].transform.position + Vector3.up * cameraDistance + Vector3.Scale(offset, offsetMultiplier);
			cameraTarget.rotation = _originalRotation;
			cameraTarget.RotateAround(controller.players[controller._pointer].transform.position, Vector3.right, cameraAngle);
			cameraTarget.rotation = Quaternion.Euler(cameraTarget.rotation.eulerAngles.x / 2, 0, 0);
		}

		LerpTransform(transform, cameraTarget, 2*Time.deltaTime);
	}
}
