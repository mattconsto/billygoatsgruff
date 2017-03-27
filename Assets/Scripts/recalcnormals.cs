using UnityEngine;
using System.Collections;

public class recalcnormals : MonoBehaviour {
	public void Start() {
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.RecalculateNormals(-90f);
	}
}