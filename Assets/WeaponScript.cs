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
			Instantiate (projectilePrefab, projectileOrigin.position, projectileOrigin.rotation);
			break;

		case 1:
			Instantiate (projectilePrefab, projectileOrigin.position, projectileOrigin.rotation);
			Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.Euler(0,0,7));
			Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.Euler(0,0,-7));
			break;

		case 2:
			Instantiate (projectilePrefab, projectileOrigin.position, projectileOrigin.rotation);
			Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.Euler (0, 0, 7));
			Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.Euler (0, 0, -7));
			Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.Euler (0, 0, 14));
			Instantiate (projectilePrefab, projectileOrigin.position, Quaternion.Euler (0, 0, -14));
			break;
		}
	}

	void Awake(){

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
