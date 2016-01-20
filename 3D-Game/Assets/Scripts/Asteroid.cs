using UnityEngine;
using System.Collections;

// BEGIN 3d_asteroid
public class Asteroid : MonoBehaviour {

	// The speed at which the asteroid moves.
	public float speed = 10.0f;

	void Start () {
		// Set the velocity of the rigidbody
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

		// Create a red indicator for this asteroid
		var indicator = IndicatorManager.instance.AddIndicator(gameObject, Color.red);

		// BEGIN 3d_asteroid_gamemanager
		// Track the distance from this object to the current space station
		// that's managed by the GameManager
        indicator.showDistanceTo = GameManager.instance.currentSpaceStation.transform;
		// END 3d_asteroid_gamemanager
	}

}
// END 3d_asteroid