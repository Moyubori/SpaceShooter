using UnityEngine;
using System.Collections;

public class EnemyController : Enemy {

	//shots fired per second
	public float firerate {
		get { return _firerate; }
		set { _firerate = value;
			defaultFirerate = value; }
	}

	public const int points = 100;

	[SerializeField]
	private float _firerate = 0.5f;
	private float defaultFirerate;
	private float fireTimer = 0; //shooting cooldown timer
	private ScoreClass scoreObject;

	public Weapon weapon;
	public SpriteRenderer spriteRenderer;

	// use this to deal damage to this enemy
	override public void TakeDamage (int damage){
		_health = Mathf.Clamp (health - damage, 0, defaultHealth);
		if (health == 0) {
			scoreObject.AddPoints (points);
			gameObject.SetActive(false);
		}
	}

	void OnEnable() {
		fireTimer = Random.Range (0, 1 / firerate);
	}

	void OnDisable(){
		_health = defaultHealth;
		}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "ProjectilesPlayer") {
			TakeDamage (collider.GetComponent<Projectile> ().damageOnContact);
			collider.gameObject.SetActive (false);
		}
	}

	void Awake(){
		defaultHealth = health;
		defaultFirerate = firerate;
	}

	void Start(){
		scoreObject = Resources.FindObjectsOfTypeAll<ScoreClass> ()[0];
		foreach (Weapon weapon in GetComponentsInChildren<Weapon>()) {
			weapon.projectilePool = GameObject.FindWithTag ("ObjectPools").transform.FindChild ("EnemyProjectiles").GetComponent<ObjectPool> ();
			weapon.projectileOrigin = transform;
		}
	}
	
	void Update (){
		fireTimer += Time.deltaTime;
		if (fireTimer >= 1 / firerate) {
			weapon.Shoot (spriteRenderer.bounds);
			fireTimer = 0;
		}
	}
}
