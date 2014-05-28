using UnityEngine;
using System.Collections;

public class GnomeComponents : MonoBehaviour {

	public Rigidbody2D ropeBody;
	public GameObject treasure;

	public void DestroyGnome() {
		// find all child objects, and disconnect their joints

		foreach (Transform part in transform) {

			foreach (Joint2D joint in part.GetComponentsInChildren<Joint2D>()) {
				Destroy (joint);
			}

		}
	}
}
