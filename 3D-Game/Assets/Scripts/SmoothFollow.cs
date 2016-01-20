using UnityEngine;

// BEGIN 3d_smoothfollow
public class SmoothFollow : MonoBehaviour
{

	// The target we are following
	public Transform target;

	// The height we want the camera to be above the target
	public float height = 5.0f;

	// The distance to the target, not counting height
	public float distance = 10.0f;

	// How much we slow down changes in rotation and height
	public float rotationDamping;
	public float heightDamping;

	// Update is called once per frame
	void LateUpdate()
	{
		// Bail out if we don't have a target
		if (!target)
			return;

		// Calculate the current rotation angles
		var wantedRotationAngle = target.eulerAngles.y;
		var wantedHeight = target.position.y + height;

		// Note where we're currently positioned and looking
		var currentRotationAngle = transform.eulerAngles.y;
		var currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, 
			wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, 
			wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set the position of the camera on the x-z plane to:
		// "distance" meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the position of the camera using our new height
		transform.position = new Vector3(transform.position.x, 
			currentHeight, transform.position.z);

		// Finally, look at where the target is looking
		transform.rotation = Quaternion.Lerp(transform.rotation, 
			target.rotation, rotationDamping * Time.deltaTime);

	}
}
// END 3d_smoothfollow