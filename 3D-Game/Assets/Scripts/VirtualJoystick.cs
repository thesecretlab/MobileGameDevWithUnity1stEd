using UnityEngine;
using System.Collections;

// BEGIN 3d_virtualjoystick
// Get access to the Event interfaces
using UnityEngine.EventSystems;

// Get access to UI elements
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, 
	IDragHandler, IEndDragHandler {	

	// The sprite that gets dragged around
	public RectTransform thumb;

	// The locations of the thumb and joystick when no dragging is happening
	private Vector2 originalPosition;
	private Vector2 originalThumbPosition;

	// The distance that the thumb has been dragged away from its
	// original position
	public Vector2 delta;

	void Start () {
		// When the joystick starts up, record the original positions
		originalPosition = this.GetComponent<RectTransform>().localPosition;
		originalThumbPosition = thumb.localPosition;

		// Disable the thumb so that it's not visible
		thumb.gameObject.SetActive(false);

		// Reset the delta to zero
		delta = Vector2.zero;
	}

	// Called when dragging starts
	public void OnBeginDrag (PointerEventData eventData) {

		// Make the thumb visible
		thumb.gameObject.SetActive(true);

		// Figure out where in world-space the drag started
		Vector3 worldPoint = new Vector3();
		RectTransformUtility.ScreenPointToWorldPointInRectangle(
			this.transform as RectTransform, 
            eventData.position, 
            eventData.enterEventCamera, 
            out worldPoint);


		// Place the joystick at that point
		this.GetComponent<RectTransform>().position = worldPoint;

		// Ensure that the thumb is in its original location,
		// relative to the joystick
		thumb.localPosition = originalThumbPosition;
	}

	// Called when the drag moves
	public void OnDrag (PointerEventData eventData) {

		// Work out where the drag is in world space now
		Vector3 worldPoint = new Vector3();
		RectTransformUtility.ScreenPointToWorldPointInRectangle(
			this.transform as RectTransform, 
            eventData.position, 
            eventData.enterEventCamera, 
            out worldPoint);

		// Place the thumb at that point
		thumb.position = worldPoint;


		// Calculate distance from original position
		var size = GetComponent<RectTransform>().rect.size;

		delta = thumb.localPosition;

		delta.x /= size.x / 2.0f;
		delta.y /= size.y / 2.0f;

		delta.x = Mathf.Clamp(delta.x, -1.0f, 1.0f);
		delta.y = Mathf.Clamp(delta.y, -1.0f, 1.0f);


	}

	// Called when dragging ends
	public void OnEndDrag (PointerEventData eventData) {
		// Reset the position of the joystick
		this.GetComponent<RectTransform>().localPosition = originalPosition;

		// Reset the distance to zero
		delta = Vector2.zero;

		// Hide the thumb
		thumb.gameObject.SetActive(false);
	}
}
// END 3d_virtualjoystick