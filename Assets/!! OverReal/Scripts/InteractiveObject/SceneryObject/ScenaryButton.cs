﻿using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ScenaryButton:ScenaryObject {
	[SerializeField] protected GameObject objectSelected;
	[SerializeField] protected Color defaultColor;
	[SerializeField] protected Color pressingColor;
	[SerializeField] protected Color pointingColor;
	[SerializeField] protected Color disableColor;
	void Start() {
		DebugScreen.debugScreen.AddWrite("START: ScenaryButton");
	}
	protected void OnAble() {
		ChangeButtonColor(defaultColor);
	}
	public override void PointClick() {
		ChangeButtonColor(pressingColor);
		PlaySFX(click);
		DebugScreen.debugScreen.AddWrite("PointClick: ScenaryButton");
		Action();
	}
	public override void LaserEnter(RaycastHit hit) {
		objectSelected = hit.collider.gameObject;
		ChangeButtonColor(pointingColor);
		PlaySFX(select);
		Select();
		//DebugScreen.debugScreen.Write("LaserEnter: ScenaryButton");
	}
	public override void LaserExit() {
		OnAble();
		DebugScreen.debugScreen.AddWrite("LaserExit: ScenaryButton");
		Unselect();
	}
	protected void onDisable() {
		ChangeButtonColor(disableColor);
	}
	protected void ChangeButtonColor(Color newColor) {
		objectSelected.GetComponent<MeshRenderer>().material.color = newColor;
	}
	protected void PlaySFX(AudioClip sfx) {
		GetComponent<AudioSource>().clip = sfx;
		GetComponent<AudioSource>().loop = false;
		GetComponent<AudioSource>().Play();
	}
	protected virtual void Action() {}
	protected virtual void Select() {}
	protected virtual void Unselect() {}
}
