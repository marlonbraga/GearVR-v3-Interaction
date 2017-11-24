using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OutlineEffect))]
public class VRWand:MonoBehaviour {

	public GameObject cursor;
	[Range(0, 10)]
	public float MaxDistance;
	private Material cursorMaterial;
	void Start() {
		//cursor = transform.GetChild(0).gameObject;
		cursorMaterial = cursor.GetComponent<MeshRenderer>().materials[0];
	}
	void Update() {
		Vector3 fwd = transform.TransformDirection(Vector3.forward * MaxDistance);
		Ray ray = new Ray(transform.position, fwd);

		RaycastHit hit;
		if(!Physics.Raycast(ray, out hit, 100))
			Debug.LogError("VR Wand does not Raycasting. Check layers");
		if(hit.collider.gameObject != cursor) {
			Debug.DrawLine(ray.origin, hit.point, Color.green);
			cursorMaterial.color = Color.green;
			if(hit.collider.tag == "InteractiveObject") {
				hit.collider.gameObject.GetComponent<Outline>().enabled = true;
				if(hit.collider.gameObject.GetComponent<InteractiveObject>())
					hit.collider.gameObject.GetComponent<InteractiveObject>().Select();
			}
		} else {
			Debug.DrawRay(ray.origin, ray.direction, Color.red);
			cursorMaterial.color = Color.white;
			foreach(var item in GameObject.FindGameObjectsWithTag("InteractiveObject")) {
				try {
					item.GetComponent<Outline>().enabled = false;
				} catch {
					if(item.GetComponent<Outline>())
						Debug.Log("Outline was found in " + item.name);
					else
						Debug.LogError("Outline not found in " + item.name);
				}
				if(item.GetComponent<InteractiveObject>())
					item.GetComponent<InteractiveObject>().Deselect();
			}
		}
	}
	//void VRControler() {
	//	// The Gear VR Controller has a clickable trackpad, so we can differentiate between
	//	// the finger simply touching the pad and actively depressing it.

	//	// is player using a controller?
	//	if(OVRInput.GetActiveController() == OVRInput.Controller.LTrackedRemote ||
	//		OVRInput.GetActiveController() == OVRInput.Controller.RTrackedRemote) {
	//		// yes, are they touching the touchpad?
	//		if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad)) {
	//			// yes, let's require an actual click rather than just a touch.
	//			if(OVRInput.Get(OVRInput.Button.PrimaryTouchpad)) {
	//				// button is depressed, handle the touch.
	//				Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	//				//ProcessControllerClickAtPosition(touchPosition);
	//			}
	//		}
	//	} else if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad)) // finger on HMD pad?
	//	  {
	//		// not using controller, same behavior as before.
	//		Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	//		//ProcessHMDClickAtPosition(touchPosition);
	//	}
	//}
}