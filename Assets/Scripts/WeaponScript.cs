using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {

	public Transform projectilePrefab;
	public Transform projectileOrigin;

	[SerializeField]
	private int weaponLevel = 0;

	public void Shoot (){
		switch(weaponLevel){
		case 0:
			CreateProjectile(projectileOrigin.rotation);
			break;

		case 1:
			CreateProjectile(projectileOrigin.rotation);
			CreateProjectile(Quaternion.Euler(0,0,7));
			CreateProjectile(Quaternion.Euler(0,0,-7));
			break;

		case 2:
			CreateProjectile(projectileOrigin.rotation);
			CreateProjectile(Quaternion.Euler (0, 0, 7));
			CreateProjectile(Quaternion.Euler (0, 0, -7));
			CreateProjectile(Quaternion.Euler (0, 0, 14));
			CreateProjectile(Quaternion.Euler (0, 0, -14));
			break;
		}
	}

	void CreateProjectile(Quaternion direction) {
		Transform projectile = GetProjectileInstance ();
		projectile.position = projectileOrigin.position;
		projectile.rotation = direction;
	}

	//seeks for existing inactive projectile instance or creates new one
	Transform GetProjectileInstance() {
		//projectiles located inside weapon object. That means multiple pools in the future. Change this to some global location?
		foreach(Transform child in transform) {
			if (!child.gameObject.activeSelf) {
				child.gameObject.SetActive (true);
				return child;
			}
		}

		return (Transform) Instantiate (projectilePrefab.transform, transform);
	}
}
