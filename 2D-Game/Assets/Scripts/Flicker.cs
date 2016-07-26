using UnityEngine;
using System.Collections;

// Randomly varies the alpha component of a sprite renderer.
[RequireComponent(typeof(SpriteRenderer))]
public class Flicker : MonoBehaviour {

	// The minimum alpha value
	[Range(0.0f, 1.0f)]
	public float minimumBrightness = 0.5f;

	// The maximum value value
	[Range(0.0f, 1.0f)]
	public float maximumBrightness = 1.0f;

	// How much flicker there should be. 0 = zero flicker, 1 = LOTS of 
    // flicker.
	[Range(0.0f, 1.0f)]
	public float flickerStrength = 0.1f;

	void Update () {

		// Get the sprite renderer
		var spriteRenderer = GetComponent<SpriteRenderer>();

		// Work out a new alpha value
		var flickerAmount = Random.Range(minimumBrightness, 
maximumBrightness);

		var color = spriteRenderer.color;

		// Use flickerStrength to work out how much of flickerAmount 
        // should affect the current alpha value. 
        // 1.0 = all of it, 0.0 = none.

		color.a *= 1.0f - flickerStrength;
		color.a += flickerAmount * flickerStrength;

		// Apply the new alpha value
		spriteRenderer.color = color;
	}
}
