using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour {
	public TextMesh textMesh;
	void Start() {
		StartCoroutine(RotationUpdate());
	}
	IEnumerator RotationUpdate() {
		yield return new WaitForSeconds(1f);
		while(true) {
			transform.localRotation = Quaternion.Euler(0, VRWand._VRWand.transform.parent.parent.parent.parent.rotation.eulerAngles.y+90, 0);
			yield return new WaitForSeconds(0.01f);
			textMesh.text = transform.localRotation.y + " | " + VRWand._VRWand.transform.parent.parent.parent.parent.rotation.eulerAngles.y;
		}
	}
}
