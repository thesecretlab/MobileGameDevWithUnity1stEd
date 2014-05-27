using UnityEngine;
using System.Collections;

public class SignalOnTouch : MonoBehaviour {

	public string messageOnTouch;

	void SendSignal(GameObject objectThatHit) {
		if (objectThatHit.CompareTag("Player")) {
			
			if (messageOnTouch != null) {
				GameManager.instance.SendMessage(messageOnTouch, this.gameObject);
			}
			
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		SendSignal (collider.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		SendSignal (collision.gameObject);
	}
}
