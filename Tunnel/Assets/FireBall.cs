using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public float speed = 6.0f;

	public Vector3 direction;

	public float lifetime = 5.0f;

	// Use this for initialization
	void Start () {

		StartCoroutine("RemoveAfterTimeout");

	}

	IEnumerator RemoveAfterTimeout() {
		yield return new WaitForSeconds(lifetime);
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.right * speed * Time.deltaTime);
	}
}
