using UnityEngine;
using System.Collections;

public class RemoveAfterDelay : MonoBehaviour {

	public float delay = 1.0f;

	// Use this for initialization
	void Start () {
		StartCoroutine("Remove");
	}

	IEnumerator Remove() {
		yield return new WaitForSeconds(delay);
		Destroy (gameObject);
	}
}
