using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float startTime = 10.0f;
	private float timeRemaining = 0.0f;

	public Text timeRemainingLabel;

	public void StartClock() {
		timeRemaining = startTime;
	}

	void Update() {
		if (GameManager.instance.gameIsPlaying == false) {
			return;
		}

		timeRemaining -= Time.deltaTime;
		
		if (timeRemaining <= 0.0f) {
			GameManager.instance.GameOver();
			timeRemaining = 0;
		}

		if (timeRemaining >= 0.0f) {
			var labelString = string.Format("{0}", timeRemaining);
			timeRemainingLabel.text = labelString;
		}

	}
}
