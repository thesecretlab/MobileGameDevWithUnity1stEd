using UnityEngine;
using System.Collections;

// BEGIN 3d_shipweapons
public class ShipWeapons : MonoBehaviour {

	public GameObject shotPrefab;

	// BEGIN 3d_shipweapons_inputmanager
	public void Awake() {
		InputManager.instance.SetWeapons(this);
	}

	public void OnDestroy() {
		InputManager.instance.RemoveWeapons(this);
	}
	// END 3d_shipweapons_inputmanager

	public Transform[] firePoints;

	private int firePointIndex;

	public void Fire() {

		if (firePoints.Length == 0) 
			return;

		var firePointToUse = firePoints[firePointIndex];

		var shot = Instantiate(shotPrefab);

		shot.transform.position = firePointToUse.position;
		shot.transform.rotation = firePointToUse.rotation;

		// BEGIN 3d_shipweapons_audio
		// Sound
		var audio = firePointToUse.GetComponent<AudioSource>();
		if (audio) {
			audio.Play();
		}
		// END 3d_shipweapons_audio

		firePointIndex++;

		if (firePointIndex >= firePoints.Length)
			firePointIndex = 0;

	}

}
// END 3d_shipweapons