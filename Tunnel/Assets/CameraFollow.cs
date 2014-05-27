using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public GameObject target;
	public float topLimit;
	public float bottomLimit;

	void LateUpdate () {

		Vector3 newPosition = this.transform.position;

		newPosition.y = Mathf.Lerp (newPosition.y, target.transform.position.y, 0.5f);
		newPosition.y = Mathf.Min(newPosition.y, topLimit);
		newPosition.y = Mathf.Max(newPosition.y, bottomLimit);
		transform.position = newPosition;

	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;

		Vector3 topPoint = new Vector3(this.transform.position.x, topLimit, this.transform.position.z);
		Vector3 bottomPoint = new Vector3(this.transform.position.x, bottomLimit, this.transform.position.z);

		Gizmos.DrawLine(topPoint, bottomPoint);
	}
}
