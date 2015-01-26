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
	public GameObject startingPoint;
	public Rope rope;
	public Fade fade;

	public CameraFollow cameraFollow;

	GnomeComponents currentGnome;

	public GameObject gnomePrefab;

	public RectTransform mainMenu;
	public RectTransform gameplayMenu;
	public RectTransform gameOverMenu;

	bool treasureCollected = false;

	void Start() {
		Reset ();
	}

	public void TreasureCollected() {
		treasureCollected = true;
		treasure.SetActive(false);

		currentGnome.SetHoldingTreasure(true);
	}

	void CreateNewGnome() {

		// if we have a current gnome, make that no longer be the player
		if (currentGnome != null) {
			currentGnome.gameObject.tag = "Untagged";

			// Find everything that's currently tagged "Player", and remove that tag
			foreach (Transform child in currentGnome.transform) {
				child.gameObject.tag = "Untagged";
			}
		}

		GameObject newGnome = (GameObject)Instantiate(gnomePrefab);
		currentGnome = newGnome.GetComponent<GnomeComponents>();
		
		currentGnome.transform.position = startingPoint.transform.position;
		
		rope.connectedObject = currentGnome.ropeBody;

		cameraFollow.target = newGnome;
		
		rope.Reset();

	}

	public void Reset() {

		gameOverMenu.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(false);

		gameplayMenu.gameObject.SetActive(true);

		treasureCollected = false;
		treasure.SetActive(true);

		CreateNewGnome();

		fade.gameObject.SetActive(true);

		fade.SetAlpha(1.0f);
		fade.FadeTo(0.0f, 0.5f);

		currentGnome.SetHoldingTreasure(false);

		Time.timeScale = 1.0f;
	}

	public void TrapTouched() {
		currentGnome.DestroyGnome();
		Reset ();
	}

	public void ExitReached() {
		if (treasureCollected == true) {
			ShowCompleteMenu();
		}
	}

	public void ShowCompleteMenu() {
		Time.timeScale = 0.0f;
		gameOverMenu.gameObject.SetActive(true);
		gameplayMenu.gameObject.SetActive(false);
	}

	public void SetPaused(bool active) {

		if (active) {
			Time.timeScale = 0.0f;
			mainMenu.gameObject.SetActive(true);
			gameplayMenu.gameObject.SetActive(false);
		} else {
			Time.timeScale = 1.0f;
			mainMenu.gameObject.SetActive(false);
			gameplayMenu.gameObject.SetActive(true);
		}


	}

}
