using UnityEngine;
using System.Collections;

// BEGIN 3d_shiptarget
public class ShipTarget : MonoBehaviour {

	public Sprite targetImage;

	void Start () {
		IndicatorManager.instance.AddLabel(gameObject, Color.yellow, targetImage);
	}

}
// END 3d_shiptarget