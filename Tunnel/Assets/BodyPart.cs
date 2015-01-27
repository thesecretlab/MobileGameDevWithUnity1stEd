using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour {

	public Sprite detachedSprite;
	public Sprite burnedSprite;

	public float removalTime = 3.0f;

	public void Detach() {
		if (detachedSprite != null) {
			GetComponent<SpriteRenderer>().sprite = detachedSprite;
		}

		StartCoroutine("RemoveAfterTime");
	}

	IEnumerator RemoveAfterTime() {
		yield return new WaitForSeconds(removalTime);
		Destroy (gameObject);
	}

	public void Burn() {
		if (burnedSprite != null) {
			GetComponent<SpriteRenderer>().sprite = burnedSprite;
		}

		var fade = gameObject.AddComponent<Fade>();

		StartCoroutine("RemoveAfterTime");
	}
}
