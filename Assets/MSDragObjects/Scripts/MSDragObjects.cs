using System;
using System.Collections;
using UnityEngine;

public class MSDragObjects:MonoBehaviour {

	[Tooltip("The key that must be pressed to move the objects.")]
	public KeyCode KeyToMove = KeyCode.Mouse0;
	[Tooltip("The key that must be pressed to throwing the objects.")]
	public KeyCode KeyToThrowing = KeyCode.Mouse1;
	[Tooltip("The key that must be pressed to rotate the objects.")]
	public KeyCode keyToRotate = KeyCode.R;

	[Space(15)]
	[Tooltip("If this variable is checked, the mouse is automatically hidden.")]
	public bool hideTheMouse = false;
	[Tooltip("If this variable is true, the code will automatically adjust the player layer to 'IgnoreRaycast'")]
	public bool setLayerInPlayer = true;
	[Tooltip("Sets whether the cursor is free, locked and invisible or only limited.")]
	public CursorLockMode _cursorLockMode = CursorLockMode.None;

	[Range(0.1f, 5.0f)]
	[Tooltip("The closest you can bring your player's camera object.")]
	public float minDistance = 0.5f;
	[Range(0.2f, 9.0f)]
	[Tooltip("As far as you can bring your player's camera object.")]
	public float maxDistance = 1;
	[Range(1.0f, 10.0f)]
	[Tooltip("The speed at which the object can be moved.")]
	public float speedOfMovement = 5;
	[Range(10.0f, 100.0f)]
	[Tooltip("The speed at which the object can be rotated.")]
	public float speedOfRotation = 50;
	[Range(5.0f, 15.0f)]
	[Tooltip("The speed at which the object can be approached or disapproved.")]
	public float speedScrool = 10;
	[Tooltip("The force with which the object can be thrown.")]
	public float throwingForce = 800;
	[Space(10)]
	[Tooltip("The force with which the object can be moved. This variable only takes effect if the selected motion type is 'AddForce'.")]
	public float moveForce = 200;

	public TextMesh textMesh;

	bool canMove;
	bool isMoving;
	float distance;
	float rotXTemp;
	float rotYTemp;
	float tempDistance;
	RaycastHit tempHit;
	Rigidbody rbTemp;
	Vector3 rayEndPoint;
	//Vector3 velocity = Vector3.zero;
	Vector3 tempDirection;
	Vector3 tempSpeed;
	Vector3 direcAddForceMode;
	GameObject tempObject;
	public static bool rotatingObject;
	Camera mainCamera;
	private Vector2 initialtouchPosition = Vector2.zero;
	private bool distanceChange = false;

