using UnityEngine;
using System.Collections;

// BEGIN 3d_shipsteering
public class ShipSteering : MonoBehaviour {

	public float turnRate = 2.0f;
	public float levelDamping = 1.0f;

	void Update () {

		// Create a new rotation by multiplying the joystick's direction
		// by turnRate, and clamping that to 90% of half a circle
		var steeringInput = InputManager.instance.steering.delta;

		var rotation = new Vector3();

		rotation.y = steeringInput.x;
		rotation.x = steeringInput.y;

		rotation *= turnRate;

		rotation.x = Mathf.Clamp(rotation.x, -Mathf.PI * 0.9f, Mathf.PI * 0.9f);
        
        var newOrientation = Quaternion.Euler(rotation);

		// Combine this turn with our current orientation
		transform.rotation *= newOrientation;

		// Next, try to minimise roll

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