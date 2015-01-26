using UnityEngine;
using System.Collections;

public class SpriteSwapper : MonoBehaviour {

	public Sprite spriteToUse;
	public SpriteRenderer spriteRenderer;

	public void SwapSprite() {
		spriteRenderer.sprite = spriteToUse;
	}
}
