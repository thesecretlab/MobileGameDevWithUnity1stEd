using UnityEngine;
using System.Collections;
using UnityEngine.Events;

// Periodically emits fireballs at a given rate.
public class Flamethrower : MonoBehaviour {

	// The sprite to use when we're shooting a fireball
	public Sprite activeSprite;

	// The sprite to use when we're not shooting
	public Sprite inactiveSprite;

	// The sprite renderer that should change sprites
	public SpriteRenderer spriteRenderer;

	// The object to create when shooting a fireball
	public GameObject fireballPrefab;

	// How long to wait before shooting?
	public float timeBetweenShots = 4.0f;

	// How long the activeSprite will be visible
	public float timeToCoolDown = 0.2f;

	// Where the fireball should be created from
	public Transform emissionPoint;

	// How long to wait before we start shooting sprites
	public float timeToStart = 1.0f;

	// The sound to play when shooting a fireball
	public AudioClip fireballSound;

	void Start () {
		// Ensure that the inactive sprite is being used
		spriteRenderer.sprite = inactiveSprite;

		// Start the coroutine that shoots fireballs
		StartCoroutine("ShootFireballs");
	}

	IEnumerator ShootFireballs() {

		// Wait for timeToStart seconds before starting to shoot
		yield return new WaitForSeconds(timeToStart);

		// Loop forever (it won't hang, due to the use of 
        // WaitForSeconds)
		while (true) {

			// Swap sprites and then reset after timeToCoolDown 
            // seconds
			StartCoroutine("Cooldown");

			// If we have a fireball, shoot one
			if (fireballPrefab != null) {

				// If we have an audio component, play the sound
				var audio = GetComponent<AudioSource>();
				if (audio && fireballSound) {
					audio.PlayOneShot(fireballSound);
				}

				// Create the fireball at the emissionPoint's 
                // location, but with no rotation
				var fireball = 
                    (GameObject)Instantiate(
                        fireballPrefab, 
                        emissionPoint.position, 
                        Quaternion.identity
                    );

				// Make the fireball's Mover component start 
                // moving in this object's rightward-pointing 
                // direction (the red arrow in Unity)
				fireball.GetComponent<Mover>().direction =
                    transform.right;

				// Connect the fireball's Signal On Touch to 
                // the game manager
				
                fireball.GetComponent<SignalOnTouch>().
                    onTouch.AddListener(
    					delegate { 
    						// When the fireball touches 
                            // the player, call FireTrapTouched
                            // to kill the gnome
    						
                            GameManager.instance.
                                FireTrapTouched(); 
    					}
    				);
			}

			// Wait timeBetweenShots seconds to shoot again
			yield return new WaitForSeconds(this.timeBetweenShots);


		}
	}

	IEnumerator Cooldown() {
		// Enable the activeSprite
		spriteRenderer.sprite = activeSprite;

		// Wait for timeToCoolDownSeconds
		yield return new WaitForSeconds(timeToCoolDown);

		// And swap back to the inactiveSprite
		spriteRenderer.sprite = inactiveSprite;
	}

}
