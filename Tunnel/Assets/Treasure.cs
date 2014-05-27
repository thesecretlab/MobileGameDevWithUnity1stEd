using UnityEngine;
using System.Collections;

public class Treasure : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("Player")) {
			// remove this object
			gameObject.SetActive(false);
		}
	}
}
