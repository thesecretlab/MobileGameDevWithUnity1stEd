using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private static GameManager _instance;
	
	public static GameManager instance {
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<GameManager>();
			}
			return _instance;
		}
	}

	public GameObject treasure;
	public GameObject gnome;
	public GameObject startingPoint;
	public Rope rope;
	public Fade fade;

	bool treasureCollected = false;

	void Start() {
		Reset ();
	}

	void TreasureCollected() {
		treasureCollected = true;
		treasure.SetActive(false);
	}

	void Reset() {
		treasureCollected = false;
		treasure.SetActive(true);

		gnome.transform.position = startingPoint.transform.position;

		rope.Reset();

		fade.SetAlpha(1.0f);
		fade.FadeTo(0.0f, 0.5f);
	}

	void TrapTouched() {

		Reset ();
	}

	void ExitReached() {
		if (treasureCollected == true) {
			Reset();
		}
	}

}
