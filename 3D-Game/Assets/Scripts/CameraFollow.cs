using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;

	public float damping = 1.0f;

	Vector3 offset;
	
	void Start() {
		offset = target.transform.position - transform.position;
	}
	
	void LateUpdate() {
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
		
		Quaternion rotation = Quaternion.Euler(0, angle, 0);

		Vector3 newPosition = target.transform.position - (rotation * offset);
		transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * damping);

		transform.LookAt(target.transform);
	}
}
