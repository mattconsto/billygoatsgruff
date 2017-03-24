using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float cameraAMinimum = 1;
	public float cameraDMinimum = 1;
	public float cameraAngle   = 0;

	private GameObject[] _players;
	private Quaternion _originalRotation;

	public void Start() {
		// Store a list of players and our original rotation for later.
		_players = GameObject.FindGameObjectsWithTag("Player");
		_originalRotation = transform.rotation;
	}

	public void Update () {
		// Average the players position, to calculate the distance between the players.
		Vector3[] positions = _players.Select(player => player.transform.position).ToArray();
		Vector3 average = positions.Aggregate(new Vector3(0,0,0), (a,b) => a+b) / (float)positions.Length;
		Vector3[] bounds = new Vector3[] {
			new Vector3(positions.Min(p => p.x), Mathf.Max(positions.Min(p => p.y), 0), positions.Min(p => p.z)),
			new Vector3(positions.Max(p => p.x), positions.Max(p => p.y), positions.Max(p => p.z))
		};
		float distance = Vector3.Distance(bounds[0], bounds[1]);
		Vector3 normal = Vector3.Cross(bounds[0], bounds[1]);

		// Position and rotate the camera
		transform.position = new Vector3(average.x, distance/1.5f + cameraDMinimum, average.z);
		transform.rotation = _originalRotation;
		transform.RotateAround(average, Vector3.right, Mathf.Max(cameraAngle - distance/1.5f - cameraAMinimum, -80));
		// transform.RotateAround(average, Vector3.up, Mathf.Rad2Deg * Mathf.Atan(normal.z/normal.x)+90); // Almost works, but it switches over.
	}
}
