using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public ObjectPool pool;
	public Sprite[] sprites;
	public Camera camera;
	public float scrollRate;
	public float spawnRate;

	void Start() {
		InvokeRepeating ("CreateObject", 0, spawnRate);
	}

	private void CreateObject() {
		Transform obj = pool.GetInstance ();

		SpriteRenderer spriteRenderer = obj.gameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = sprites [Random.Range (0, sprites.Length)];

		BackgroundScroller scroller = obj.gameObject.GetComponent<BackgroundScroller> ();
		scroller.scrollRate = scrollRate;
		scroller.camera = camera;

		Vector3 position = new Vector3 ();
		position.x = camera.orthographicSize * camera.aspect;

		float random = (Random.value - 0.5f);
		position.y = random * camera.orthographicSize;

		Debug.Log (random+" * " + camera.orthographicSize + " = " + position.y);

		obj.position = position;	
	}
}