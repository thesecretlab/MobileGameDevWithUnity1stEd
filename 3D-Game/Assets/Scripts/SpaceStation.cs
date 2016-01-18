using UnityEngine;
using System.Collections;

// BEGIN 3d_spacestation
public class SpaceStation : MonoBehaviour {

	void Start () {
		IndicatorManager.instance.AddLabel(gameObject, Color.green);
	}

}
// END 3d_spacestation