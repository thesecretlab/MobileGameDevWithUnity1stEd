using UnityEngine;
using System.Collections;

public class GnomeComponents : MonoBehaviour {

	public Rigidbody2D ropeBody;

	public Sprite armHoldingEmpty;
	public Sprite armHoldingTreasure;

	public SpriteRenderer holdingArm;

	public void SetHoldingTreasure(bool holding) {

		if (holding) {
			holdingArm.sprite = armHoldingTreasure;
		} else {
			holdingArm.sprite = armHoldingEmpty;
		}

	}

	public void DestroyGnome() {
		// find all child objects, and disconnect their joints

		foreach (Transform part in transform) {

			foreach (Joint2D joint in part.GetComponentsInChildren<Joint2D>()) {
				Destroy (joint);
			}

		}
	}
}
