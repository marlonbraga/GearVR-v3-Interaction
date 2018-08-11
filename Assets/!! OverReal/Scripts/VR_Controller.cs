using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Controller : MonoBehaviour {

	static public VR_Controller _VRInput;
	void Awake() {
		_VRInput = this;
	}

	#region GearVR
	static public bool TriggerButton() {
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
			return true;
		else
			return false;
	}
	static public bool TriggerButtonDown() {
		if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
			return true;
		else
			return false;
	}
	static public bool TouchButton() {
		if(OVRInput.Get(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	static public bool isTouch() {
		if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	static public bool TouchGetDown() {
		if(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	static public bool TouchGetUp() {
		if(OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	static public Vector2 TouchPoint() {
		return OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	}
	

	#endregion

}
