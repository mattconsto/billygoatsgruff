using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject[] players;
	public GameObject camera;

	public Vector3 cameraOffset   = new Vector3(0, 0, 0);
	public float   cameraMinimum  = 1;
	public float   cameraAddition = 1;

	void Update () {
		// Average the players position, then move the camera
		Vector3[] pos  = players.Select(player => player.transform.position).ToArray();
		Vector3   ave  = pos.Aggregate(new Vector3(0,0,0), (a,b) => a+b) / (float) pos.Length;
		float     dist = Vector3.Distance(new Vector3(pos.Min(p => p.x), Mathf.Max(pos.Min(p => p.y), 0), pos.Min(p => p.z)), new Vector3(pos.Max(p => p.x), pos.Max(p => p.y), pos.Max(p => p.z)));

		camera.transform.position = new Vector3(ave.x + cameraOffset.x, Mathf.Max(dist + cameraAddition, cameraMinimum) + cameraOffset.y, ave.z + cameraOffset.z);
	}
}
