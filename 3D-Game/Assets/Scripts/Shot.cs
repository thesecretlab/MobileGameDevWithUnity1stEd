using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	// Moves forward at a certain speed, and dies after a certain time.
	public float speed = 50.0f;
	public float life = 5.0f;

	void Start() {
		Destroy(gameObject, life);
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * speed  * Time.deltaTime);
	}


}
