using UnityEngine;
using System.Collections;

// BEGIN 3d_shipweapons
public class ShipWeapons : MonoBehaviour {

	// The prefab to use for each shot
	public GameObject shotPrefab;

	// BEGIN 3d_shipweapons_inputmanager
	public void Awake() {
		// When this object starts up, tell the input manager
		// to use me as the current weapon object
		InputManager.instance.SetWeapons(this);
	}

	// Called when the object is removed
	public void OnDestroy() {
		// Don't do this if we're not playing
		if (Application.isPlaying == true) {
			InputManager.instance.RemoveWeapons(this);
		}
	}
	// END 3d_shipweapons_inputmanager

	// The list of places where a shot can emerge from
	public Transform[] firePoints;

	// The index into firePoints that the next shot will fire from
	private int firePointIndex;

	// Called by InputManager.
	public void Fire() {

		// If we have no points to fire from, return
		if (firePoints.Length == 0) 
			return;

		// Work out which point to fire from
		var firePointToUse = firePoints[firePointIndex];

		// Create the new shot, at the fire point's position
		// and with its rotation
		Instantiate(shotPrefab, 
			firePointToUse.position, 
			firePointToUse.rotation);

		// BEGIN 3d_shipweapons_audio
		// If the fire point has an audio source component, 
		// play its sound effect
		var audio = firePointToUse.GetComponent<AudioSource>();
		if (audio) {
			audio.Play();
		}
		// END 3d_shipweapons_audio

		// Move to the next fire point
		firePointIndex++;

		// If we've moved past the last fire point in the list,
		// move back to the start of the queue
		if (firePointIndex >= firePoints.Length)
			firePointIndex = 0;

	}

}
// END 3d_shipweapons