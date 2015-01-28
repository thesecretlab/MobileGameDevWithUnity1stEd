using UnityEngine;
using System.Collections;

public class GnomeComponents : MonoBehaviour {

	public Rigidbody2D ropeBody;

	public Sprite armHoldingEmpty;
	public Sprite armHoldingTreasure;

	public SpriteRenderer holdingArm;

	public GameObject deathPrefab;
	public GameObject ghostPrefab;

	public float delayBeforeRemoving = 3.0f;

	public GameObject bloodFountainPrefab;

	bool dead = false;

	public void SetHoldingTreasure(bool holding) {
		Debug.Break ();
		Debug.Log (StackTraceUtility.ExtractStackTrace());
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

		if (deathPrefab != null) {
			Instantiate(deathPrefab, transform.position, transform.rotation);
        }

		if (GameManager.instance.gnomeInvincible)
			return;

		SetHoldingTreasure(false);

		dead = true;

		// find all child objects, and randomly disconnect their joints
		foreach (BodyPart part in GetComponentsInChildren<BodyPart>()) {

			bool shouldDetach = Random.Range (0, 2) == 0;

			if (shouldDetach) {

				switch (type) {
				case DamageType.Slicing:

					part.Detach ();

					if (part.bloodFountainOrigin != null) {
						// Attach a blood fountain for this detached part
						GameObject fountain = Instantiate(bloodFountainPrefab, 
						                                  part.bloodFountainOrigin.position, 
						                                  part.bloodFountainOrigin.rotation) as GameObject;

						fountain.transform.SetParent(this.transform, true);
					}

					break;

				case DamageType.Burning:

					part.Burn ();
					break;
				}

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
