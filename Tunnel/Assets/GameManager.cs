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
	public GameObject mainMenu;
	public GameObject completeMenu;

	public CameraFollow cameraFollow;

	GnomeComponents currentGnome;

	public GameObject gnomePrefab;

	bool treasureCollected = false;

	void Start() {
		Reset ();
	}

	void TreasureCollected() {
		treasureCollected = true;
		treasure.SetActive(false);

		currentGnome.treasure.SetActive(true);
	}

	void CreateNewGnome() {

		// if we have a current gnome, make that no longer be the player
		if (currentGnome != null) {
			currentGnome.gameObject.tag = null;
		}

		GameObject newGnome = (GameObject)Instantiate(gnomePrefab);
		currentGnome = newGnome.GetComponent<GnomeComponents>();
		
		currentGnome.transform.position = startingPoint.transform.position;
		
		rope.connectedObject = currentGnome.ropeBody;

		cameraFollow.target = newGnome;
		
		rope.Reset();

	}

	void Reset() {
		treasureCollected = false;
		treasure.SetActive(true);

		CreateNewGnome();

		fade.gameObject.SetActive(true);

		fade.SetAlpha(1.0f);
		fade.FadeTo(0.0f, 0.5f);

		mainMenu.SetActive(false);
		completeMenu.SetActive(false);

		currentGnome.treasure.SetActive(false);

		Time.timeScale = 1.0f;
	}

	void TrapTouched() {
		currentGnome.DestroyGnome();
		Reset ();
	}

	void ExitReached() {
		if (treasureCollected == true) {
			ShowCompleteMenu();
		}
	}

	void ShowCompleteMenu() {
		Time.timeScale = 0.0f;
		completeMenu.SetActive(true);
	}

	void ShowMenu() {
		Time.timeScale = 0.0f;
		mainMenu.SetActive(true);
	}

	void ResetGame() {
		Reset ();
	}

	void ResumeGame() {
		Time.timeScale = 1.0f;
		mainMenu.SetActive(false);
	}

}
