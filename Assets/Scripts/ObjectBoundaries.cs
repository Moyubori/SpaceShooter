using UnityEngine;
using System.Collections;

public class ObjectBoundaries : MonoBehaviour {

	[SerializeField]
	private Transform _left;
	[SerializeField]
	private Transform _right;
	[SerializeField]
	private Transform _top;
	[SerializeField]
	private Transform _bottom;

	public float left {
		get { return _left.localPosition.x; }
	}
	public float right {
		get { return _right.localPosition.x; }
	}
	public float top {
		get { return _top.localPosition.y; }
	}
	public float bottom {
		get { return _bottom.localPosition.y; }
	}

}
