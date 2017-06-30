using UnityEngine;
using System.Collections;

// BEGIN 3d_damagetaking
public class DamageTaking : MonoBehaviour {

	// The number of hit points this object has
	public int hitPoints = 10;

	// If we're destroyed, create one of these at 
	// our current position
	public GameObject destructionPrefab;

	// Should we end the game if this object is destroyed?
	public bool gameOverOnDestroyed = false;

	// Called by other objects (like Asteroids and Shots) 
	// to take damage
	public void TakeDamage(int amount) {

		// Report that we got hit
		Debug.Log(gameObject.name + " damaged!");

		// Deduct the amount from our hit points
		hitPoints -= amount;

		// Are we dead?
		if (hitPoints <= 0) {

			// Log it
			Debug.Log(gameObject.name + " destroyed!");

			// Remove ourselves from the game
			Destroy(gameObject);

			// Do we have a destruction prefab to use?
			if (destructionPrefab != null) {

				// Create it at our current position and
				// with our rotation.
				Instantiate(destructionPrefab, 
                    transform.position, transform.rotation);
			}

			// BEGIN 3d_damagetaking_gamemanager
			// If we should end the game now, call the GameManager's GameOver method.
			if (gameOverOnDestroyed == true) {
				GameManager.instance.GameOver();
			}
			// END 3d_damagetaking_gamemanager
		}

	}

}
// END 3d_damagetaking