using UnityEngine;
using System.Collections;

// BEGIN 2d_signalontouch
using UnityEngine.Events;

// Invokes a UnityEvent when the Player collides with this object.
[RequireComponent (typeof(Collider2D))]
public class SignalOnTouch : MonoBehaviour {

	// The UnityEvent to run when we collide.
	// Attach methods to run in the editor.
	public UnityEvent onTouch;

	// If true, attempt to play an AudioSource when we collide.
	public bool playAudioOnTouch = true;

	// When we enter a trigger area, call SendSignal.
	void OnTriggerEnter2D(Collider2D collider) {
		SendSignal (collider.gameObject);
	}

	// When we collide with this object, call SendSignal.
	void OnCollisionEnter2D(Collision2D collision) {
		SendSignal (collision.gameObject);
	}

	// Checks to see if this object was tagged as Player, and invoke
	// the UnityEvent if it was.
	void SendSignal(GameObject objectThatHit) {

		// Was this object tagged Player?
		if (objectThatHit.CompareTag("Player")) {

			// If we should play a sound, attempt to play it
			if (playAudioOnTouch) {
				var audio = GetComponent<AudioSource>();
				
				if (audio && audio.gameObject.activeInHierarchy)
					audio.Play();
			}

			// Invoke the event
			onTouch.Invoke();
		}
	}


}
// END 2d_signalontouch