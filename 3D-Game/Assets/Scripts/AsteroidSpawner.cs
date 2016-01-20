using UnityEngine;
using System.Collections;

// BEGIN 3d_asteroidspawner
public class AsteroidSpawner : MonoBehaviour {

	// The radius of the spawn area
	public float radius = 250.0f;

	// The asteroids to spawn
	public Rigidbody asteroidPrefab;

	// Wait spawnRate ± variance seconds between each asteroid
	public float spawnRate = 5.0f;
	public float variance = 1.0f;

	// The object to aim the asteriods at
	public Transform target;

	// If false, disable spawning
	public bool spawnAsteroids = false;

	void Start () {
		// Start the coroutine that creates asteroids immediately
		StartCoroutine(CreateAsteroids());
	}

	IEnumerator CreateAsteroids() {

		// Loop forever
		while (true) {

			// Work out when the next asteroid should appear
			float nextSpawnTime = spawnRate + Random.Range(-variance, variance);

			// Wait that much time
			yield return new WaitForSeconds(nextSpawnTime);

			// Additionally, wait until physics is about to update
			yield return new WaitForFixedUpdate();

			// Create the asteroid
			CreateNewAsteroid();
		}

	}

	void CreateNewAsteroid() {

		// If we're not currently spawning asteroids, bail out
		if (spawnAsteroids == false) {
			return;
		}

		// Randomly select a point on the surface of the sphere
		var asteroidPosition = Random.onUnitSphere * radius;

		// Scale this by the object's scale
		asteroidPosition.Scale(transform.lossyScale);

		// And offset it by the asteroid spawner's location
		asteroidPosition += transform.position;

		// Create the new asteroid
		var newAsteroid = Instantiate(asteroidPrefab);

		// Place it at the spot we just calculated
		newAsteroid.transform.position = asteroidPosition;

		// Aim it at the target
		newAsteroid.transform.LookAt(target);
	}

	// Called by the editor while the spawner object is selected.
	void OnDrawGizmosSelected() {

		// We want to draw yellow stuff
		Gizmos.color = Color.yellow;

		// Tell the Gizmos drawer to use our current position and scale
		Gizmos.matrix = transform.localToWorldMatrix;

		// Draw a sphere representing the spawn area
		Gizmos.DrawWireSphere(Vector3.zero, radius);
	}
		
	public void DestroyAllAsteroids() {
		// Remove all asteroids in the game
		foreach (var asteroid in FindObjectsOfType<Asteroid>()) {
			Destroy (asteroid.gameObject);
		}
	}	
}
// END 3d_asteroidspawner