using UnityEngine;
using System.Collections;

// BEGIN 3d_gamemanager
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


	public SmoothFollow cameraFollow;

	// BEGIN 3d_gamemanager_boundary_timer
	public Timer timer;
	// END 3d_gamemanager_boundary_timer

	// BEGIN 3d_gamemanager_boundary
	public Boundary boundary;

	public GameObject warningUI;
	// END 3d_gamemanager_boundary

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

		// BEGIN 3d_gamemanager_timer
		timer.StartClock();
		// END 3d_gamemanager_timer
	}



	public void GameOver() {
		ShowUI(gameOverUI);

		_gameIsPlaying = false;

		if (_currentShip != null)
			Destroy (_currentShip);

		if (_currentSpaceStation != null)
			Destroy (_currentSpaceStation);

		// BEGIN 3d_gamemanager_boundary
		warningUI.SetActive(false);
		// END 3d_gamemanager_boundary

		asteroidSpawner.spawnAsteroids = false;
		asteroidSpawner.DestroyAllAsteroids();
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

	// BEGIN 3d_gamemanager_boundary
	public void Update() {

		if (_currentShip == null)
			return;

		// If the ship is outside the Boundary's Destroy Radius,
		// game over. If it's within the Destroy Radius, but outside
		// the Warning radius, show the Warning UI. If it's within both,
		// don't show the Warning UI.

		float distance = 
			(_currentShip.transform.position 
				- boundary.transform.position).magnitude;

		if (distance > boundary.destroyRadius) {
			// The ship has gone beyond the destroy radius, so it's game over
			GameOver();
		} else if (distance > boundary.warningRadius) {
			// The ship has gone beyond the warning radius, so show the 
			// warning UI
			warningUI.SetActive(true);
		} else {
			// It's within the warning threshold, so don't show the warning UI
			warningUI.SetActive(false);
		}


	}
	// END 3d_gamemanager_boundary

}
// END 3d_gamemanager