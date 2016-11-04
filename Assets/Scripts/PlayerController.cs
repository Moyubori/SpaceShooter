﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	//stats

	// setter sets the default value
	// if only need to temporarily change the speed, use the ApplySpeedModifier() method
	public float speed {
		get { return _speed; }
		set { _speed = value;
			defaultSpeed = value; }
	}

	[Header("Stats:")]
	[SerializeField]
	private float _speed = 0.07f;
	private float defaultSpeed;

	//shots fired per second
	public float firerate {
		get { return _firerate; }
		set { _firerate = value;
			defaultFirerate = value; }
	}

	[SerializeField]
	private float _firerate = 5f;
	private float defaultFirerate;
	[SerializeField]
	private float manualMaxFirerate = 6f; //firerate when shooting manually(pressing and releasing button repeatedly)
	private float fireTimer = 0; //shooting cooldown timer


	public float health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	private float _health = 1;
	private float defaultHealth;


	public int lives {
		get { return _lives; }
		set { _lives = value;
			defaultLives = value; }
	}

	[SerializeField]
	private int _lives = 5;
	private int defaultLives;


	//other stuff

	[Header("Other:")]
	public Camera cameraReference;
	public Transform healthBar;
	public Transform livesIcons;
	public Transform weaponSlot;
	public ObjectBoundaries boundaries;


	//methods

	void Awake(){
		defaultSpeed = _speed;
		defaultFirerate = _firerate;
		defaultHealth = _health;
		defaultLives = _lives;
	}

	// changes the speed of the player for a given amount of time(given in seconds)
	public void ApplySpeedModifier (float modifier, float duration){
		_speed = _speed * modifier;
		StartCoroutine (RevertSpeedModifier (modifier, duration));
	}

	IEnumerator RevertSpeedModifier (float modifier, float duration){
		yield return new WaitForSeconds (duration);
		_speed = _speed / modifier;
	}

	// changes the firerate at which the weapon fires for a given amount of time(given in seconds)
	public void ApplyFirerateModifier (float modifier, float duration){
		_firerate = _firerate * modifier;
		StartCoroutine (RevertFirerateModifier (modifier, duration));
	}

	IEnumerator RevertFirerateModifier (float modifier, float duration){
		yield return new WaitForSeconds (duration);
		_firerate = _firerate / modifier;
	}

	// damage should be given as a float between 0 and 1
	public void TakeDamage(float damage){
		if (damage > health) {
			_health = 0;
		} else {
			_health -= damage;
		}
		healthBar.GetComponent<ManageHealthbar> ().SetHealth(health);
	}


	private void Shoot(){
		weaponSlot.GetChild (0).GetComponent<WeaponScript> ().Shoot ();
	}


	// makes sure the player won't fly out of the screen
	private void SetMovementVariables(ref float translationX, ref float translationY){
		translationY = Input.GetAxis ("Vertical") * speed;
		translationX = Input.GetAxis ("Horizontal") * speed;

		// subtract width and height of ship's sprite
		float yBoundary = cameraReference.orthographicSize;
		float xBoundary = yBoundary * cameraReference.aspect - boundaries.top;
		yBoundary -= boundaries.right;

		if ((transform.localPosition.x > xBoundary && translationX > 0) || (transform.localPosition.x < -xBoundary && translationX < 0)) {
			translationX = 0;
		}
		if ((transform.localPosition.y > yBoundary && translationY > 0) || (transform.localPosition.y < -yBoundary && translationY < 0)) {
			translationY = 0;
		}

	}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "ProjectilesEnemy") {
			TakeDamage (collider.GetComponent<Projectile> ().damage);
			collider.gameObject.SetActive (false);
		}
	}

	void Update () {
		
		//movement
		float translationX = 0;
		float translationY = 0;
		SetMovementVariables (ref translationX, ref translationY);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (translationX * 100, translationY * 100);

		//shooting
		//fires automatically if spacebar is held
		if (Input.GetKey ("space") && Time.time > fireTimer) {
			fireTimer = Time.time + 1 / firerate;
			Shoot ();
		}
		//when the spacebar is released, sets a small cooldown to avoid spamming the projectiles
		if (Input.GetKeyUp ("space")) {
			fireTimer += (1 / manualMaxFirerate - 1 / firerate);
		}
	}
}
