using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventaryMenu:MonoBehaviour {
	//private InventaryButton[] inventaryButton;
	[SerializeField] private Equipament defautEquip;
	[SerializeField] private List<GameObject> RaycastsIgnored = new List<GameObject>();
	public void Rotate(int slots) {
		transform.Rotate(0, 15 * slots, 0);
	}
	private void OnEnable() {
		VRAvatar._VRAvatar.UnequipAll();
		defautEquip.gameObject.SetActive(true);
	}
	private void OnDisable() {
		VRAvatar._VRAvatar.UnequipAll();
		VRAvatar._VRAvatar.equipamentInUse.gameObject.SetActive(true);
		foreach(var item in RaycastsIgnored) {
			item.layer = 0;
		}
		RaycastsIgnored.Clear();
	}
	private void OnTriggerEnter(Collider other) {
		if(!other.gameObject.GetComponent<InventoryButton>() && !other.gameObject.GetComponent<Floor>() && !other.gameObject.GetComponent<FootPrint>()) {
			other.gameObject.layer = 2;
			RaycastsIgnored.Add(other.gameObject);
			DebugScreen.debugScreen.AddWrite("<color=blue>"+other.name+ "</color> Add to RaycastsIgnored list");
		}
	}
}