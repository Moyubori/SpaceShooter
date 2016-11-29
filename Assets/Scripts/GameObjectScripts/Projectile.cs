using UnityEngine;
using System.Collections;

public class Projectile : InflictingDamage {

	public float speed = 10f;
	//public int damage = 10;

	[SerializeField]
	new private Renderer renderer;

	void CheckIfOutOfCamera(){
		// that is good enough, right?
		if (!renderer.isVisible) {
			gameObject.SetActive (false);
		}

	}

	void OnEnable() {
		//every half a second checks if the projectile should be deleted
		InvokeRepeating ("CheckIfOutOfCamera", 1, 0.5f);
	}

	void OnDisable() {
		CancelInvoke ();
	}

	void Update () {
		// movement of the projectile
		transform.localPosition += transform.right * speed * Time.deltaTime;
	}
}
