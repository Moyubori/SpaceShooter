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
		iTween.MoveBy (gameObject, iTween.Hash("islocal",true,"x",6,"time",3f,"delay",1f));
		StartCoroutine (Animate());
	}

	IEnumerator Animate(){
		Vector3[] path = {
			new Vector3 (0, transform.localPosition.y + 6, 0),
			new Vector3 (0, transform.localPosition.y - 6, 0)
		};
		yield return new WaitForSeconds (4f);
		iTween.MoveTo (gameObject, iTween.Hash ("islocal", true, "y", -6f, "time", 2f));
		yield return new WaitForSeconds (2f);
		while (gameObject.activeSelf) {
			iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPath("EnemyPath"), "time", 5f, "easeType", "linearTween", "movetopath", false));
			yield return new WaitForSeconds (5f);
			iTween.MoveTo (gameObject, iTween.Hash ("path", iTweenPath.GetPathReversed("EnemyPath"), "time", 5f, "easeType", "linearTween", "movetopath", false));
			yield return new WaitForSeconds (5f);
		}
	}
		
}
