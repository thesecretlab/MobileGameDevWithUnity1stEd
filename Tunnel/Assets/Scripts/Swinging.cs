using UnityEngine;
using System.Collections;

// Require a 2D physicsbody
[RequireComponent (typeof(Rigidbody2D))]
public class Swinging : MonoBehaviour {

	public float swingSensitivity = 1.0f;

	void FixedUpdate() {
		// get the swing amount
		float swing = InputManager.instance.sidewaysMotion;

		// calculate a force
		Vector2 force = new Vector2(swing * swingSensitivity, 0);

		// apply the force to the rigidbody
		GetComponent<Rigidbody2D>().AddForce(force);
	}

}
