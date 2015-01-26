using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SignalOnTouch : MonoBehaviour {

	public UnityEvent onTouch;

	void SendSignal(GameObject objectThatHit) {
		if (objectThatHit.CompareTag("Player")) {
			
			onTouch.Invoke();

		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		SendSignal (collider.gameObject);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		SendSignal (collision.gameObject);
	}
}
