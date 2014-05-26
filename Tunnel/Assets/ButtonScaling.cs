using UnityEngine;
using System.Collections;

public class ButtonScaling : MonoBehaviour {

	void Start () {

		// iPhone screen
		float contentScale = Screen.width / 320.0f ;

		// Adjust to account for size
		Rect inset = guiTexture.pixelInset;

		inset.x *= contentScale;
		inset.y *= contentScale;
		inset.height *= contentScale;
		inset.width *= contentScale;

		guiTexture.pixelInset = inset;

	}

	void OnMouseDown() {
		Debug.Log("Hi");
	}

}
