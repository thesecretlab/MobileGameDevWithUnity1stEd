using UnityEngine;
using System.Collections;

public class SignalOnTouch : MonoBehaviour {

	public string messageOnTouch;

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.CompareTag("Player")) {

			if (messageOnTouch != null) {
				GameManager.instance.SendMessage(messageOnTouch, this.gameObject);
			}

		}
	}
}
