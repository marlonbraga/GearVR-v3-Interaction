using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRAvatar:MonoBehaviour {
	[SerializeField]
	private float minDistance = 1.5f;
	private Color overColor = new Color(0.4f, 0.4f, 0.0f, 0.5f);
	private Color pressColor = new Color(0.5f, 0.2f, 0.5f, 0.8f);
	public static VRAvatar _VRAvatar;
	public bool canMove = false;
	void Start() {
		_VRAvatar = this;
	}
	public void Movement(Vector3 newTarget) {
		if(canMove)
			StartCoroutine(Teleport(newTarget));
	}
	private IEnumerator Teleport(Vector3 newTarget) {
		transform.position = newTarget;
		yield return new WaitForSeconds(0.01f);
	}
}
