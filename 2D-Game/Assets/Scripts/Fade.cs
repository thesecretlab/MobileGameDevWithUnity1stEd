using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Fades a UI image from its current alpha value to a target value.
// Used when the game resets.
[RequireComponent (typeof(Image))]
public class Fade : MonoBehaviour {

	// These private fields are calculated when FadeTo is called:

	// How many seconds are left before we're done fading?
	float fadeTimeRemaining = 0.0f;

	// How much should the opacity change, per frame?
	float fadeSpeed = 0.0f;

	// Trigged by the Reset system
	public void FadeIn() {

		// Make the Fade go full-alpha and then fade out over 0.5 
        // seconds
		SetAlpha(1.0f);
		FadeTo(0.0f, 0.5f);

	}

	// Begins fading from current alpha to a target alpha, over 'time' 
    // seconds
	public void FadeTo(float alpha, float time) {

		// Work out our current alpha value
		float currentAlpha = this.GetComponent<Image>().color.a;

		// Work out how much we need to change by
		float deltaAlpha = alpha - currentAlpha;

		// Divide by number of seconds to work out how much alpha
		// to change every frame
		fadeSpeed = deltaAlpha / time;

		// Reset the counter
		fadeTimeRemaining = time;

	}

	// Sets the alpha of the image to a target immediately
	public void SetAlpha(float alpha) {
		Color color = this.GetComponent<Image>().color;
		color.a = alpha;
		this.GetComponent<Image>().color = color;
	}

	// Every frame, if there's time remaining on the counter,
	// update the alpha
	void Update () {
		if (fadeTimeRemaining > 0) {
			fadeTimeRemaining -= Time.deltaTime;

			Color color = this.GetComponent<Image>().color;
			color.a += fadeSpeed * Time.deltaTime;
			this.GetComponent<Image>().color = color;
		}
	}
}
