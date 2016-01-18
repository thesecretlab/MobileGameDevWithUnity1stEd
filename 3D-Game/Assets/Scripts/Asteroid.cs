using UnityEngine;
using System.Collections;

// BEGIN 3d_asteroid
public class Asteroid : MonoBehaviour {

	public float speed = 1.0f;

	void Start () {
		GetComponent<Rigidbody>().velocity = transform.forward * speed;

		var indicator = IndicatorManager.instance.AddLabel(gameObject, Color.red);
		// BEGIN 3d_asteroid_gamemanager
        indicator.showDistanceTo = GameManager.instance.currentSpaceStation.transform;
		// END 3d_asteroid_gamemanager
	}

}
// END 3d_asteroid