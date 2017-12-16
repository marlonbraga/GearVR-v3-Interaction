using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSphere:MonoBehaviour {

	// Use this for initialization
	void Start() {
		tag = "ActionObject";
	}

	public void Action() {
		if(GetComponent<MeshRenderer>().material.color != Color.green)
			GetComponent<MeshRenderer>().material.color = Color.green;
		else
			GetComponent<MeshRenderer>().material.color = Color.grey;
	}
}
