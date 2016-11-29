using UnityEngine;
using System.Collections;

public class EnemyController : Enemy {

	//shots fired per second
	public float firerate {
		get { return _firerate; }
		set { _firerate = value;
			defaultFirerate = value; }
	}

	[SerializeField]
	private float _firerate = 0.5f;
	private float defaultFirerate;
	[SerializeField]
	private float fireTimer = 0; //shooting cooldown timer

	public Weapon weapon;

	// use this to deal damage to this enemy
	override public void TakeDamage (int damage){
		_health = Mathf.Clamp (health - damage, 0, 100);
		if (health == 0) {
			gameObject.SetActive(false);
		}
	}

	void OnDisable(){
		_health = defaultHealth;
		fireTimer = 0;
		}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "ProjectilesPlayer") {
			TakeDamage (collider.GetComponent<Projectile> ().damage);
			collider.gameObject.SetActive (false);
		}
	}

	void Awake(){
		defaultHealth = health;
		defaultFirerate = firerate;
		weapon = transform.Find ("Weapon").GetComponent<Weapon> ();
	}

	void Start(){
		weapon.projectilePool = GameObject.FindWithTag ("ObjectPools").transform.FindChild ("EnemyProjectiles").GetComponent<ObjectPool> ();
		weapon.projectileOrigin = transform.FindChild ("Weapon");
	}
	
	void Update (){
		fireTimer += Time.deltaTime;
		if (fireTimer >= 1 / firerate) {
			SpriteRenderer spriteRenderer = transform.Find ("Sprite").GetComponent<SpriteRenderer> ();
			weapon.Shoot (spriteRenderer.bounds);
			fireTimer = 0;
		}
	}
}
