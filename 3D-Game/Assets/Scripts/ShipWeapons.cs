using UnityEngine;
using System.Collections;

public class ShipWeapons : MonoBehaviour {

	public GameObject shotPrefab;

	public void Awake() {
		InputManager.instance.SetWeapons(this);
	}

	public void OnDestroy() {
		InputManager.instance.RemoveWeapons(this);
	}

	public Transform[] firePoints;

	private int firePointIndex;

	public void Fire() {

		if (firePoints.Length == 0) 
			return;

		var firePointToUse = firePoints[firePointIndex];

		var shot = Instantiate(shotPrefab);

		shot.transform.position = firePointToUse.position;
		shot.transform.rotation = firePointToUse.rotation;

		firePointIndex++;

		if (firePointIndex >= firePoints.Length)
			firePointIndex = 0;

	}

}
