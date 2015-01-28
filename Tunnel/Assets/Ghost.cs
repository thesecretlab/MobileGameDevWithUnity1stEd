using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour {

	public float speed = 5.0f;

	void Update () {
		transform.Translate(new Vector3(0, speed, 0) * Time.deltaTime);
	}
}
