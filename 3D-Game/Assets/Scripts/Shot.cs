using UnityEngine;
using System.Collections;

// BEGIN 3d_shot
// Moves forward at a certain speed, and dies after a certain time.
public class Shot : MonoBehaviour {

	public float speed = 50.0f;
	public float life = 5.0f;

	// Destroy after 'life' seconds
	void Start() {
		Destroy(gameObject, life);
	}

	// Move forward at constant speed
	void Update () {
		transform.Translate(Vector3.forward * speed  * Time.deltaTime);
	}
}
// END 3d_shot