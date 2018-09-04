using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventary : MonoBehaviour {

	//equipaments
	public bool wand = true;
	public bool gun = true;

	//stuffs
	public int bullets = 0;

	//constructors
	public Inventary() {
		wand = true;
		gun = false;
		bullets = 0;
	}
	public Inventary(bool _wand, bool _gun, int _bullets) {
		wand = _wand;
		gun = _gun;
		bullets = _bullets;
	}
}