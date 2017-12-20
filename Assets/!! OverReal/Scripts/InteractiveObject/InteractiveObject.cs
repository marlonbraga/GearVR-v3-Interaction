using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
abstract public class InteractiveObject:MonoBehaviour {

	public AudioClip select;
	public AudioClip click;

	abstract public void LaserEnter(Laser laser);
	abstract public void LaserExit(Laser laser);
	abstract public void PointClick(Laser laser);
	abstract public void PointPress(Laser laser);
	abstract public void PointDepress(Laser laser);

	void Awake() {
		if(gameObject.tag != "InteractiveObject") {
			//Debug.LogError(transform.name + " has a '" + transform.tag + "' tag. InteractiveObject class needs a 'InteractiveObject' tag");
		}
	}

}
