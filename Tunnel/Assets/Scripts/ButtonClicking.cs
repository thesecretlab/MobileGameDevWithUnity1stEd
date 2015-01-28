using UnityEngine;
using System.Collections;

public class ButtonClicking : MonoBehaviour {

	public GameObject target;

	public string mouseDownMessage;
	public string mouseUpMessage;

	void OnMouseDown() {
		if (mouseDownMessage != null && mouseDownMessage != "")
			target.SendMessage(mouseDownMessage, SendMessageOptions.RequireReceiver);
	}

	void OnMouseUp() {
		if (mouseUpMessage != null && mouseUpMessage != "")
			target.SendMessage(mouseUpMessage, SendMessageOptions.RequireReceiver);
	}
}
