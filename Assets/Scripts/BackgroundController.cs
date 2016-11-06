using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {
	public Camera camera;

	Vector3 previousPosition;

	void Start () {
		previousPosition = camera.transform.position;
	}

	void Update () {
		Vector3 cameraPosition = camera.transform.position;
		float cameraOffset = cameraPosition.x - previousPosition.x;

		if (cameraOffset != 0) {
			foreach (Transform child in transform) {
				if (child.gameObject.activeSelf) {
					//objects move with camera
					Vector3 pos = child.position;
					pos.x += cameraOffset;

					//parallax is inversely proportional to zDistance. It's not mathematically accurate,
					//but monotonicity is correct
					float zDistance = Mathf.Abs (child.position.z - camera.transform.position.z);
					float parallax = cameraOffset / zDistance;
					pos.x -= parallax;

					child.position = pos;
				}
			}
			previousPosition = cameraPosition;
		}
	}
}
