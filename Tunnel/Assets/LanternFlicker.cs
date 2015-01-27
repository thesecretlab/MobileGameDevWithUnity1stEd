using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class LanternFlicker : MonoBehaviour {


	[Range(0.0f, 1.0f)]
	public float minimumBrightness = 0.5f;
	[Range(0.0f, 1.0f)]
	public float maximumBrightness = 1.0f;

	[Range(0.0f, 1.0f)]
	public float flickerStrength = 0.1f;

	// Update is called once per frame
	void Update () {

		var spriteRenderer = GetComponent<SpriteRenderer>();

		var flickerAmount = Random.Range(minimumBrightness, maximumBrightness);

		var color = spriteRenderer.color;

		color.a *= 1.0f - flickerStrength;
		color.a += flickerAmount * flickerStrength;

		spriteRenderer.color = color;
	}
}
