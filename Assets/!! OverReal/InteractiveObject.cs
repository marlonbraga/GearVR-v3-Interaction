using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
abstract public class InteractiveObject:MonoBehaviour {

	public AudioClip select;
	public AudioClip click;

	abstract public void Select();
	abstract public void Deselect();
	abstract public void Click();

	void Awake(){
		if(gameObject.tag != "InteractiveObject"){
			Debug.LogError(transform.name+ " has a '"+transform.tag+"' tag. InteractiveObject class needs a 'InteractiveObject' tag");
		}
	}
}
