using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
public class Fade : MonoBehaviour {

	float fadeTimeRemaining = 0.0f;
	float fadeSpeed = 0.0f;

	public void FadeTo(float alpha, float time) {
		float currentAlpha = this.GetComponent<Image>().color.a;

		float deltaAlpha = alpha - currentAlpha;

		fadeSpeed = deltaAlpha / time;

		fadeTimeRemaining = time;

	}


	public void SetAlpha(float alpha) {
		Color color = this.GetComponent<Image>().color;
		color.a = alpha;
		this.GetComponent<Image>().color = color;
	}

	
	void Update () {
		if (fadeTimeRemaining > 0) {
			fadeTimeRemaining -= Time.deltaTime;
			Color color = this.GetComponent<Image>().color;
			color.a += fadeSpeed * Time.deltaTime;
			this.GetComponent<Image>().color = color;
		}
	}
}
