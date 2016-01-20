using UnityEngine;
using System.Collections;

// BEGIN 3d_shot
// Moves forward at a certain speed, and dies after a certain time.
public class Shot : MonoBehaviour {

	// The speed at which the shot will move forward
	public float speed = 100.0f;

	// Remove this object after this many seconds
	public float life = 5.0f;

	void Start() {
		// Destroy after 'life' seconds
		Destroy(gameObject, life);
	}

	void Update () {
		// Move forward at constant speed
		transform.Translate(Vector3.forward * speed  * Time.deltaTime);
	}
}
// END 3d_shot