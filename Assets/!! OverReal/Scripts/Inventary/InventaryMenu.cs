using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventaryMenu : MonoBehaviour {
	//private InventaryButton[] inventaryButton;

	public void Rotate(int slots) {
		transform.Rotate(0, 15*slots, 0);
	}
}