using UnityEngine;
using System.Collections;


// BEGIN 3d_inputmanager
public class InputManager : Singleton<InputManager> {

	// The joystick used to steer the ship.
	public VirtualJoystick steering;

	// BEGIN 3d_inputmanager_weapons
	// The delay between firing shots, in seconds.
	public float fireRate = 0.2f;

	// The current ShipWeapons script to fire from.
	private ShipWeapons currentWeapons;

	// If true, we are currently firing weapons.
	private bool isFiring = false;

	// Called by ShipWeapons to update the currentWeapons 
	// variable.
	public void SetWeapons(ShipWeapons weapons) {
		this.currentWeapons = weapons;
	}

	// Likewise; called to reset the currentWeapons variable.
	public void RemoveWeapons(ShipWeapons weapons) {

		// If the currentWeapons object is 'weapons', 
		// set it to null.
		if (this.currentWeapons == weapons) {
			this.currentWeapons = null;
		}
	}

	// Called when the user starts touching the Fire button.
	public void StartFiring() {

		// Kick off the routine that starts firing shots.
		StartCoroutine(FireWeapons());
	}


	IEnumerator FireWeapons() {

		// Mark ourself as firing shots
		isFiring = true;

		// Loop for as long as isFiring is true
		while (isFiring) {

			// If we have a weapons script, tell it to fire
			// a shot!
			if (this.currentWeapons != null) {
				currentWeapons.Fire();
			}

			// Wait for fireRate seconds before firing the
			// next shot
			yield return new WaitForSeconds(fireRate);

		}

	}

	// Called when the user stops touching the Fire button
	public void StopFiring() {

		// Setting this to false will end the loop in FireWeapons
		isFiring = false;
	}


	// END 3d_inputmanager_weapons
}
// END 3d_inputmanager