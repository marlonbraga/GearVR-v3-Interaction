using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Floor:InteractiveObject {
	private Color overColor = new Color(0.2f, 0.5f, 0.2f, 0.5f);
	private Color pressColor = new Color(0.2f, 0.5f, 0.2f, 0.8f);

	public override void LaserEnter(RaycastHit hit) {
		Laser._Laser.ChangeLaserColor(overColor);
		VRWand._VRWand.footprint.SetActive(true);
		VRWand._VRWand.footprint.transform.position = hit.point;

		VRWand._VRWand.footprint.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
		Vector3 Q = VRWand._VRWand.footprint.transform.localRotation.eulerAngles;
		VRWand._VRWand.footprint.transform.localRotation = Quaternion.Euler(Q.x/*VRWand._VRWand.transform.localRotation.eulerAngles.x*/, Q.y, Q.z);
	//	VRWand._VRWand.footprint.transform.localRotation = Quaternion.Euler(Q.x/*VRWand._VRWand.transform.localRotation.eulerAngles.x*/, Q.y, Q.z);

	}
	public override void LaserExit() {
		Laser._Laser.ResetColor();
		VRWand._VRWand.footprint.SetActive(false);
	}
	public override void PointClick(Vector3 target) {
		VRAvatar._VRAvatar.Movement(target);
	}
}
