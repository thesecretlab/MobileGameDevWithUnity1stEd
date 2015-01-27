using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Reset : MonoBehaviour {

	public UnityEvent onReset;

	public void DoReset() {
		Debug.Log (string.Format ("{0} is resetting", name));
		onReset.Invoke();
	}
}
