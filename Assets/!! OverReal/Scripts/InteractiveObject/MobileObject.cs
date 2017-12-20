using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileObject : InteractiveObject {
	public bool statusSwitch = false;
	public bool holding = false;
	private Color overColor = new Color(0.0f, 0.4f, 0.4f, 0.5f);
	private Color pressColor = new Color(0.5f, 0.2f, 0.5f, 0.5f);

	public override void LaserEnter(Laser laser) {
		laser.ChangeLaserColor(overColor);
	}
	public override void LaserExit(Laser laser) {
		laser.ResetColor();
	}
	public override void PointClick(Laser laser) {
		SetStatusSwitch(true);
	}
	public override void PointDepress(Laser laser) {
		holding = false;
	}
	public override void PointPress(Laser laser) {
		holding = true;
	}
	void SetStatusSwitch(bool newStatus) {
		statusSwitch = newStatus;
	}
}
