using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

	// The target we are following
	public Transform target;
	// The distance in the x-z plane to the target
	public float distance = 7.0f;
	// the height we want the camera to be above the target
	public float height = 2.5f;

	// How quickly we change rotation
	public float rotationDamping = 2;

	// How quickly we change height
	public float heightDamping = 5;

	// Whether we should be facing the same direction as the target
	public bool matchOrientation = true;

	// Update is called once per frame
	void LateUpdate()
	{
		// Early out if we don't have a target
		if (!target)
			return;

		// Calculate the current rotation angles
		var wantedRotationAngle = target.eulerAngles.y;
		var wantedHeight = target.position.y + height;

		var currentRotationAngle = transform.eulerAngles.y;
		var currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height
		currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = target.position;
		transform.position -= currentRotation * Vector3.forward * distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x ,currentHeight , transform.position.z);

		if (matchOrientation) {
			// Look at where the target is looking
			transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rotationDamping * Time.deltaTime);
		} else {
			// Always look at the target
			transform.LookAt(target);
		}

	}
}
