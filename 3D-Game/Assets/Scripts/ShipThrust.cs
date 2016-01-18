using UnityEngine;
using System.Collections;

// BEGIN 3d_shipthrust
public class ShipThrust : MonoBehaviour {

	public float speed = 5.0f;

	// Move the ship forward at a constant speed
	void Update () {
		var offset = Vector3.forward * Time.deltaTime * speed;
		this.transform.Translate(offset);
	}
}
// END 3d_shipthrust