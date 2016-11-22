using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public float speed = 0.7f;

	private Vector3 spawnPoint = new Vector3(20,0,0);
	private Vector3 fPoint;
	private float sTime;


	void OnEnable(){
		Start ();
	}

	void OnDisable(){
		iTween.Stop (gameObject);
		StopCoroutine("Animate");
	}

	void Start(){
		StartCoroutine (Animate());
	}

	IEnumerator Animate(){
		iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath("SpawnToStdPatrolPath"), "time", 4f, "easeType", "linearTween", "movetopath", false));
		yield return new WaitForSeconds (4f);
		while (gameObject.activeSelf) {
			iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath("StdPatrolPath"), "time", 5f, "easeType", "linearTween", "movetopath", false));
			yield return new WaitForSeconds (5f);
			iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPathReversed("StdPatrolPath"), "time", 5f, "easeType", "linearTween", "movetopath", false));
			yield return new WaitForSeconds (5f);
		}
	}
		
}
