using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Animation>().Play("Walk");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
