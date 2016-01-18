using UnityEngine;
using System.Collections;

// BEGIN 3d_boundary
public class Boundary : MonoBehaviour {

	public float warningRadius = 140.0f;
	public float destroyRadius = 150.0f;

	public void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position, warningRadius);

		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, destroyRadius);
	}
}
// END 3d_boundary
