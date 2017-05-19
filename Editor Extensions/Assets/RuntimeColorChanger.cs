using UnityEngine;
using System.Collections;

// BEGIN color_changer
public class RuntimeColorChanger : MonoBehaviour {

	public Color color = Color.white;

	void Awake() {
		GetComponent<Renderer>().material.color = color;
	}
}
// END color_changer

