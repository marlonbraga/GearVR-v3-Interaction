using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class MobileObject : InteractiveObject {
	public bool statusSwitch = false;
	public bool holding = false;
	private Color overColor = new Color(0.0f, 0.4f, 0.4f, 0.5f);
	private Color pressColor = new Color(0.5f, 0.2f, 0.5f, 0.5f);

	public override void LaserEnter(RaycastHit hit) {
		Laser._Laser.ChangeLaserColor(overColor);
	}
	public override void LaserExit() {
		Laser._Laser.ResetColor();
	}
	public override void PointClick(Vector3 vector3) {
		SetStatusSwitch(true);
	}
	public override void PointDepress() {
		holding = false;
	}
	public override void PointPress() {
		holding = true;
	}
	void SetStatusSwitch(bool newStatus) {
		statusSwitch = newStatus;
	}
}
