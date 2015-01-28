using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class SignalOnTouch : MonoBehaviour {

	public UnityEvent onTouch;

	public bool playAudioOnTouch = true;

	void SendSignal(GameObject objectThatHit) {


		if (objectThatHit.CompareTag("Player")) {

			if (playAudioOnTouch) {
				var audio = GetComponent<AudioSource>();
				
				if (audio)
					audio.Play();
			}

			
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
