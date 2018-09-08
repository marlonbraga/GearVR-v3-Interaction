using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class VRAvatar:MonoBehaviour {
	[SerializeField]
	private float minDistance = 1.5f;
	public static VRAvatar _VRAvatar;
	public bool canMove = false;
	public AudioClip[] steps;
	[HideInInspector] public Inventary inventary;
	[HideInInspector]
	public Equipament equipamentInUse;
	public InventaryMenu inventaryMenu;
	private AudioSource audioSource;
	private Coroutine coroutine;
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
		DebugScreen.debugScreen.AddWrite("<color=yellow>VRAvatar: Teleport()</color>");

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
	void Update() {
		if(GameConfiguration._VRInput.TouchButtonDown()) {
			_VRAvatar.inventaryMenu.gameObject.transform.position = transform.position;
			_VRAvatar.inventaryMenu.gameObject.transform.rotation = transform.rotation;
			_VRAvatar.inventaryMenu.gameObject.SetActive(!inventaryMenu.gameObject.activeInHierarchy);
		}
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