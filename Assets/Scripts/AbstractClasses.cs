using UnityEngine;
using System.Collections;

public class AbstractClasses : MonoBehaviour {}

public abstract class Enemy : MonoBehaviour {

	public int health {
		get { return _health; }
		set { _health = value;
			defaultHealth = value; }
	}

	[SerializeField]
	protected int _health = 100;
	protected int defaultHealth;

	public abstract void TakeDamage (int damage);
}



public abstract class Weapon : MonoBehaviour {

	public ObjectPool projectilePool;
	public Transform projectileOrigin;

	[SerializeField]
	protected int weaponLevel = 0;

	public abstract void Shoot ();
}



public abstract class BackgroundController : MonoBehaviour {
	new public Camera camera;

	private Vector3 previousPosition;
	void Start () {
		previousPosition = camera.transform.position;
	}

	void Update () {
		Vector3 cameraPosition = camera.transform.position;
		float cameraOffset = cameraPosition.x - previousPosition.x;

		if (cameraOffset != 0) {
			//Whole background moves with camera
			Vector3 newPos = camera.transform.position;
			newPos.x = camera.transform.position.x + cameraOffset;
			transform.position = newPos;
		
			UpdateChildren (cameraOffset);
			previousPosition = cameraPosition;
		}
	}

	protected abstract void UpdateChildren(float cameraOffset);

	protected void UpdateParallax(Transform child, float cameraOffset) {
		//parallax is inversely proportional to zDistance. May be not strictly correct
		float zDistance = Mathf.Abs (child.position.z - camera.transform.position.z);
		float parallax = -cameraOffset / zDistance;
		child.Translate(new Vector3(parallax, 0, 0), Space.World);
	}
}