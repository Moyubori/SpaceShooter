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
			createProjectile(projectileOrigin.rotation);
			break;

		case 1:
			createProjectile(projectileOrigin.rotation);
			createProjectile(Quaternion.Euler(0,0,7));
			createProjectile(Quaternion.Euler(0,0,-7));
			break;

		case 2:
			createProjectile(projectileOrigin.rotation);
			createProjectile(Quaternion.Euler (0, 0, 7));
			createProjectile(Quaternion.Euler (0, 0, -7));
			createProjectile(Quaternion.Euler (0, 0, 14));
			createProjectile(Quaternion.Euler (0, 0, -14));
			break;
		}
	}

	void createProjectile(Quaternion quaternion) {
		Transform projectile = getProjectileInstance ();
		projectile.position = projectileOrigin.position;
		projectile.rotation = quaternion;
	}

	//seeks for existing inactive projectile instance or creates new one
	Transform getProjectileInstance() {
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
