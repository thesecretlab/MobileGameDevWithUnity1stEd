using UnityEngine;
using System.Collections;


// BEGIN 3d_inputmanager
public class InputManager : Singleton<InputManager> {

	public Vector3 rotationRate;

	public VirtualJoystick steering;


	// BEGIN 3d_inputmanager_weapons
	public float fireRate = 0.2f;

	private ShipWeapons weapons;

	private bool isFiring = false;

	public void SetWeapons(ShipWeapons weapons) {

		this.weapons = weapons;

	}

	public void RemoveWeapons(ShipWeapons weapons) {
		if (this.weapons == weapons) {
			this.weapons = null;
		}
	}

	public void StartFiring() {
		isFiring = true;
		StartCoroutine(FireWeapons());
	}

	public void StopFiring() {
		isFiring = false;
	}


	IEnumerator FireWeapons() {
		while (true) {

			if (isFiring == false) {
				yield break;
			}

			if (this.weapons != null) {
				weapons.Fire();
			}

			yield return new WaitForSeconds(fireRate);

		}

	}
	// END 3d_inputmanager_weapons
}
// END 3d_inputmanager