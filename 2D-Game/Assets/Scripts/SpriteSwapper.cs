using UnityEngine;
using System.Collections;

// BEGIN 2d_spriteswapper
// Swaps out a sprite for another. For example, the treasure
// switches from 'treasure present' to 'treasure not present'.
public class SpriteSwapper : MonoBehaviour {


	// The sprite that should be displayed.
	public Sprite spriteToUse;

	// The sprite renderer that should use the new sprite.
	public SpriteRenderer spriteRenderer;

	// The original sprite. Used when ResetSprite is called.
	private Sprite originalSprite;

	// Swaps out the sprite.
	public void SwapSprite() {

		// If this sprite is different to the current sprite...
		if (spriteToUse != spriteRenderer.sprite) {

			// Store the previous store in originalSprite
			originalSprite = spriteRenderer.sprite;

			// Make the sprite renderer use the new sprite.
			spriteRenderer.sprite = spriteToUse;
		}
	}

	// Reverts back to the old sprite.
	public void ResetSprite() {

		// If we have a previous sprite...
		if (originalSprite != null) {
			// ...make the sprite renderer use it.
			spriteRenderer.sprite = originalSprite;
		}
	}
}
// END 2d_spriteswapper