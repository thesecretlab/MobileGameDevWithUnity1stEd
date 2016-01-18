using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public RectTransform pad;

	private Vector2 originalPosition;
	private Vector2 originalPadPosition;

	private Vector2 startScreenPoint;

	public Vector2 delta;

	// Use this for initialization
	void Start () {
		originalPosition = this.GetComponent<RectTransform>().localPosition;
		originalPadPosition = pad.localPosition;

		pad.gameObject.SetActive(false);

		delta = Vector2.zero;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBeginDrag (PointerEventData eventData) {

		pad.gameObject.SetActive(true);

		Vector3 worldPoint = new Vector3();
		RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, 
		                                                        eventData.position, 
		                                                        eventData.enterEventCamera, 
		                                                        out worldPoint);



		this.GetComponent<RectTransform>().position = worldPoint;
		pad.localPosition = originalPadPosition;
	}

	public void OnDrag (PointerEventData eventData) {
		Vector3 worldPoint = new Vector3();
		RectTransformUtility.ScreenPointToWorldPointInRectangle(this.transform as RectTransform, 
		                                                        eventData.position, 
		                                                        eventData.enterEventCamera, 
		                                                        out worldPoint);

		pad.position = worldPoint;

		var size = GetComponent<RectTransform>().rect.size;

		delta = pad.localPosition;

		delta.x /= size.x / 2.0f;
		delta.y /= size.y / 2.0f;

		delta.x = Mathf.Clamp(delta.x, -1.0f, 1.0f);
		delta.y = Mathf.Clamp(delta.y, -1.0f, 1.0f);


	}

	public void OnEndDrag (PointerEventData eventData) {
		this.GetComponent<RectTransform>().localPosition = originalPosition;

		delta = Vector2.zero;

		pad.gameObject.SetActive(false);
	}
}
// END 3d_virtualjoystick