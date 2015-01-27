using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class FireBall : MonoBehaviour {

	public Sprite[] possibleSprites;

	public float speed = 6.0f;

	public float lifetime = 5.0f;

	// Use this for initialization
	void Start () {

		var sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];

		GetComponent<SpriteRenderer>().sprite = sprite;

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
