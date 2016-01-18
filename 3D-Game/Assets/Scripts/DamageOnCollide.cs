using UnityEngine;
using System.Collections;

// BEGIN 3d_damageoncollide
public class DamageOnCollide : MonoBehaviour {

    // The amount of damage we'll deal to anything we hit.
	public int damage = 1;
    
    // The amount of damage we'll deal to ourselves when we hit something.
	public int damageToSelf = 5;

	void HitObject(GameObject theObject) {
		// Do damage to the thing we hit, if possible
		var theirDamage = theObject.GetComponentInParent<DamageTaking>();
		if (theirDamage) {
			theirDamage.TakeDamage(damage);
		}
		
		// Do damage to ourself, if possible
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
// END 3d_damageoncollide