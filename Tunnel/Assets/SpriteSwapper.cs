using UnityEngine;
using System.Collections;

public class SpriteSwapper : MonoBehaviour {

	private Sprite originalSprite;

	public Sprite spriteToUse;
	public SpriteRenderer spriteRenderer;

	public void SwapSprite() {
		if (spriteToUse != spriteRenderer.sprite) {
			originalSprite = spriteRenderer.sprite;
			spriteRenderer.sprite = spriteToUse;
		}
	}

	public void ResetSprite() {
		if (originalSprite != null) {
			Debug.Log ("Resetting sprite");
			spriteRenderer.sprite = originalSprite;
		}
	}
}
