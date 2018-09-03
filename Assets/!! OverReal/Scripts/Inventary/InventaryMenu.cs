using UnityEngine;

public class InventaryMenu : MonoBehaviour {
	//private InventaryButton[] inventaryButton;
	[SerializeField]
	private Equipament defautEquip;
	public void Rotate(int slots) {
		transform.Rotate(0, 15*slots, 0);
	}
	private void OnEnable() {
		VRAvatar._VRAvatar.UnequipAll();
		defautEquip.gameObject.SetActive(true);
	}
	private void OnDisable() {
		VRAvatar._VRAvatar.UnequipAll();
		VRAvatar._VRAvatar.equipamentInUse.gameObject.SetActive(true);
	}
}