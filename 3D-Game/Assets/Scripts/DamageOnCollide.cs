using UnityEngine;
using System.Collections;

public class DamageOnCollide : MonoBehaviour {

	public int damage = 1;
	public int damageToSelf = 5;

	void HitObject(GameObject theObject) {
		// Do damage to the thing we hit, if possible
		var theirDamage = theObject.GetComponentInParent<DamageTaking>();
		if (theirDamage) {
			theirDamage.TakeDamage(damage);
		}
		
		// Do damage to ourself
		var ourDamage = this.GetComponentInParent<DamageTaking>();
		if (ourDamage) {
			ourDamage.TakeDamage(damageToSelf);
		}
		

	}
	
	void OnTriggerEnter(Collider collider) {
		HitObject(collider.gameObject);
	}
	
	void OnCollisionEnter(Collision collision) {
		
		HitObject(collision.gameObject);
	}
}
