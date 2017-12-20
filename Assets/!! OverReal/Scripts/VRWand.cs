using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Laser))]
public class VRWand:MonoBehaviour {

	[Range(0, 10)]
	public float maxDistance;
	private Laser laser;
	private InteractiveObject lastHit;
	void Start() {
		laser = GetComponent<Laser>();

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
				lastHit.LaserEnter(laser);
				if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) {
					lastHit.PointClick(laser);
					lastHit.PointPress(laser);
				}
			} else {
				laser.ResetColor();
				if(lastHit) {
					lastHit.PointDepress(laser);
					lastHit.LaserExit(laser);
					lastHit = null;
				}
			}
		} else {
			laser.ResetColor();
			if(lastHit) {
				lastHit.PointDepress(laser);
				lastHit.LaserExit(laser);
				lastHit = null;
			}
			foreach(var item in GameObject.FindGameObjectsWithTag("InteractiveObject")) {
				if(item.GetComponent<InteractiveObject>())
					item.GetComponent<InteractiveObject>().PointDepress(laser);
			}
		}
	}
}