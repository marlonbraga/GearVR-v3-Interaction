using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSphere:ScenaryObject {
	public override void SetStatusSwitch(bool newStatus) {
		statusSwitch = newStatus;
		if(statusSwitch) {
			GetComponent<MeshRenderer>().material.color = Color.green;
		} else {
			GetComponent<MeshRenderer>().material.color = Color.grey;
		}
	}
}
