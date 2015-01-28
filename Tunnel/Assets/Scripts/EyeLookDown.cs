using UnityEngine;
using System.Collections;

public class EyeLookDown : MonoBehaviour {

	public float rotation = -90.0f;

	void LateUpdate() {
		transform.rotation = Quaternion.Euler(0, 0, rotation);
	}
}
