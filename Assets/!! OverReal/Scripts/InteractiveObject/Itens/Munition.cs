using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Munition : Item {
	override protected void action(){
		VRAvatar._VRAvatar.inventary.bullets += 10;
		DebugScreen.debugScreen.AddWrite("<#3333dd> 10 Bullets </color> added to inventory!");
		DebugScreen.debugScreen.AddWrite("Bullets on inventory: <#3333dd> "+ VRAvatar._VRAvatar.inventary.bullets + " </color>");
	}
}
