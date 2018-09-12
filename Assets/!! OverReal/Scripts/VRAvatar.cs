using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class VRAvatar:MonoBehaviour {
	[SerializeField] private float minDistance = 1.5f;
	public static VRAvatar _VRAvatar;
	public bool canMove = false;
	public AudioClip[] steps;
	[HideInInspector] public Inventary inventary;
	[HideInInspector] public Equipament equipamentInUse;
	public InventaryMenu inventaryMenu;
	private AudioSource audioSource;
	private Coroutine coroutine;
	[HideInInspector] private float snapTuringAngle = 45;
	void Start() {
		inventary = new Inventary(true, true, 20);
		_VRAvatar = this;
		audioSource = GetComponent<AudioSource>();
	}
	public void Movement(Vector3 newTarget) {
		DebugScreen.debugScreen.AddWrite("<color=yellow>VRAvatar: Movement()</color>");
		if(canMove) {
			if(coroutine != null)
				StopCoroutine(coroutine);
			coroutine = StartCoroutine(Teleport(newTarget));
		}
	}
	private IEnumerator Teleport(Vector3 newTarget) {
		InventaryMenuClose();
		transform.position = newTarget;
		yield return new WaitForSeconds(0f);
		foreach(var step in steps) {
			audioSource.clip = step;
			audioSource.Play();
			while(audioSource.isPlaying) {
				yield return new WaitForSeconds(0.01f);
			}
		}
	}
	protected void generalControllers() {
		//Inventory
		if(GameConfiguration._VRInput.TouchButton2Down()) {
			DebugScreen.debugScreen.AddWrite("Inventory");
			_VRAvatar.inventaryMenu.gameObject.transform.position = transform.position;
			_VRAvatar.inventaryMenu.gameObject.transform.rotation = transform.rotation;
			_VRAvatar.inventaryMenu.gameObject.SetActive(!_VRAvatar.inventaryMenu.gameObject.activeInHierarchy);
		}
		//SnapTuring
		if(GameConfiguration._VRInput.TouchButton1Down()) {
			float touchX = GameConfiguration._VRInput.TouchPoint().x;

			if(touchX >= -1.0f && touchX < -0.8f) {
				DebugScreen.debugScreen.AddWrite("SnapTuring: <color=yellow>Left</color>");
				_VRAvatar.transform.Rotate(0, -snapTuringAngle, 0);
			} else if(touchX > 0.8f && touchX <= 1.0f) {
				DebugScreen.debugScreen.AddWrite("SnapTuring: <color=yellow>Right</color>");
				_VRAvatar.transform.Rotate(0, +snapTuringAngle, 0);
			}
		}
	}
	void Update() {
		generalControllers();
	}
	void InventaryMenuClose() {
		_VRAvatar.inventaryMenu.gameObject.SetActive(false);
		EquipItem(equipamentInUse);
	}
	public void EquipItem(Equipament equipament) {
		equipamentInUse = equipament;
		UnequipAll(transform);
		equipament.gameObject.SetActive(true);
		DebugScreen.debugScreen.AddWrite("<#dd3333>" + equipament.name + "</color> Equiped");
		_VRAvatar.inventaryMenu.gameObject.SetActive(false);
	}
	[ContextMenu("UnequipAll")]
	public void UnequipAll(Transform parent) {
		foreach(Transform child in parent) {
			if(child.GetComponent<Equipament>())
				child.gameObject.SetActive(false);
			else
				UnequipAll(child);
		}
	}
	public void UnequipAll() {
		UnequipAll(transform);
	}
}