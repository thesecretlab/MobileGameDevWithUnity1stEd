using UnityEngine;
using System.Collections;

// BEGIN 3d_shiptarget
public class ShipTarget : MonoBehaviour {

	// The sprite to use for the target reticle.
	public Sprite targetImage;

	void Start () {

		// Register a new indicator that tracks this object, using a
		// yellow color and the custom sprite.
		IndicatorManager.instance.AddIndicator(gameObject, 
			Color.yellow, targetImage);
	}

}
// END 3d_shiptarget