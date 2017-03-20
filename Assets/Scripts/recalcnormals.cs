using UnityEngine;
using System.Collections;

public class recalcnormals : MonoBehaviour {
    void Start() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.RecalculateNormals(-90f);
    }
}