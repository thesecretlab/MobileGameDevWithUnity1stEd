using UnityEngine;
using System.Collections;

// Every frame, rotate this object so that it's rotation around
// the Z axis is a given value.
public class EyeLookDown : MonoBehaviour {

	// The angle we want to be facing.
	public float rotation = -90.0f;

	void LateUpdate() {
		// The rotation is done in LateUpdate in order to make sure
		// that any updating of position due to physics, etc, has 
        // finished.

		transform.rotation = Quaternion.Euler(0, 0, rotation);
	}
}
