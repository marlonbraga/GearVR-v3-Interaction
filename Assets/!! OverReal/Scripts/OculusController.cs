using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusController : VR_Controller {

	override public bool TriggerButton() {
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
			return true;
		else
			return false;
	}
	override public bool TriggerButtonDown() {
		if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
			return true;
		else
			return false;
	}
	override public bool TouchButton() {
		if(OVRInput.Get(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	override public bool TouchButtonDown() {
		if(OVRInput.GetDown(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	override public bool TouchButtonUp() {
		if(OVRInput.GetUp(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	override public bool isTouch() {
		if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	override public bool TouchGetDown() {
		if(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	override public bool TouchGetUp() {
		if(OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad))
			return true;
		else
			return false;
	}
	override public Vector2 TouchPoint() {
		return OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	}
}
