using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : ScenaryButton {
	[SerializeField] protected Equipament equipament;
	protected override void Action(){
		VRAvatar._VRAvatar.EquipItem(equipament);
		DebugScreen.debugScreen.AddWrite("PointClick(): InventoryButton ["+equipament.name+"]");
	}
}
//FFFFFF87
//82FFB287
//BCFFBCA0
//9C000087