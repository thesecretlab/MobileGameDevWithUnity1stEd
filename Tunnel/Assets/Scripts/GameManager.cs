using UnityEngine;
using System.Collections;

// Manages the game state.
public class GameManager : Singleton<GameManager> {

	// The location where the gnome should appear.
	public GameObject startingPoint;

	// The rope object, which lowers and raises the gnome.
	public Rope rope;

	// The fade-out script (triggered when the game resets)
	public Fade fade;

	// The follow script, which will follow the gnome
	public CameraFollow cameraFollow;

	// The 'current' gnome (as opposed to all those dead ones)
	Gnome currentGnome;

	// The prefab to instantiate when we need a new gnome
	public GameObject gnomePrefab;

	// The UI component that contains the 'restart' and 'resume' buttons
	public RectTransform mainMenu;

	// The UI component that contains the 'up', 'down' and 'menu' buttons
	public RectTransform gameplayMenu;

	// The UI component that contains the 'you win!' screen
	public RectTransform gameOverMenu;

	// If true, ignore all damage (but still show damage effects)
	public bool gnomeInvincible { get; set; }

	// How long to wait after dying before creating a new gnome
	public float delayAfterDeath = 1.0f;

	// The sound to play when the gnome dies
	public AudioClip gnomeDiedSound;

	// The sound to play when the game is won
	public AudioClip gameOverSound;

	void Start() {
		// When the game starts, call Reset to set up the gnome.
		Reset ();
	}

	// Called when the gnome picks up the treasure.
	public void TreasureCollected() {
		// Tell the currentGnome that it should have the treasure.
		currentGnome.holdingTreasure = true;
	}


	void StopGnome() {

		// Don't actually do anything if the gnome is invincible
		if (gnomeInvincible)
			return;

		// Hide the rope
		rope.gameObject.SetActive(false);

		// Stop tracking the gnome
		cameraFollow.target = null;

		// If we have a current gnome, make that no longer be the player
		if (currentGnome != null) {

			// This gnome is no longer holding the treasure
			currentGnome.holdingTreasure = false;

			// Mark this object as not the player (so that colliders won't report when
			// the object hits them)
			currentGnome.gameObject.tag = "Untagged";
			
			// Find everything that's currently tagged "Player", and remove that tag
			foreach (Transform child in currentGnome.transform) {
				child.gameObject.tag = "Untagged";
			}

			// Mark ourselves as not currently having a gnome
			currentGnome = null;
		}
	}

	void CreateNewGnome() {

		// Remove the current gnome, if there is one
		StopGnome();

		// Create a new gnome object, and make it be our currentGnome
		GameObject newGnome = (GameObject)Instantiate(gnomePrefab, 
		                                              startingPoint.transform.position, 
		                                              Quaternion.identity);		                                              
		currentGnome = newGnome.GetComponent<Gnome>();

		// Make the rope visible
		rope.gameObject.SetActive(true);

		// Connect the rope's trailing end to whichever rigidbody the Gnome object wants
		// (e.g. his foot)
		rope.connectedObject = currentGnome.ropeBody;

		// Reset the rope's length to the default
		rope.ResetLength();

		// Tell the cameraFollow to start tracking the new gnome object
		cameraFollow.target = newGnome;

	}

	// Reset the entire game.
	public void Reset() {

		// Turn off the menus, turn on the gameplay UI
		gameOverMenu.gameObject.SetActive(false);
		mainMenu.gameObject.SetActive(false);
		gameplayMenu.gameObject.SetActive(true);

		// Find all Resettable components and tell them to reset
		var resetObjects = FindObjectsOfType<Resettable>();

		foreach (Resettable r in resetObjects) {
			r.Reset();
		}

		// Make a new gnome
		CreateNewGnome();

		// Make the Fade go full-alpha and then fade out over 0.5 seconds
		fade.SetAlpha(1.0f);
		fade.FadeTo(0.0f, 0.5f);

		// Un-pause the game
		Time.timeScale = 1.0f;
	}

	// Called when the player touches a trap
	public void TrapTouched() {
		KillGnome(Gnome.DamageType.Slicing);
	}

	// Called when the player touches a fire trap
	public void FireTrapTouched() {
		KillGnome(Gnome.DamageType.Burning);
	}

	// Kills the gnome.
	void KillGnome(Gnome.DamageType damageType) {

		// If we have an audio source, play "gnome died" sound
		var audio = GetComponent<AudioSource>();
		if (audio) {
			audio.PlayOneShot(this.gnomeDiedSound);
		}

		// Tell the gnome that it died
		currentGnome.DestroyGnome(damageType);


		// If we're not invincible, reset the game and make
		// the gnome not be the current player.
		if (gnomeInvincible == false) {
			StartCoroutine("ResetAfterDelay");
			StopGnome();
		}
	}

	// Called when gnome dies.
	IEnumerator ResetAfterDelay() {

		// Wait for delayAfterDeath seconds, then call Reset
		yield return new WaitForSeconds(delayAfterDeath);
		Reset();
	}

	// Called when the player touches the exit.
	public void ExitReached() {
		// If we have a player, and that player is holding treasure, game over!
		if (currentGnome != null && currentGnome.holdingTreasure == true) {

			// If we have an audio source, play the game over sound
			var audio = GetComponent<AudioSource>();
			if (audio) {
				audio.PlayOneShot(this.gameOverSound);
			}

			// Pause the game
			Time.timeScale = 0.0f;

			// Turn off the game over menu, and turn on the game over screen!
			gameOverMenu.gameObject.SetActive(true);
			gameplayMenu.gameObject.SetActive(false);
		}
	}

	// Called when the Menu button is tapped, and when the Resume game is tapped.
	public void SetPaused(bool paused) {

		// If we're paused, stop time and enable the menu (and disable the game overlay)
		if (paused) {
			Time.timeScale = 0.0f;
			mainMenu.gameObject.SetActive(true);
			gameplayMenu.gameObject.SetActive(false);
		} else {
			// If we're not paused, resume time and disable the menu (and enable the game overlay)
			Time.timeScale = 1.0f;
			mainMenu.gameObject.SetActive(false);
			gameplayMenu.gameObject.SetActive(true);
		}
	}

	// Called when the Restart button is tapped. 
	public void RestartGame() {

		// Immediately remove the gnome (instead of killing it)
		Destroy(currentGnome.gameObject);
		currentGnome = null;

		// Now reset the game to create a new gnome.
		Reset();
	}

}
