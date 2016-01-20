using UnityEngine;
using System.Collections;

// BEGIN 3d_shipsteering
public class ShipSteering : MonoBehaviour {

	// The rate at which the ship turns
	public float turnRate = 6.0f;

	// The strength with which the ship levels out
	public float levelDamping = 1.0f;

	void Update () {

		// Create a new rotation by multiplying the joystick's direction
		// by turnRate, and clamping that to 90% of half a circle.

		// First, get the user's input.
		var steeringInput = InputManager.instance.steering.delta;

		// Now, create a rotation amount, as a vector.
		var rotation = new Vector2();

		rotation.y = steeringInput.x;
		rotation.x = steeringInput.y;

		// Multiply by turnRate to get the amount we want to steer by.
		rotation *= turnRate;

		// Turn this into radians by multiplying by 90% of a half-circle
		rotation.x = Mathf.Clamp(rotation.x, -Mathf.PI * 0.9f, Mathf.PI * 0.9f);
        
		// And turn those radians into a rotation quaternion!
        var newOrientation = Quaternion.Euler(rotation);

		// Combine this turn with our current orientation
		transform.rotation *= newOrientation;

		// Next, try to minimise roll!

		// Start by working out what our orientation 
		// would be if we weren't rolled around the Z 
		// axis at all
		var levelAngles = transform.eulerAngles;
		levelAngles.z = 0.0f;
		var levelOrientation = Quaternion.Euler(levelAngles);

		// Combine our current orientation with a small amount of this
		// "zero-roll" orientation; when this happens over multiple
		// frames, the object will slowly level out to zero roll
		transform.rotation = Quaternion.Slerp(transform.rotation, levelOrientation, levelDamping * Time.deltaTime);

	}
}
// END 3d_shipsteering