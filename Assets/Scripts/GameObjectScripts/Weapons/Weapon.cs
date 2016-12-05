using UnityEngine;
using System.Collections;

public abstract class Weapon : MonoBehaviour {

	public ObjectPool projectilePool;
	public Transform projectileOrigin;

	[SerializeField]
	public int weaponLevel = 0;

	public abstract void Shoot (Bounds bounds);
}