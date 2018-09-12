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
	override public bool TouchButton1() {
		if(OVRInput.Get(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	override public bool TouchButton1Down() {
		if(OVRInput.GetDown(OVRInput.Button.One))
			return true;
		else
			return false;
	}
	override public bool TouchButton2() {
		if(OVRInput.Get(OVRInput.Button.Two))
			return true;
		else
			return false;
	}
	override public bool TouchButton2Down() {
		if(OVRInput.GetDown(OVRInput.Button.Two))
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
	//override public bool Swipe() {
	//	if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad) && (!OVRInput.Get(OVRInput.Button.One)) && (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))) {
	//		if(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad)) {
	//			initialtouchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	//		} else if(OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad)) {
	//			initialtouchPosition = Vector2.zero;
	//		}
	//		Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
	//		Vector2 resultPosition = touchPosition - initialtouchPosition;

	//		float deadZone = 0.3f;
	//		if(resultPosition.y > deadZone) {
	//			moveForward = true;
	//			dpad_move = true;
	//			MoveScale = Math.Min(Math.Abs(resultPosition.y), 1f);
	//		} else if(resultPosition.y < -deadZone) {
	//			moveBack = true;
	//			dpad_move = true;
	//			MoveScale = Math.Min(Math.Abs(resultPosition.y), 1f);
	//		}
	//		if(resultPosition.x > deadZone) {
	//			moveRight = true;
	//			dpad_move = true;
	//			MoveScale = Math.Min(Math.Abs(resultPosition.x), 1f);
	//		} else if(resultPosition.x < -deadZone) {
	//			moveLeft = true;
	//			dpad_move = true;
	//			MoveScale = Math.Min(Math.Abs(resultPosition.x), 1f);
	//		}

	//		string text = "| initialPosition: " + initialtouchPosition +
	//						"\n| touchPosition:   " + touchPosition +
	//						"\n| resultPosition:  " + resultPosition +
	//						"\n| moveScale:       " + MoveScale;
	//		//textmesh.text = text;
	//	}
	//}
}
