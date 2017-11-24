using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box:InteractiveObject {
	enum Status { none, selected }
	private Status status;
	private AudioSource audiosource;
	void Start() {
		audiosource = GetComponent<AudioSource>();
		status = Status.none;
	}
	override public void Click() {
		audiosource.clip = click;
		audiosource.Play();
	}
	override public void Select() {
		if(status != Status.selected) {
			audiosource.clip = select;
			audiosource.Play();
			Debug.Log("Select()");
		}
		status = Status.selected;
	}
	override public void Deselect() {
		status = Status.none;
	}
}
