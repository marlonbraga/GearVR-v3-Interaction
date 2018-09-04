using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {
	public float force = 1f;
	void Start () {
		StartCoroutine(Trajectory());
	}
	IEnumerator Trajectory() {
		while(true){
			GetComponent<Rigidbody>().velocity = transform.right * force * -Time.deltaTime;
			yield return null;
		}
	}
	private void OnCollisionEnter(Collision collision) {
		if(!collision.gameObject.GetComponent<VRAvatar>()) {
			Destroy(gameObject);
		}
	}
}