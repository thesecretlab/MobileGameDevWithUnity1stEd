using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Indicator : MonoBehaviour {

	// How far away from the edges of the screen the indicator should be
	public int margin = 10;

	// Create a property that allows other objects to set the colour
	// of the sprite
	public Color color {
		set {
			GetComponent<Image>().color = value;
		}
		get {
			return GetComponent<Image>().color;
		}
	}

	// The object that we're tracking the location of
	public Transform target;

	// The object from which we're calculating the distance to
	// 'target' 
	public Transform showDistanceTo;

	// The label that shows the distance
	public Text distanceLabel;

	IEnumerator Start() {
		// Disable the distance label
		distanceLabel.enabled = false;

		// Wait one frame before the indicator appears,
		// so that Update() has run at least once to sprite
		// at the right time
		GetComponent<Image>().enabled = false;
		yield return new WaitForEndOfFrame();
		GetComponent<Image>().enabled = true;
	}

	void Update()
	{

		// If we don't have a target (or it was destroyed),
		// remove this indicator
		if (target == null) {
			Destroy (gameObject);
			return;
		}

		// Update the label with the distance from
		// our target object to the "showDistanceTo" object,
		// but only if showDistanceTo is actually an object
		if (showDistanceTo != null) {

			distanceLabel.enabled = true;
			var distance = (int)Vector3.Magnitude(showDistanceTo.position - target.position);
			distanceLabel.text = distance.ToString() + "m";

		} else {
			distanceLabel.enabled = false;
		}

		// Work out where in screen-space the object is
		var viewportPoint = Camera.main.WorldToViewportPoint(target.position);

		// Check to see if this object is "behind" the screen
		if (viewportPoint.z < 0) {
			// It's behind us - push it to the edges of the screen
			viewportPoint.z = 0;
			viewportPoint = viewportPoint.normalized;
			viewportPoint.x *= -Mathf.Infinity;
		} 

		// Convert this updated point to screen coordinates
		var screenPoint = Camera.main.ViewportToScreenPoint(viewportPoint);

		// Clamp to screen edges
		screenPoint.x = Mathf.Clamp(screenPoint.x, margin, Screen.width - margin * 2);	
		screenPoint.y = Mathf.Clamp(screenPoint.y, margin, Screen.height - margin * 2);

		// Work out where in the canvas we should be
		var localPosition = new Vector2();
		RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), 
		                                                        screenPoint, 
		                                                        Camera.main, 
		                                                        out localPosition);

		// Update our position
		var rectTransform = GetComponent<RectTransform>();
		rectTransform.localPosition = localPosition;

	}
}