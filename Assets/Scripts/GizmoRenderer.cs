using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	Draw a Gizmo
*/
public class GizmoRenderer : MonoBehaviour {
	public string gizmoPath = "";

	public void OnDrawGizmos() {
		if(gizmoPath != "") Gizmos.DrawIcon(transform.position, gizmoPath, true);

		Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
		
		if(GetComponent<BoxCollider>() != null)
			Gizmos.DrawWireCube(GetComponent<BoxCollider>().center, GetComponent<BoxCollider>().size);

		if(GetComponent<SphereCollider>() != null)
			Gizmos.DrawWireSphere(GetComponent<SphereCollider>().center, GetComponent<SphereCollider>().radius);
	}
}
