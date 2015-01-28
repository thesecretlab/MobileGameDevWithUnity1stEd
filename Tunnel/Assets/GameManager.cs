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

	public bool gnomeInvincible { get; set; }
		

	public float delayAfterDeath = 1.0f;

	bool treasureCollected = false;

	void Start() {
		Reset ();
	}

	public void TreasureCollected() {
		treasureCollected = true;
		currentGnome.SetHoldingTreasure(true);
	}

	void StopGnome() {

		if (gnomeInvincible)
			return;

		rope.gameObject.SetActive(false);

		// if we have a current gnome, make that no longer be the player
		if (currentGnome != null) {
			currentGnome.gameObject.tag = "Untagged";
			
			// Find everything that's currently tagged "Player", and remove that tag
			foreach (Transform child in currentGnome.transform) {
				child.gameObject.tag = "Untagged";
			}

			currentGnome = null;
		}
	}

	void CreateNewGnome() {

		StopGnome();

		GameObject newGnome = (GameObject)Instantiate(gnomePrefab);
		currentGnome = newGnome.GetComponent<GnomeComponents>();
		
		currentGnome.transform.position = startingPoint.transform.position;

		rope.gameObject.SetActive(true);
		
		rope.connectedObject = currentGnome.ropeBody;

		cameraFollow.target = newGnome;
		
		rope.Reset();

	}

	public void Reset() {

		// Turn off the menus, turn on the gameplay UI
		gameOverMenu.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(false);
		gameplayMenu.gameObject.SetActive(true);

		var resetObjects = FindObjectsOfType<Reset>();

		// Find all Reset components and tell them to reset
		foreach (Reset r in resetObjects) {
			r.DoReset();
		}

		// If we have a gnome, then make it not hold the treasure
		if (currentGnome != null)
			currentGnome.SetHoldingTreasure(false);

		// Make a new gnome
		CreateNewGnome();

		treasureCollected = false;
		currentGnome.SetHoldingTreasure(false);
		


		fade.gameObject.SetActive(true);

		fade.SetAlpha(1.0f);
		fade.FadeTo(0.0f, 0.5f);



		Time.timeScale = 1.0f;
	}

	public void TrapTouched() {
		currentGnome.SetHoldingTreasure(false);
		currentGnome.DestroyGnome(GnomeComponents.DamageType.Slicing);

		if (gnomeInvincible)
			return;

		StopGnome();
		StartCoroutine("ResetAfterDelay");

	}

	IEnumerator ResetAfterDelay() {
		cameraFollow.target = null;
		yield return new WaitForSeconds(delayAfterDeath);
		Reset();
	}

	public void FireTrapTouched() {
		currentGnome.SetHoldingTreasure(false);
		currentGnome.DestroyGnome(GnomeComponents.DamageType.Burning);

		if (gnomeInvincible)
			return;
        
		StopGnome ();
		StartCoroutine("ResetAfterDelay");

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

	public void RestartGame() {
		Destroy(currentGnome.gameObject);
		Reset();
	}

}
