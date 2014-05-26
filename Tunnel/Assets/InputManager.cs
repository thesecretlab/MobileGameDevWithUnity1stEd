using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private static InputManager _instance;

	public static InputManager instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<InputManager>();
			}
			return _instance;
		}
	}

	// -1.0 = full left, +1.0 = full right
	private float _sidewaysMotion = 0.0f;

	public float sidewaysMotion {
		get {
			return _sidewaysMotion;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 accel = Input.acceleration;

		_sidewaysMotion = accel.x;

	}
}
