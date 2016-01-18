using UnityEngine;
using System.Collections;

// BEGIN 3d_damagetaking
public class DamageTaking : MonoBehaviour {

	public int hitPoints = 10;

	public GameObject destructionPrefab;

	// BEGIN 3d_damagetaking_gamemanager
	// Should we end the game if this object is destroyed?
	public bool gameOverOnDestroyed = false;
	// END 3d_damagetaking_gamemanager

	public void TakeDamage(int amount) {

		Debug.Log(gameObject.name + " damaged!");

		hitPoints -= amount;

		if (hitPoints <= 0) {
			Debug.Log(gameObject.name + " destroyed!");

			Destroy(gameObject);

			if (destructionPrefab != null) {
				Instantiate(destructionPrefab, transform.position, transform.rotation);
			}

			// BEGIN 3d_damagetaking_gamemanager
			// If we should end the game now, call the GameManager's GameOver method.
			if (gameOverOnDestroyed) {
				GameManager.instance.GameOver();
			}
			// END 3d_damagetaking_gamemanager
		}

	}

}
// END 3d_damagetaking