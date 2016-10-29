using UnityEngine;
using System.Collections;

public class EnemyController : EnemyClass {

	public void TakeDamage (float damage){
		if (damage > health) {
			health = 0;
			gameObject.SetActive (false);
		} else {
			health -= damage;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "Projectiles") {
			TakeDamage (collider.GetComponent<Projectile> ().damage);
			collider.gameObject.SetActive (false);
		}
	}

}
