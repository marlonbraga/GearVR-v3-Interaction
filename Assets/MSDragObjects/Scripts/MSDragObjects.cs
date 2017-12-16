using System;
using System.Collections;
using UnityEngine;

public class MSDragObjects:MonoBehaviour {

	public enum TypeSelect { Tag, Name, PhysicsMaterial };
	[Tooltip("The way the code will detect the objects, can be by tag, by name or by 'physics material'.")]
	public TypeSelect detectionMode = TypeSelect.Tag;
	[Tooltip("The tag of objects that can be moved with the mouse.")]
	public string tagObjects = "Respawn";
	[Tooltip("The name of objects that can be moved with the mouse.")]
	public string nameObjects = "Cube";
	[Tooltip("The 'physic material' of objects that can be moved with the mouse.")]
	public PhysicMaterial physicMaterialObjects;

	[Space(15)]
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

	public enum TypeMove { position, velocity, addForce };
	[Space(15), Tooltip("Here it is possible to decide the type of movement that the script will make to the rigid body of the objects that can be moved, so that the system adapts better to several situations.")]
	public TypeMove TypeOfMovement = TypeMove.addForce;
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
	[Space(15), Tooltip("A 'closed hand' texture, representing that the object is being moved.")]
	public Texture closedHandTexture;
	[Tooltip("A 'open hand' texture, representing that the object can be moved.")]
	public Texture openHandTexture;

	bool canMove;
	bool blockMovement;
	bool isMoving;
	float distance;
	float rotXTemp;
	float rotYTemp;
	float tempDistance;
	RaycastHit tempHit;
	Rigidbody rbTemp;
	Vector3 rayEndPoint;
	Vector3 velocity = Vector3.zero;
	Vector3 tempDirection;
	Vector3 tempSpeed;
	Vector3 direcAddForceMode;
	GameObject tempObject;
	public static bool rotatingObject;
	Camera mainCamera;
	private Vector2 initialtouchPosition = Vector2.zero;
	private bool distanceChange = false;

