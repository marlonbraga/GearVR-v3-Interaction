﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Laser),typeof(VRAvatar))]
public class VRWand:MonoBehaviour {

	[Range(0, 10)]
	public float maxDistance;
	public GameObject footprint;
	public static VRWand _VRWand;
	private VRAvatar vrAvatar;
	private InteractiveObject lastHit;
	void Start() {
		_VRWand = this;
	}
	void Update() {
		CheckHitRaycast();
	}
	void CheckHitRaycast() {
		Vector3 fwd = transform.TransformDirection(Vector3.forward * maxDistance);
		Ray ray = new Ray(transform.position, fwd);

		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 200)) {
			if(hit.collider.gameObject.GetComponent<InteractiveObject>()) {
				lastHit = hit.collider.gameObject.GetComponent<InteractiveObject>();
				lastHit.LaserEnter(hit);
				if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
					lastHit.PointClick(hit.point);
					lastHit.PointPress();
				}
			} else {
				Laser._Laser.ResetColor();
				if(lastHit) {
					lastHit.PointDepress();
					lastHit.LaserExit();
					lastHit = null;
				}
			}
		} else {
			Laser._Laser.ResetColor();
			if(lastHit) {
				lastHit.PointDepress();
				lastHit.LaserExit();
				lastHit = null;
			}
			foreach(var item in GameObject.FindGameObjectsWithTag("InteractiveObject")) {
				if(item.GetComponent<InteractiveObject>())
					item.GetComponent<InteractiveObject>().PointDepress();
			}
		}
	}
}