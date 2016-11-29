using UnityEngine;
using System.Collections;

public class FanWeapon : Weapon {

	override public void Shoot (Bounds bounds){
		switch(weaponLevel){
		case 0:
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation);
			break;

		case 1:
			projectilePool.GetInstance(projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0, 0, 5));
			projectilePool.GetInstance(projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0, 0, -5));
            break;

       case 2:
            projectilePool.GetInstance(projectileOrigin.position, projectileOrigin.rotation);
            projectilePool.GetInstance(projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0, 0, 7));
            projectilePool.GetInstance(projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0, 0, -7));
            break;

		case 3:
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,-4));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,4));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,-6));
			projectilePool.GetInstance (projectileOrigin.position, projectileOrigin.rotation * Quaternion.Euler(0,0,6));
			break;
		}
	}

}
