// BEGIN 2d_swinging

using UnityEngine;
using System.Collections;

// Uses the input manager to apply sideways forces to an object.
// Used to make the gnome swing side-to-side.
public class Swinging : MonoBehaviour {

	// How much should we swing by? Bigger numbers = more swing
	public float swingSensitivity = 100.0f;

	// Use FixedUpdate instead of Update, in order to play better with 
	// the physics engine
	void FixedUpdate() {

		// If we have no ridigbody (anymore), remove this component
		if (GetComponent<Rigidbody2D>() == null) {
			Destroy (this);
			return;
		}

		// Get the tilt amount from the InputManager
		float swing = InputManager.instance.sidewaysMotion;

		// Calculate a force to apply
		Vector2 force = new Vector2(swing * swingSensitivity, 0);

		// Apply the force
		GetComponent<Rigidbody2D>().AddForce(force);
	}

}
// END 2d_swinging