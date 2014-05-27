using UnityEngine;
using System.Collections;

public class ButtonClicking : MonoBehaviour {

	public GameObject target;

	public string mouseDownMessage;
	public string mouseUpMessage;

	void OnMouseDown() {
		if (mouseDownMessage != null)
			target.SendMessage(mouseDownMessage, SendMessageOptions.RequireReceiver);
	}

	void OnMouseUp() {
		if (mouseDownMessage != null)
			target.SendMessage(mouseDownMessage, SendMessageOptions.RequireReceiver);
	}
}
