using UnityEngine;
using System.Collections;

public class SpaceStation : MonoBehaviour {

	void Start () {
		IndicatorManager.instance.AddLabel(gameObject, Color.green);
	}

}