	void Awake() {
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

	void Update() {
		//raycast vector3.down
		if(tempObject) {
			if(Physics.Raycast(transform.position, Vector3.down, out tempHit, 5)) {
				switch(detectionMode) {
					case TypeSelect.Tag:
						if(tempHit.transform.tag == tagObjects && tempObject.transform.gameObject == tempHit.transform.gameObject) {
							blockMovement = true;
						} else {
							blockMovement = false;
						}
						break;
					case TypeSelect.Name:
						if(tempHit.transform.name == nameObjects && tempObject.transform.gameObject == tempHit.transform.gameObject) {
							blockMovement = true;
						} else {
							blockMovement = false;
						}
						break;
					case TypeSelect.PhysicsMaterial:
						if(physicMaterialObjects) {
							if(tempHit.transform.GetComponent<Collider>().sharedMaterial == physicMaterialObjects && tempObject.transform.gameObject == tempHit.transform.gameObject) {
								blockMovement = true;
							} else {
								blockMovement = false;
							}
						} else {
							blockMovement = false;
						}
						break;
				}
			} else {
				blockMovement = false;
			}
		} else {
			blockMovement = false;
		}

		//raycast camera forward
		rayEndPoint = transform.position + transform.forward * distance;
		if(Physics.Raycast(transform.position, transform.forward, out tempHit, (maxDistance + 1))) {
			switch(detectionMode) {
				case TypeSelect.Tag:
					if(Vector3.Distance(transform.position, tempHit.point) <= maxDistance && tempHit.transform.tag == tagObjects) {
						canMove = true;
					} else {
						canMove = false;
					}
					break;
				case TypeSelect.Name:
					if(Vector3.Distance(transform.position, tempHit.point) <= maxDistance && tempHit.transform.name == nameObjects) {
						canMove = true;
					} else {
						canMove = false;
					}
					break;
				case TypeSelect.PhysicsMaterial:
					if(physicMaterialObjects) {
						if(Vector3.Distance(transform.position, tempHit.point) <= maxDistance && tempHit.transform.GetComponent<Collider>().sharedMaterial == physicMaterialObjects) {
							canMove = true;
						} else {
							canMove = false;
						}
					} else {
						canMove = false;
					}
					break;
			}
			//
			if((Input.GetKeyDown(KeyToMove) && canMove) || (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && canMove)) {
				if(tempHit.rigidbody) {
					switch(TypeOfMovement) {
						case TypeMove.position:
							tempHit.rigidbody.useGravity = false;
							break;
						case TypeMove.velocity:
							tempHit.rigidbody.useGravity = false;
							break;
						case TypeMove.addForce:
							tempHit.rigidbody.useGravity = true;
							break;
					}
					distance = Vector3.Distance(transform.position, tempHit.point);
					tempObject = tempHit.transform.gameObject;
					isMoving = true;
				} else {
					Debug.LogWarning("The target object does not have the 'Rigidbody' component. This way, you can not interact with it.");
				}
			}
		} else {
			canMove = false;
		}

		//OverLib - Distance
		if((OVRInput.Get(OVRInput.Touch.PrimaryTouchpad) && (!OVRInput.Get(OVRInput.Button.One)))) {

			if(OVRInput.GetDown(OVRInput.Touch.PrimaryTouchpad)) {
				initialtouchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
			} else if(OVRInput.GetUp(OVRInput.Touch.PrimaryTouchpad)) {
				initialtouchPosition = Vector2.zero;
			}
			Vector2 touchPosition = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
			Vector2 resultPosition = touchPosition - initialtouchPosition;

			float deadZone = 0.3f;
			if(resultPosition.y > deadZone) {
				distanceChange = true;
			} else if(resultPosition.y < -deadZone) {
				distanceChange = true;
			} else {
				distanceChange = false;
			}
			if(distanceChange) {
				distance += resultPosition.y * speedScrool;
				distance = Mathf.Clamp(distance, minDistance, maxDistance);
			}
		}
		
		if(tempObject) {
			rbTemp = tempObject.GetComponent<Rigidbody>();
		}

		if(blockMovement && tempObject) {
			rbTemp.useGravity = true;
			tempObject = null;
			rbTemp = null;
			isMoving = false;
		}
		if((Input.GetKeyUp(KeyToMove) && tempObject) || (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && tempObject)) {
			rbTemp.useGravity = true;
			tempObject = null;
			rbTemp = null;
			isMoving = false;
		}
		if((Input.GetKeyDown(KeyToThrowing) && tempObject) || (OVRInput.Get(OVRInput.Button.One) && (tempObject))) {
			tempDirection = rayEndPoint - transform.position;
			tempDirection.Normalize();
			rbTemp.useGravity = true;
			rbTemp.AddForce(tempDirection * throwingForce * 4);
			tempObject = null;
			rbTemp = null;
			isMoving = false;
		}
		if(tempObject) {
			if(Vector3.Distance(transform.position, tempObject.transform.position) > maxDistance) {
				rbTemp.useGravity = true;
				tempObject = null;
				rbTemp = null;
				isMoving = false;
			}
		}

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

	void FixedUpdate() {
		if(tempObject) {
			rbTemp = tempObject.GetComponent<Rigidbody>();
			rbTemp.angularVelocity = new Vector3(0, 0, 0);
			switch(TypeOfMovement) {
				case TypeMove.position:
					rbTemp.velocity = new Vector3(0, 0, 0);
					rbTemp.position = Vector3.SmoothDamp(tempObject.transform.position, rayEndPoint, ref velocity, (1 / speedOfMovement));
					break;
				case TypeMove.velocity:
					tempSpeed = (rayEndPoint - rbTemp.transform.position);
					tempSpeed.Normalize();
					tempDistance = Vector3.Distance(rayEndPoint, rbTemp.transform.position);
					tempDistance = Mathf.Clamp(tempDistance, 0, 1);
					rbTemp.velocity = Vector3.Lerp(rbTemp.velocity, tempSpeed * speedOfMovement * 1.2f * tempDistance, Time.deltaTime * 15);
					break;
				case TypeMove.addForce:
					tempSpeed = (rayEndPoint - rbTemp.transform.position);
					tempSpeed.Normalize();
					tempDistance = Vector3.Distance(rayEndPoint, rbTemp.transform.position);
					tempDistance = Mathf.Clamp(tempDistance, 0, 1);
					direcAddForceMode = tempSpeed * speedOfMovement * moveForce * tempDistance;

					rbTemp.velocity = Vector3.zero;
					rbTemp.AddForce(direcAddForceMode, ForceMode.Force);
					break;
			}
		}
	}

	void OnGUI() {
		if(canMove && !isMoving && openHandTexture) {
			GUI.DrawTexture(new Rect(Screen.width / 2 - openHandTexture.width / 2, Screen.height / 2 - openHandTexture.height / 2, openHandTexture.width, openHandTexture.height), openHandTexture);
		}
		if(isMoving && closedHandTexture) {
			GUI.DrawTexture(new Rect(Screen.width / 2 - closedHandTexture.width / 2, Screen.height / 2 - closedHandTexture.height / 2, closedHandTexture.width, closedHandTexture.height), closedHandTexture);
		}
	}
}