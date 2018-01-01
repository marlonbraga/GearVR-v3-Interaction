using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour {
	private Material material;
	void Start() {
		material = GetComponent<MeshRenderer>().material;
	}
	void Update() {
		transform.rotation = Quaternion.Euler(0, VRWand._VRWand.transform.rotation.eulerAngles.y + 180, 0);
	}
	private void OnTriggerStay(Collider collider) {
		DisableMovement(collider);
	}
	void OnTriggerExit(Collider collider) {
		AbleMovement();
	}
	private void AbleMovement() {
		material.SetColor("_EmissionColor", Color.green);
		VRAvatar._VRAvatar.canMove = true;
	}
	private void DisableMovement(Collider collider) {
		if(!collider.gameObject.GetComponent<Floor>()) {
			material.SetColor("_EmissionColor", Color.red);
			VRAvatar._VRAvatar.canMove = false;
		}
	}
}