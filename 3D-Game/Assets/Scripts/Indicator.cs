using UnityEngine;
using System.Collections;

// BEGIN 3d_indicator
// Get access to the UI classes
using UnityEngine.UI;

public class Indicator : MonoBehaviour {

	// How far we should be from the screen edges.
	public int margin = 10;

	// Our image's tint colour.
	public Color color {
		set {
			GetComponent<Image>().color = value;
		}
		get {
			return GetComponent<Image>().color;
		}
	}
	
	// The object we're tracking.
	public Transform target;
	
	// Measure the distance from 'target' to this transform.
	public Transform showDistanceTo;

	// The label that shows the distance we're measuring.
	public Text distanceLabel;

	IEnumerator Start() {
		// Hide the label; it will be re-enabled in 
		// Update if we have a target
		distanceLabel.enabled = false;

		// On start, wait a frame before appearing to prevent visual glitches
		GetComponent<Image>().enabled = false;
		yield return new WaitForEndOfFrame();
		GetComponent<Image>().enabled = true;

	}

	// Update the indicator's position every frame
	void Update()
	{

		// Is our target gone? Then we should go too
		if (target == null) {
			Destroy (gameObject);
			return;
		}

		// If we have a target for calculating distance, then calculate it and
		// display it in the distanceLabel
		if (showDistanceTo != null) {

			// Show the label
			distanceLabel.enabled = true;

			// Calculate the distance
			var distance = (int)Vector3.Magnitude(
				showDistanceTo.position - target.position);

			// Show the distance in the label
			distanceLabel.text = distance.ToString() + "m";
		} else {
			// Don't show the label
			distanceLabel.enabled = false;
		}

		// Work out where in screen-space the object is
		var viewportPoint = Camera.main.WorldToViewportPoint(target.position);

		// Is the point behind us?
		if (viewportPoint.z < 0) {
			// Push it to the edges of the screen
			viewportPoint.z = 0;
			viewportPoint = viewportPoint.normalized;
			viewportPoint.x *= -Mathf.Infinity;
		} 

		// Work out where in view-space we should be
		var screenPoint = Camera.main.ViewportToScreenPoint(viewportPoint);

		// Clamp to screen edges
		screenPoint.x = Mathf.Clamp(screenPoint.x, margin, Screen.width - margin * 2);	
		screenPoint.y = Mathf.Clamp(screenPoint.y, margin, Screen.height - margin * 2);


		// Work out where in the canvas-space the view-space coordinate is
		var localPosition = new Vector2();
		RectTransformUtility.ScreenPointToLocalPointInRectangle(
			transform.parent.GetComponent<RectTransform>(), 
            screenPoint, 
            Camera.main, 
            out localPosition);

		// Update our position
		var rectTransform = GetComponent<RectTransform>();
		rectTransform.localPosition = localPosition;

	}
}
// END 3d_indicator