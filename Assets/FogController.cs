using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {
	public Color darkColor  = Color.black;
	public Color lightColor = Color.white;

	public float darkDensity  = 0.02f;
	public float lightDensity = 0.01f;

	public void Update () {
		float position = Mathf.Max(0, transform.position.y/500f);
		RenderSettings.fogColor = Color.Lerp(darkColor, lightColor, position);
		RenderSettings.fogDensity = Mathf.Lerp(darkDensity, lightDensity, position);
	}
}
