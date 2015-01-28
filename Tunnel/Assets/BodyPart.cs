using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour {

	public Sprite detachedSprite;
	public Sprite burnedSprite;

	public void Detach() {
		if (detachedSprite != null) {
			GetComponent<SpriteRenderer>().sprite = detachedSprite;
		}

	}

	public void Burn() {
		if (burnedSprite != null) {
			GetComponent<SpriteRenderer>().sprite = burnedSprite;
		}

	}
}
