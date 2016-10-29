using UnityEngine;
using System.Collections;

public class AbstractClasses : MonoBehaviour {

}

public abstract class EnemyClass : MonoBehaviour {

	public float health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	private float _health = 1;
	private float defaultHealth;

	//methods

	public void TakeDamage(float damage){
		if (damage > health) {
			health = 0;
			gameObject.SetActive (false);
		} else {
			health -= damage;
		}
	}
}

public abstract class WeaponClass : MonoBehaviour {

	public Transform projectilePrefab;
	public Transform projectileOrigin;

	[SerializeField]
	protected int weaponLevel = 0;

	public abstract void Shoot ();
}