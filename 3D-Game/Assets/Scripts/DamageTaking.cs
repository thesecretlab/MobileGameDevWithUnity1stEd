using UnityEngine;
using System.Collections;

public class DamageTaking : MonoBehaviour {

	public int hitPoints = 10;

	public GameObject destructionPrefab;

	public bool gameOverOnDestroyed = false;

	public void TakeDamage(int amount) {

		Debug.Log(gameObject.name + " damaged!");

		hitPoints -= amount;

		if (hitPoints <= 0) {
			Debug.Log(gameObject.name + " destroyed!");

			Destroy(gameObject);

			if (destructionPrefab != null) {
				//Create the explosion and remove it after 5 seconds
				var explosion = Instantiate(destructionPrefab, transform.position, transform.rotation) as GameObject;

				Destroy (explosion.gameObject, 5.0f);
			}

			if (gameOverOnDestroyed) {
				GameManager.instance.GameOver();
			}
		}


	}

}
