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

	public abstract void TakeDamage (float damage);
}

public abstract class WeaponClass : MonoBehaviour {

	public ObjectPool projectilePool;
	public Transform projectileOrigin;

	[SerializeField]
	protected int weaponLevel = 0;

	public abstract void Shoot ();
}

public abstract class BackgroundController : MonoBehaviour {
	public Camera camera;

	private Vector3 previousPosition;
	void Start () {
		previousPosition = camera.transform.position;
	}

	void Update () {
		Vector3 cameraPosition = camera.transform.position;
		float cameraOffset = cameraPosition.x - previousPosition.x;

		if (cameraOffset != 0) {
			//Whole background moves with camera
			transform.Translate (new Vector2 (cameraOffset, 0));

			UpdateChildren (cameraOffset);
			previousPosition = cameraPosition;
		}
	}

	protected void UpdateParallax(Transform child, float cameraOffset) {
		//parallax is inversely proportional to zDistance. May be not strictly corret
		float zDistance = Mathf.Abs (child.position.z - camera.transform.position.z);
		float parallax = -cameraOffset / zDistance;
		child.Translate(new Vector3(parallax, 0, 0));
	}
		
	protected abstract void UpdateChildren(float cameraOffset);
}