	void Awake() {
		textMesh.text = "000";
		distance = (minDistance + maxDistance) / 2;
		mainCamera = Camera.main;
		if(!mainCamera) {
			Debug.LogError("The camera containing the code must have the 'MainCamera' tag so that you can rotate the objects.");
		}
		if(hideTheMouse) {
			Cursor.visible = false;
		}
		Cursor.lockState = _cursorLockMode;
		if(setLayerInPlayer) {
			GameObject refTemp = transform.root.gameObject;
			refTemp.layer = 2;
			foreach(Transform trans in refTemp.GetComponentsInChildren<Transform>(true)) {
				trans.gameObject.layer = 2;
			}
		}
	}
	bool IsMovementBlock() {
		if(tempObject) 
			if(Physics.Raycast(transform.position, Vector3.down, out tempHit, 5))
				if(tempHit.transform.GetComponent<MobileObject>() && tempObject.transform.gameObject == tempHit.transform.gameObject)
					return true;
		return false;
	}
	void Update(){
		rayEndPoint = transform.position + transform.forward * distance;
		if(Physics.Raycast(transform.position, transform.forward, out tempHit, (maxDistance + 1))) {
			if(Vector3.Distance(transform.position, tempHit.point) <= maxDistance && tempHit.transform.GetComponent<MobileObject>()) {
				canMove = true;
			} else {
				canMove = false;
			}
			PointAndClick();
		} else {
			canMove = false;
		}
		TouchPad();
		if(tempObject) {
			rbTemp = tempObject.GetComponent<Rigidbody>();
		}
		if(IsMovementBlock() && tempObject) {
			rbTemp.useGravity = true;
			tempObject = null;
			rbTemp = null;
			isMoving = false;
		}
		CheckButton();
		Throwing();
		Distance();
		RotateObject();
	}
	void FixedUpdate() {
		MoveObject();
	}
	void TouchPad(){
		if((VR_Controller.isTouch() && (!VR_Controller.TouchButton()))) {
			//READ TOUCHPAD
			if(VR_Controller.TouchGetDown()) {
				initialtouchPosition = VR_Controller.TouchPoint();
			} else if(VR_Controller.TouchGetUp()) {
				initialtouchPosition = Vector2.zero;
			}
			Vector2 touchPosition = VR_Controller.TouchPoint();
			Vector2 resultPosition = touchPosition - initialtouchPosition;

			//DEADZONE
			float deadZone = 0.3f;
			if(resultPosition.y > deadZone) {
				distanceChange = true;
			} else if(resultPosition.y < -deadZone) {
				distanceChange = true;
			} else {
				distanceChange = false;
			}

			//CHANGE DISTANCE
			if(distanceChange) {
				distance += resultPosition.y * speedScrool;
				distance = Mathf.Clamp(distance, minDistance, maxDistance);
			}
		}
	}
	//IS BUTTON PRESSED ??
	void CheckButton(){
		textMesh.text = GameConfig.vr_controller.TriggerButton() + " \n " + canMove + " \n " + tempObject;
		if((Input.GetKeyUp(KeyToMove) && tempObject) || (!GameConfig.vr_controller.TriggerButton()) && tempObject) {	
			rbTemp.useGravity = true;
			tempObject = null;
			rbTemp = null;
			isMoving = false;
		}
	}
	void Throwing(){
		if((Input.GetKeyDown(KeyToThrowing) && tempObject) || (VR_Controller.TouchButton()) && (tempObject)) {
			tempDirection = rayEndPoint - transform.position;
			tempDirection.Normalize();
			rbTemp.useGravity = true;
			rbTemp.AddForce(tempDirection * throwingForce * 4);
			tempObject = null;
			rbTemp = null;
			isMoving = false;
			GetComponent<AudioSource>().Play();
		}
	}
	//HOW THE OBJECT IS FAR ??
	void Distance() {
		if(tempObject) {
			if(Vector3.Distance(transform.position, tempObject.transform.position) > maxDistance) {
				rbTemp.useGravity = true;
				tempObject = null;
				rbTemp = null;
				isMoving = false;
			}
		}
	}
	void RotateObject() {
		if(tempObject && mainCamera) {
			if(Input.GetKey(keyToRotate)) {
				rotatingObject = true;
				rotXTemp = Input.GetAxis("Mouse X") * speedOfRotation / 10;
				rotYTemp = Input.GetAxis("Mouse Y") * speedOfRotation / 10;
				tempObject.transform.Rotate(mainCamera.transform.up, -rotXTemp, Space.World);
				tempObject.transform.Rotate(mainCamera.transform.right, rotYTemp, Space.World);
			}
			if(Input.GetKeyUp(keyToRotate)) {
				rotatingObject = false;
			}
		} else {
			rotatingObject = false;
		}
	}
	void MoveObject() {
		if(tempObject) {
			rbTemp = tempObject.GetComponent<Rigidbody>();
			rbTemp.angularVelocity = new Vector3(0, 0, 0);

			tempSpeed = (rayEndPoint - rbTemp.transform.position);
			tempSpeed.Normalize();
			tempDistance = Vector3.Distance(rayEndPoint, rbTemp.transform.position);
			tempDistance = Mathf.Clamp(tempDistance, 0, 1);
			direcAddForceMode = tempSpeed * speedOfMovement * moveForce * tempDistance;

			rbTemp.velocity = Vector3.zero;
			rbTemp.AddForce(direcAddForceMode, ForceMode.Force);
		}
	}
	void PointAndClick() {
		if((Input.GetKeyDown(KeyToMove) && canMove) || (VR_Controller.TriggerButtonDown() && canMove)) {
			if(tempHit.rigidbody) {
				tempHit.rigidbody.useGravity = true;
				distance = Vector3.Distance(transform.position, tempHit.point);
				tempObject = tempHit.transform.gameObject;
				isMoving = true;
			} else {
				Debug.LogWarning("The target object does not have the 'Rigidbody' component. This way, you can not interact with it.");
			}
		}
	}
}