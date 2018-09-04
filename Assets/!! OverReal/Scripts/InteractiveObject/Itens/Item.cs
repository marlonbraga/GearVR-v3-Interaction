using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : InteractiveObject {
	override public void PointClick() {
		action();
		GetComponent<AudioSource>().clip = click;
		GetComponent<AudioSource>().Play();
		GetComponent<MeshRenderer>().enabled = false;
		Destroy(gameObject,3);
	}
	virtual protected void action() {}
}