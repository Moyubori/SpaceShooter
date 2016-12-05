using UnityEngine;
using System.Collections;

public class StraightWeapon : Weapon {

	override public void Shoot (Bounds bounds){
		float offset = bounds.extents.y * 0.3f;
		Vector3 position = projectileOrigin.position;

		switch (weaponLevel) {
		case 0:
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			break;

		case 1:
			position.y += offset;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			position.y -= 2 * offset;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			break;

		case 2:
			position.y += 2 * offset;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			position.y -= 4 * offset;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
		
			position.y += offset;
			Vector3 horizontalOffset = projectileOrigin.rotation * Vector3.right * (5 * offset);
			position += horizontalOffset;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			break;

		case 3:
			float offsetY = bounds.size.y * 0.07f;
			float offsetX = bounds.extents.x * 0.2f;

			projectilePool.GetInstance (position, projectileOrigin.rotation);
			position.x -= offsetX;
			position.y += offsetY;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			position.y -= 2 * offsetY;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			position.y += offsetY;
			position.x -= offsetX;
			projectilePool.GetInstance (position, projectileOrigin.rotation);
			break;
		}
	}
}
