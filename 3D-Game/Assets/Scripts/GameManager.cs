using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {

	public GameObject shipPrefab;
	public Transform shipStartPosition;
	private GameObject _currentShip;
	public GameObject currentShip {
		get { return _currentShip; }
	}

	public GameObject spaceStationPrefab;
	public Transform spaceStationStartPosition;
	private GameObject _currentSpaceStation;
	public GameObject currentSpaceStation {
		get { return _currentSpaceStation; }
	}

	public Boundary boundary;

	public SmoothFollow cameraFollow;
	public Timer timer;

	public GameObject warningUI;

	private bool _gameIsPlaying = false;

	public bool gameIsPlaying {
		get {
			return _gameIsPlaying;
		}
	}

	void ShowUI(GameObject newUI) {
		GameObject[] allUI = {inGameUI, pausedUI, gameOverUI, mainMenuUI};

		foreach (GameObject UIToHide in allUI) {
			UIToHide.SetActive(false);
		}

		newUI.SetActive(true);
	}

	public GameObject inGameUI;
	public GameObject pausedUI;
	public GameObject gameOverUI;
	public GameObject mainMenuUI;

	public AsteroidSpawner asteroidSpawner;

	private bool _paused;
	public bool paused {
		set {
			_paused = value;

			asteroidSpawner.spawnAsteroids = !_paused;
		}
	}

	void Start() {
		ShowMainMenu();
	}

	public void ShowMainMenu() {
		// TODO: end game
		ShowUI(mainMenuUI);

		_gameIsPlaying = false;

		asteroidSpawner.spawnAsteroids = false;
	}


	public void StartGame() {
		ShowUI(inGameUI);

		_gameIsPlaying = true;

		if (_currentShip != null) {
			Destroy(_currentShip);
		}

		if (_currentSpaceStation != null) {
			Destroy(_currentSpaceStation);
		}

		_currentShip = Instantiate(shipPrefab);
		_currentShip.transform.position = shipStartPosition.position;
		_currentShip.transform.rotation = shipStartPosition.rotation;

		_currentSpaceStation = Instantiate(spaceStationPrefab);
		_currentSpaceStation.transform.position = spaceStationStartPosition.position;
		_currentSpaceStation.transform.rotation = spaceStationStartPosition.rotation;

		cameraFollow.target = _currentShip.transform;

		asteroidSpawner.spawnAsteroids = true;
		asteroidSpawner.target = _currentSpaceStation.transform;

		timer.StartClock();
	}



	public void GameOver() {
		ShowUI(gameOverUI);

		_gameIsPlaying = false;

		if (_currentShip != null)
			Destroy (_currentShip);

		if (_currentSpaceStation != null)
			Destroy (_currentSpaceStation);

		warningUI.SetActive(false);

		asteroidSpawner.spawnAsteroids = false;
		asteroidSpawner.DestroyAllAsteroids();

		// TODO: shut down all spawners
	}

	public void SetPaused(bool paused) {

		inGameUI.SetActive(!paused);
		pausedUI.SetActive(paused);

		if (paused) {
			Time.timeScale = 0.0f;
		} else {
			Time.timeScale = 1.0f;
		}
	}

	public void Update() {

		if (_currentShip == null)
			return;

		// If the ship is outside the Boundary's Destroy Radius,
		// game over. If it's within the Destroy Radius, but outside
		// the Warning radius, show the Warning UI. If it's within both,
		// don't show the Warning UI.

		float distance = Vector3.Magnitude(_currentShip.transform.position -
		                                   boundary.transform.position);

		if (distance > boundary.destroyRadius) {
			GameOver();
		} else if (distance > boundary.warningRadius) {
			// Enable the Warning UI
			warningUI.SetActive(true);
		} else {
			// It's not past the warning threshold, so don't show the warning ui
			warningUI.SetActive(false);
		}


	}




}
