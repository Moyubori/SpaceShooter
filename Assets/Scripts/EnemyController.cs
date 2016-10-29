using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	private float _health = 1;
	private float defaultHealth;

	public Transform weaponSlot;


	//methods

	public void TakeDamage(float damage){
		if (damage > health) {
			health = 0;
			gameObject.SetActive (false);
		} else {
			health -= damage;
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		TakeDamage (collider.GetComponent<Projectile>().damage);
		collider.gameObject.SetActive (false);
	}
}
