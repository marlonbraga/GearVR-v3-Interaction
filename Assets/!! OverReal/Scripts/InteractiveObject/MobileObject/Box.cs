using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box:MobileObject {
	enum Status { none, selected }
	private Status status;
	private AudioSource audiosource;
	void Start() {
		audiosource = GetComponent<AudioSource>();
		status = Status.none;
	}
	override public void PointClick(Vector3 vector3) {
		audiosource.clip = click;
		audiosource.Play();
	}
	override public void PointPress() {
		if(status != Status.selected) {
			audiosource.clip = select;
			audiosource.Play();
			Debug.Log("Select()");
		}
		status = Status.selected;
	}
	override public void PointDepress() {
		status = Status.none;
	}
}
