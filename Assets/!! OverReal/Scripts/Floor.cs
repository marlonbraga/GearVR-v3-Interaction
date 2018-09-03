using UnityEngine;


public class Floor:InteractiveObject {
	private Color overColor = new Color(0.2f, 0.5f, 0.2f, 0.5f);
	private Color pressColor = new Color(0.2f, 0.5f, 0.2f, 0.8f);

	public override void LaserEnter(RaycastHit hit) {
		if(Vector3.Distance(hit.point, VRWand._VRWand.transform.position) < 5) {
			Laser._Laser.ChangeLaserColor(overColor);
			VRWand._VRWand.footprint.SetActive(true);
			VRWand._VRWand.footprint.transform.position = hit.point;

			VRWand._VRWand.footprint.transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
			Vector3 Q = VRWand._VRWand.footprint.transform.localRotation.eulerAngles;
			VRWand._VRWand.footprint.transform.localRotation = Quaternion.Euler(Q.x, Q.y, Q.z);
		} else {
			LaserExit();
		}
	}
	public override void LaserExit() {
		Laser._Laser.ResetColor();
		VRWand._VRWand.footprint.SetActive(false);
	}
	public override void PointClick(Vector3 target) {
		if(Vector3.Distance(target, VRWand._VRWand.transform.position) < 5)
			VRAvatar._VRAvatar.Movement(target);
	}
}
