using UnityEngine;
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

	//projectiles fired per second
	public float firerate {
		get { return _firerate; }
		set { _firerate = value;
			defaultFirerate = value; }
	}

	[SerializeField]
	private float _firerate = 5f;
	private float defaultFirerate;
	[SerializeField]
	private float manualMaxFirerate = 6f;
	private float fireTimer = 0;


	public int health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	private int _health = 5;
	private int defaultHealth;

	//other

	[Header("Other:")]
	public Camera cameraReference;
	public Transform healthBar;
	public Transform weaponSlot;

	//methods

	//set default values here
	public void Init(){
		defaultSpeed = _speed;
		defaultFirerate = _firerate;
		defaultHealth = _health;
	}

	// changes speed of the player for a given amount of time(given in seconds)
	public void ApplySpeedModifier (float modifier, float duration){
		_speed = _speed * modifier;
		StartCoroutine (RevertSpeedModifier (modifier, duration));
	}

	IEnumerator RevertSpeedModifier (float modifier, float duration){
		yield return new WaitForSeconds (duration);
		_speed = _speed / modifier;
	}

	public void ApplyFirerateModifier (float modifier, float duration){
		_firerate = _firerate * modifier;
		StartCoroutine (RevertFirerateModifier (modifier, duration));
	}

	IEnumerator RevertFirerateModifier (float modifier, float duration){
		yield return new WaitForSeconds (duration);
		_firerate = _firerate / modifier;
	}

	private void Shoot(){
		weaponSlot.GetChild (0).GetComponent<WeaponScript> ().Shoot ();
	}

	private void SetMovementVariables(ref float translationX, ref float translationY){
		translationY = Input.GetAxis ("Vertical") * speed;
		translationX = Input.GetAxis ("Horizontal") * speed;

		//optimise this shit
		float upBoundary = cameraReference.orthographicSize;
		float downBoundary = -upBoundary;
		float rightBoundary = upBoundary * cameraReference.aspect;
		float leftBoundary = -rightBoundary;

		if ((transform.localPosition.x > rightBoundary && translationX > 0) || (transform.localPosition.x < leftBoundary && translationX < 0)) {
			translationX = 0;
		}
		if ((transform.localPosition.y > upBoundary && translationY > 0) || (transform.localPosition.y < downBoundary && translationY < 0)) {
			translationY = 0;
		}

	}

	void OnCollisionEnter2D(Collision2D collisionInfo){
		string collider = collisionInfo.collider.gameObject.name;
		Debug.Log ("in" + collider);
	}

	void OnCollisionExit2D(Collision2D collisionInfo){
		string collider = collisionInfo.collider.gameObject.name;
		Debug.Log ("out" + collider);
	}

	//use this for initialization
	void Start () {

	}
	
	//update is called once per frame
	void Update () {
		
		//movement
		float translationX = 0;
		float translationY = 0;
		SetMovementVariables (ref translationX, ref translationY);
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (translationX * 100, translationY * 100);

		//shooting


		if (Input.GetKey ("space") && Time.time > fireTimer) {
			fireTimer = Time.time + 1 / firerate;
			Shoot ();
		}
		if (Input.GetKeyUp ("space")) {
			fireTimer += (1 / manualMaxFirerate - 1 / firerate);
		}




		//if(Input.GetKeyDown("space")){
		//	InvokeRepeating ("Shoot", 0.001f, 1 / firerate);
		//}
		//if (Input.GetKeyUp("space")) {
		//	CancelInvoke ();
		//}


	}
}
