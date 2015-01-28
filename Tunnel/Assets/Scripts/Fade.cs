using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

	float fadeTimeRemaining = 0.0f;
	float fadeSpeed = 0.0f;

	public void FadeTo(float alpha, float time) {
		float currentAlpha = this.GetComponent<Renderer>().material.color.a;

		float deltaAlpha = alpha - currentAlpha;

		fadeSpeed = deltaAlpha / time;

		fadeTimeRemaining = time;

	}


	public void SetAlpha(float alpha) {
		Color color = this.GetComponent<Renderer>().material.color;
		color.a = 1.0f;
		this.GetComponent<Renderer>().material.color = color;
	}

	
	void Update () {
		if (fadeTimeRemaining > 0) {
			fadeTimeRemaining -= Time.deltaTime;
			Color color = this.GetComponent<Renderer>().material.color;
			color.a += fadeSpeed * Time.deltaTime;
			this.GetComponent<Renderer>().material.color = color;
		}
	}
}
