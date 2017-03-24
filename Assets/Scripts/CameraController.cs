using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float cameraAMinimum = 1;
	public float cameraDMinimum = 1;
	public float cameraAngle   = 0;

	public bool cameraLocked = true;
	public Transform cameraTarget;

	private GameObject[] _players;
	private Quaternion _originalRotation = Quaternion.Euler(90, 0, 0);

	public void Start() {
		// Store a list of players and our original rotation for later.
		_players = GameObject.FindGameObjectsWithTag("Player");

		// Needs to run twice, not sure why.
		Update(); LerpTransform(transform, cameraTarget, 1);
		Update(); LerpTransform(transform, cameraTarget, 1);
	}

	public static void LerpTransform (Transform t1, Transform t2, float t) {
		t1.position   = Vector3.Lerp(t1.position, t2.position, t);
		t1.rotation   = Quaternion.Lerp (t1.rotation, t2.rotation, t);
		t1.localScale = Vector3.Lerp(t1.localScale, t2.localScale, t);
	}

	public void Update () {
		// Average the players position, to calculate the distance between the players.
		if(cameraLocked) {
			Vector3[] positions = _players.Where(player => player.active).Select(player => player.transform.position).ToArray();
			Vector3 average = positions.Aggregate(new Vector3(0,0,0), (a,b) => a+b) / (float)positions.Length;
			Vector3[] bounds = new Vector3[] {
				new Vector3(positions.Min(p => p.x), Mathf.Max(positions.Min(p => p.y), 0), positions.Min(p => p.z)),
				new Vector3(positions.Max(p => p.x), positions.Max(p => p.y), positions.Max(p => p.z))
			};
			float distance = Vector3.Distance(bounds[0], bounds[1]);
			Vector3 normal = Vector3.Cross(bounds[0], bounds[1]);

			// Position and rotate the camera
			cameraTarget.position = new Vector3(average.x, distance/1.5f + cameraDMinimum, average.z);
			cameraTarget.rotation = _originalRotation;
			cameraTarget.RotateAround(average, Vector3.right, Mathf.Max(cameraAngle - distance/1.5f - cameraAMinimum, -80));
		}

		LerpTransform(transform, cameraTarget, 2*Time.deltaTime);
	}
}
