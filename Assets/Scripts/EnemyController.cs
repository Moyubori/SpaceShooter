using UnityEngine;
using System.Collections;

public class EnemyController : EnemyClass {

	public float firerate = 0.5f;
	private float weaponCooldown = 0;

	public WeaponClass weapon;

	override public void TakeDamage (float damage){
		if (damage > health) {
			health = 0;
			gameObject.SetActive (false);
		} else {
			health -= damage;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "ProjectilesPlayer") {
			TakeDamage (collider.GetComponent<Projectile> ().damage);
			collider.gameObject.SetActive (false);
		}
	}

	
	void Update (){
		weaponCooldown += Time.deltaTime;
		if (weaponCooldown >= 1/firerate) {
			weapon.Shoot ();
			weaponCooldown = 0;
		}
	}
}
