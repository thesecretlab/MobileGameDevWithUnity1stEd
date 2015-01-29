using UnityEngine;
using System.Collections;

// Moves in a certain direction, at a given speed.
public class Mover : MonoBehaviour {

	// The speed we should move at, in units per second
	public float speed = 6.0f;

	// The direction we should move in, in world space
	public Vector3 direction;

	void Update () {

		// Every frame, move in the given direction, at the given speed.
		transform.Translate(direction * speed * Time.deltaTime);
	}
}
