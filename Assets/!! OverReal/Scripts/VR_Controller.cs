using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VR_Controller : MonoBehaviour {

	#region GearVR
	virtual public bool TriggerButton() {
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
			return true;
		else
			return false;
	}
	virtual public bool TriggerButtonDown() {
		if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
			return true;
		else
			return false;
	}
	virtual public bool TouchButton() {
		if(OVRInput.Get(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	virtual public bool TouchButtonDown() {
		if(OVRInput.GetDown(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	virtual public bool TouchButtonUp() {
		if(OVRInput.GetUp(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	virtual public bool isTouch() {
		if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	virtual public bool TouchGetDown() {
		if(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	virtual public bool TouchGetUp() {
		if(OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	virtual public Vector2 TouchPoint() {
		return OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	}
	

	#endregion

}
