using UnityEngine;
using System.Collections;

// BEGIN 3d_asteroidspawner
public class AsteroidSpawner : MonoBehaviour {

	public float radius = 100.0f;

	public Rigidbody asteroidPrefab;

	public float spawnRate = 2.0f;
	public float variance = 0.5f;

	public Transform target;

	public bool spawnAsteroids = false;

	void Start () {
		StartCoroutine(CreateAsteroids());
	}

	IEnumerator CreateAsteroids() {

		while (true) {

			float nextSpawnTime = spawnRate + Random.Range(-variance, variance);

			yield return new WaitForSeconds(nextSpawnTime);

			yield return new WaitForFixedUpdate();

			CreateNewAsteroid();
		}

	}

	void CreateNewAsteroid() {

		if (spawnAsteroids == false) {
			return;
		}

		var asteroidPosition = Random.onUnitSphere * radius;
		asteroidPosition.Scale(transform.localScale);
		asteroidPosition += transform.position;

		var newAsteroid = Instantiate(asteroidPrefab);

		newAsteroid.transform.position = asteroidPosition;

		newAsteroid.transform.LookAt(target);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.matrix = transform.localToWorldMatrix;
		Gizmos.DrawWireSphere(Vector3.zero, radius);
	}

	public void DestroyAllAsteroids() {
		foreach (Asteroid a in FindObjectsOfType<Asteroid>()) {
			Destroy (a.gameObject);
		}
	}	
}
// END 3d_asteroidspawner