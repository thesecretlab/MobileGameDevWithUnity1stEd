using UnityEngine;
using System.Collections;

public class ShipTarget : MonoBehaviour {

	public Sprite targetImage;

	// Use this for initialization
	void Start () {
		IndicatorManager.instance.AddLabel(gameObject, Color.yellow, targetImage);
	}

}
