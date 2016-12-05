using UnityEngine;
using System.Collections;

[RequireComponent (typeof(EnemyController))]
public class WeaponSwapper : MonoBehaviour {
	public float swapInterval = 3.0f;

	private int index = 0;
	private Weapon[] weapons;
	private EnemyController enemy;

	void Awake () {
		weapons = gameObject.GetComponentsInChildren<Weapon> ();
		if (weapons.Length > 0) {
			InvokeRepeating ("Swap", swapInterval, swapInterval);
		}
		enemy = GetComponent<EnemyController> ();
	}
	
	private void Swap() {
		if (index >= weapons.Length) {
			index = 0;
		}
		enemy.weapon = weapons [index++];
	}
}
