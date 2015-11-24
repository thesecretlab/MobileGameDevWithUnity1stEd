using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Indicator : MonoBehaviour {

	public int margin = 10;

	public Color color {
		set {
			GetComponent<Image>().color = value;
		}
		get {
			return GetComponent<Image>().color;
		}
	}
	
	public Transform target;
	public Transform showDistanceTo;

	public Text distanceLabel;

	IEnumerator Start() {
		distanceLabel.enabled = false;
		GetComponent<Image>().enabled = false;
		yield return new WaitForEndOfFrame();
		GetComponent<Image>().enabled = true;
	}

	void Update()
	{

		if (target == null) {
			Destroy (gameObject);
			return;
		}

		if (showDistanceTo != null) {
			distanceLabel.enabled = true;
			var distance = (int)Vector3.Magnitude(showDistanceTo.position - target.position);

			distanceLabel.text = distance.ToString() + "m";
		} else {
			distanceLabel.enabled = false;
		}

		// Work out where in screen-space the object is
		var viewportPoint = Camera.main.WorldToViewportPoint(target.position);

		if (viewportPoint.z < 0) {
			// It's behind us - push it to the edges
			viewportPoint.z = 0;
			viewportPoint = viewportPoint.normalized;
			viewportPoint.x *= -Mathf.Infinity;
		} 

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