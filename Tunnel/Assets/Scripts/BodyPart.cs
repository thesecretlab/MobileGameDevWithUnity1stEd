using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour {

	public Sprite detachedSprite;
	public Sprite burnedSprite;
	public Transform bloodFountainOrigin;

	bool detached = false;

	public void Detach() {
		detached = true;

		this.tag = "Untagged";

		transform.SetParent(null, true);
	}

	public void Update() {
		if (detached == false) {
			return;
		}

		var rigidbody = GetComponent<Rigidbody2D>();

		if (rigidbody.IsSleeping()) {

			foreach (Joint2D joint in GetComponentsInChildren<Joint2D>()) {
				Destroy (joint);
			}

			foreach (Rigidbody2D body in GetComponentsInChildren<Rigidbody2D>()) {
				Destroy (body);
			}

			foreach (Collider2D collider in GetComponentsInChildren<Collider2D>()) {
				Destroy (collider);
			}

			Destroy (this);
		}
	}

	public void ApplyDamageSprite(GnomeComponents.DamageType damageType) {

		Sprite spriteToUse = null;

		switch (damageType) {

		case GnomeComponents.DamageType.Burning:
			spriteToUse = burnedSprite;

			break;

		case GnomeComponents.DamageType.Slicing:
			spriteToUse = detachedSprite;

			break;
		}

		if (spriteToUse != null) {
			GetComponent<SpriteRenderer>().sprite = spriteToUse;
		}

	}

}
