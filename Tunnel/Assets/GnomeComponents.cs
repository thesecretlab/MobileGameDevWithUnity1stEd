using UnityEngine;
using System.Collections;

public class GnomeComponents : MonoBehaviour {

	public Rigidbody2D ropeBody;

	public Sprite armHoldingEmpty;
	public Sprite armHoldingTreasure;

	public SpriteRenderer holdingArm;

	public GameObject deathPrefab;
	public GameObject flameDeathPrefab;
	public GameObject ghostPrefab;

	public float delayBeforeRemoving = 3.0f;

	public GameObject bloodFountainPrefab;

	bool dead = false;

	public void SetHoldingTreasure(bool holding) {

		if (dead == true) {
			return;
		}

		if (holding) {
			holdingArm.sprite = armHoldingTreasure;
		} else {
			holdingArm.sprite = armHoldingEmpty;
		}
	}

	public enum DamageType {
		Slicing,
		Burning
	}

	public void DestroyGnome(DamageType type) {

		switch (type) {

		case DamageType.Burning:
			if (flameDeathPrefab != null) {
				Instantiate(flameDeathPrefab, transform.position, transform.rotation);
			}
			break;

		case DamageType.Slicing:
			if (deathPrefab != null) {
				Instantiate(deathPrefab, transform.position, transform.rotation);
			}
			break;
		}

		if (GameManager.instance.gnomeInvincible)
			return;

		SetHoldingTreasure(false);

		dead = true;

		// find all child objects, and randomly disconnect their joints
		foreach (BodyPart part in GetComponentsInChildren<BodyPart>()) {

			if (type == DamageType.Burning) {
				// 1 in 3 chance of burning
				bool shouldBurn = Random.Range (0, 2) == 0;
				if (shouldBurn) {
					part.Burn();
				}
			}

			// 1 in 3 chance of separating from body
			bool shouldDetach = Random.Range (0, 2) == 0;

			if (shouldDetach) {

				// If we're separating, and the damage type was Slicing,
				// add a blood fountain

				if (type == DamageType.Slicing) {
					part.Detach ();

					if (part.bloodFountainOrigin != null) {
						// Attach a blood fountain for this detached part
						GameObject fountain = Instantiate(bloodFountainPrefab, 
						                                  part.bloodFountainOrigin.position, 
						                                  part.bloodFountainOrigin.rotation) as GameObject;

						fountain.transform.SetParent(this.transform, true);
					}

					break;
				}

				// Disconnect this object
				foreach (Joint2D joint in part.GetComponentsInChildren<Joint2D>()) {
					Destroy (joint);
				}
			}
		}

		// Add a Remove-After-Delay component to this object
		var remove = gameObject.AddComponent<RemoveAfterDelay>();
		remove.delay = delayBeforeRemoving;
		
		// Add the ghost		
		Instantiate(ghostPrefab, transform.position, Quaternion.identity);

	}


}
