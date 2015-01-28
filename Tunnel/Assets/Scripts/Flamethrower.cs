using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Flamethrower : MonoBehaviour {

	public Sprite activeSprite;
	public Sprite inactiveSprite;

	public SpriteRenderer spriteRenderer;

	public GameObject fireballPrefab;

	public float timeBetweenShots = 4.0f;

	public float timeToCoolDown = 0.2f;

	public Transform emissionPoint;

	public float timeToStart = 1.0f;

	public AudioClip fireballSound;

	// Use this for initialization
	void Start () {
		spriteRenderer.sprite = inactiveSprite;
		StartCoroutine("ShootFireballs");
	}

	IEnumerator ShootFireballs() {

		yield return new WaitForSeconds(timeToStart);

		while (true) {

			yield return new WaitForSeconds(this.timeBetweenShots);

			StartCoroutine("Cooldown");

			if (fireballPrefab != null) {

				var audio = GetComponent<AudioSource>();
				if (audio && fireballSound) {
					audio.PlayOneShot(fireballSound);
				}

				var fireball = (GameObject)Instantiate(fireballPrefab, emissionPoint.position, Quaternion.identity);
				fireball.GetComponent<FireBall>().direction = transform.right;

				// Connect the fireball's Signal On Touch to the game manager
				fireball.GetComponent<SignalOnTouch>().onTouch.AddListener(
					delegate { 
						GameManager.instance.FireTrapTouched(); 
					}
				);
			}

		}
	}

	IEnumerator Cooldown() {
		spriteRenderer.sprite = activeSprite;
		yield return new WaitForSeconds(timeToCoolDown);
		spriteRenderer.sprite = inactiveSprite;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
