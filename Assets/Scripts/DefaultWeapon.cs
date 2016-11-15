using UnityEngine;
using System.Collections;

public class DefaultWeapon : Weapon {

	override public void Shoot (){
		switch(weaponLevel){
		case 0:
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation);
			break;

		case 1:
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation);
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,7));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,-7));
			break;

		case 2:
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation);
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,7));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,-7));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,14));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,-14));
			break;
		}
	}

}
