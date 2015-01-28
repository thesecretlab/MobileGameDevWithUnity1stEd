using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Flamethrower : MonoBehaviour {

	public Sprite activeSprite;
	public Sprite inactiveSprite;

	public SpriteRenderer spriteRenderer;

	public GameObject fireballPrefab;

	public float timeBetweenShots;

	public float timeToCoolDown;

	public Transform emissionPoint;

	// Use this for initialization
	void Start () {
		spriteRenderer.sprite = inactiveSprite;
		StartCoroutine("ShootFireballs");
	}

	IEnumerator ShootFireballs() {
		while (true) {

			yield return new WaitForSeconds(this.timeBetweenShots);

			StartCoroutine("Cooldown");

			if (fireballPrefab != null) {
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
