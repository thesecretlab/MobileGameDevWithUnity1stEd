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

	bool treasureCollected = false;

	void TreasureCollected() {
		treasureCollected = true;
		treasure.SetActive(false);
	}

	void Reset() {
		treasureCollected = false;
		treasure.SetActive(true);
	}

	void ExitReached() {
		if (treasureCollected == true) {
			Reset();
		}
	}

}
