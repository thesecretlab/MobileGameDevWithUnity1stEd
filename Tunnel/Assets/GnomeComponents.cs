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

	void DetachComponents() {
		// find all child objects, and disconnect their joints
		
		foreach (Transform part in transform) {
			
			foreach (Joint2D joint in part.GetComponentsInChildren<Joint2D>()) {
				if (Random.Range(0, 2) == 0) {
					Destroy (joint);
				}
				
			}
			
		}

		// Add a Remove-After-Delay component
		var remove = gameObject.AddComponent<RemoveAfterDelay>();
		remove.delay = delayBeforeRemoving;

		// Add the ghost

		Instantiate(ghostPrefab, transform.position, Quaternion.identity);

	}

	public void DestroyGnome() {

		dead = true;

		foreach (BodyPart part in GetComponentsInChildren<BodyPart>()) {
			part.Detach();
		}

		DetachComponents();

		if (deathPrefab != null) {
			Instantiate(deathPrefab, transform.position, transform.rotation);
		}

	}

	public void BurnGnome() {

		dead = true;

		foreach (BodyPart part in GetComponentsInChildren<BodyPart>()) {
			part.Burn();
		}

		DetachComponents();
	}


}
