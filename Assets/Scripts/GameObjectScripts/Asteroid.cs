using UnityEngine;
using System.Collections;

public class Asteroid : InflictingDamage {

	public float movementSpeed = 10;
	public float rotationSpeed = 10;
	public float randFactor = 0.3f;

	private float calculatedTranslation;
	private float calculatedRotation;
	private Transform sprite;


	private bool enteredScreen = false;

	void CheckIfOutOfScreen(){
		if (enteredScreen && !sprite.GetComponent<SpriteRenderer> ().isVisible) {
			enteredScreen = false;
			gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		// check if collision should deal damage
		if (collider.tag == "ProjectilesPlayer") {
			transform.rotation = Quaternion.Euler (0, 0, 0);
			collider.gameObject.SetActive (false);
			gameObject.SetActive (false);
		}
	}

	void OnEnable(){
		calculatedTranslation = ((int)Random.Range (-1, 1) * Random.value * randFactor * movementSpeed) + movementSpeed;
		calculatedRotation = Random.Range (-1f, 1f) * randFactor * rotationSpeed;
		GetComponent<Rigidbody2D> ().velocity = new Vector2(-calculatedTranslation,0);
	}

	void Awake(){
		damage = 15;
		calculatedTranslation = ((int)Random.Range (-1, 1) * Random.value * randFactor * movementSpeed) + movementSpeed;
		calculatedRotation = Random.Range (-1f, 1f) * randFactor * rotationSpeed;
	}

	void Start(){
		sprite = transform.GetChild (0);
		GetComponent<Rigidbody2D> ().velocity = new Vector2(-calculatedTranslation,0);
		//Debug.Log (GetComponent<Rigidbody2D> ().velocity);
		InvokeRepeating ("CheckIfOutOfScreen", 1, 0.5f);
	}

	void Update(){
		sprite.Rotate (0, 0, calculatedRotation * Time.timeScale);

		if (!enteredScreen && sprite.GetComponent<SpriteRenderer> ().isVisible) {
			enteredScreen = true;
		}
	}
}