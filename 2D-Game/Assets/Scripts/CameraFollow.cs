using UnityEngine;
using System.Collections;

// BEGIN 2d_camerafollow
// Adjusts the camera to always match the Y-position of a target object,
// within certain limits.
public class CameraFollow : MonoBehaviour {

	// The object we want to match the Y position of.
	public Transform target;

	// The highest point the camera can go.
	public float topLimit = 10.0f;

	// The lowest point the camera can go.
	public float bottomLimit = -10.0f;

	// How quickly we should move towards the target.
	public float followSpeed = 0.5f;

	// After all objects have updated position, work out where this camera 
    // should be
	void LateUpdate () {

		// If we have a target...
		if (target != null) {

			// Get its position
			Vector3 newPosition = this.transform.position;

			// Work out where this camera should be
			newPosition.y = Mathf.Lerp (newPosition.y, 
target.position.y, followSpeed);

			// Clamp this new location to within our limits
			newPosition.y = Mathf.Min(newPosition.y, topLimit);
			newPosition.y = Mathf.Max(newPosition.y, bottomLimit);

			// Update our location
			transform.position = newPosition;
		}



	}

	// When selected in the editor, draw a line from the top limit to the 
    // bottom.
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;

		Vector3 topPoint = new Vector3(this.transform.position.x, 
topLimit, this.transform.position.z);
		Vector3 bottomPoint = new Vector3(this.transform.position.x, 
bottomLimit, this.transform.position.z);

		Gizmos.DrawLine(topPoint, bottomPoint);
	}
}
// END 2d_camerafollow