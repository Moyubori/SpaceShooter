using UnityEngine;
using System.Collections;

public class AbstractClasses : MonoBehaviour {

}

public abstract class EnemyClass : MonoBehaviour {

	public int health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	protected int _health = 100;
	protected int defaultHealth;

	//methods

	public abstract void TakeDamage (int damage);
}

public abstract class WeaponClass : MonoBehaviour {

	public ObjectPool projectilePool;
	public Transform projectileOrigin;

	[SerializeField]
	protected int weaponLevel = 0;

	public abstract void Shoot ();
}