using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 accel = Input.acceleration;

		Debug.Log(accel);
	}
}
