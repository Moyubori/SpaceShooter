using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
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


	public int health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}
	public int maxHealth = 100;

	[SerializeField]
	private int _health = 100;
	private int defaultHealth;


	public int lives {
		get { return _lives; }
		set { _lives = value;
			defaultLives = value; }
	}

	[SerializeField]
	private int _lives = 5;
	private int defaultLives;


	//respawning fields
	[Header("Respawning")]
	public float respawnDelay = 0.5f;
	public float respawnProtectionDuration = 1.6f;
	public float respawnFlickerFrequency = 8.0f;
	private Vector3 defaultPosition;
	private bool respawnProtection = false; //is player under respawn protection
	private ScoreClass scoreObject;



	//other stuff

	[Header("Other:")]
	public Camera cameraReference;
	public HealthbarManager healthBar;
	public HealthIconsManager livesIcons;
	public Weapon weapon;
	public ObjectBoundaries boundaries;

	//methods

	void Awake(){
		// setting the default variables
		defaultSpeed = _speed;
		defaultFirerate = _firerate;
		defaultHealth = _health;
		defaultLives = _lives;
	}

	void Start(){
		// sets healthbar to the initial value
		healthBar.SetHealth (health);

		spriteRenderer = transform.Find ("Sprite").GetComponent<SpriteRenderer> ();
		weapon = transform.Find ("Weapon").GetComponent<Weapon>();
		if (weapon == null) {
			Debug.Log ("No player weapon.");
		}
		try{
			scoreObject = Resources.FindObjectsOfTypeAll<ScoreClass> () [0];
		} catch(IndexOutOfRangeException e){
			scoreObject = Resources.Load ("score") as ScoreClass;
		}

		defaultPosition = transform.position;
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

	private void Shoot(){
		weapon.Shoot (spriteRenderer.bounds);
	}


	// makes sure the player won't fly out of the screen
	private void SetMovementVariables(ref float translationX, ref float translationY){
		translationY = Input.GetAxis ("Vertical") * speed;
		translationX = Input.GetAxis ("Horizontal") * speed;

		// subtract width and height of ship's sprite
		float yBoundary = cameraReference.orthographicSize - boundaries.right;
		float leftBoundary = -cameraReference.orthographicSize * cameraReference.aspect - boundaries.left;
		float rightBoundary = cameraReference.orthographicSize * cameraReference.aspect - boundaries.right;

		if ((transform.localPosition.x > rightBoundary && translationX > 0) || (transform.localPosition.x < leftBoundary && translationX < 0)) {
			translationX = 0;
		}
		if ((transform.localPosition.y > yBoundary && translationY > 0) || (transform.localPosition.y < -yBoundary && translationY < 0)) {
			translationY = 0;
		}

	}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "ProjectilesEnemy" || collider.tag == "Obstacle") {
			//Debug.Log (collider.tag);
			TakeDamage (collider.GetComponent<InflictingDamage> ().damageOnContact);
			collider.gameObject.SetActive (false);
		} else if (collider.tag == "Enemy") {
			TakeDamage (collider.GetComponent<InflictingDamage> ().damageOnContact);
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


	// Taking damage

	public void TakeDamage(int damage){
		if (!respawnProtection) {
			_health -= damage;
			healthBar.SetHealth (health);
			if (health <= 0) {
				Die ();
			}
		}
	}

	private void Die() {
		lives -= 1;
		if (lives > 0) {
			gameObject.SetActive (false);
			livesIcons.SetLives (lives);

			Invoke ("Respawn", respawnDelay);
		} else {
			scoreObject.win = false;
			gameObject.SetActive (false);
			Application.LoadLevel ("level_complete");
		}
	}

	private void Respawn() {
		gameObject.SetActive (true);
		_health = maxHealth;
		transform.position = defaultPosition;
		healthBar.SetHealth (health);

		respawnProtection = true;
		InvokeRepeating ("Flicker", 0, 1/respawnFlickerFrequency);
		Invoke ("EndRespawnProtection", respawnProtectionDuration);
	}

	private void Flicker() {
		Color color = spriteRenderer.color;
		color.a = (color.a == 0) ? 1 : 0;
		spriteRenderer.color = color;
	}

	private void EndRespawnProtection() {
		Color color = spriteRenderer.color;
		color.a = 1;
		spriteRenderer.color = color;

		respawnProtection = false;
		CancelInvoke ("Flicker");
	}
}
