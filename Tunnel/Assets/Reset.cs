using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Reset : MonoBehaviour {

	public UnityEvent onReset;

	public void DoReset() {
		onReset.Invoke();
	}
}
