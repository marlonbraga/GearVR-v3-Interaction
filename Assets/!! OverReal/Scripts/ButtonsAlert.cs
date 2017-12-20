using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAlert:MonoBehaviour {
	public GameObject backButton;
	public GameObject homeButton;
	public GameObject centerButton;
	public GameObject discButton;
	public GameObject triggerButton;
	//public GameObject chassisButton;

	private Material m_backButton;
	private Material m_homeButton;
	private Material m_centerButton;
	private Material m_discButton;
	private Material m_triggerButton;
	//private Material m_chassisButton;

	private Color buttonColor;
	private Color buttonColorPress;
	private Color buttonColorOver;
	void Start() {
		m_backButton = backButton.GetComponent<MeshRenderer>().material;
		m_homeButton = homeButton.GetComponent<MeshRenderer>().material;
		m_centerButton = centerButton.GetComponent<MeshRenderer>().material;
		m_discButton = discButton.GetComponent<MeshRenderer>().material;
		m_triggerButton = triggerButton.GetComponent<MeshRenderer>().material;

		buttonColor = m_discButton.color;
		buttonColorPress = transform.parent.GetComponent<Laser>().GetLaserColor();
		buttonColorOver = new Color(0.1f, 0.1f, 0.1f, 0.8f);
	}

	void Update() {
		if(OVRInput.Get(OVRInput.Button.Any) || OVRInput.Get(OVRInput.Touch.Any)) {
			buttonColorPress = transform.parent.GetComponent<Laser>().GetLaserColor();
			buttonColorPress = new Color(
				buttonColorPress.r,
				buttonColorPress.g,
				buttonColorPress.b,
				1.0f);
			buttonColorOver = new Color(
				buttonColorPress.r,
				buttonColorPress.g,
				buttonColorPress.b,
				buttonColorPress.a - 0.3f);
			Debug.Log("ButtonsAlert/buttonColorPress: " + buttonColorPress + "| GetLaserColor(): " + transform.parent.GetComponent<Laser>().GetLaserColor());
		}
		if(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
			m_triggerButton.color = buttonColorPress;
		else
			m_triggerButton.color = buttonColor;
		if(OVRInput.Get(OVRInput.Touch.PrimaryTouchpad) || Input.GetKey(KeyCode.Alpha1))
			m_discButton.color = buttonColorOver;
		else {
			m_discButton.color = buttonColor;
		}
		if(OVRInput.Get(OVRInput.Button.One) || Input.GetKey(KeyCode.Alpha2)) /*&& OVRInput.Get(OVRInput.Button.Start) && OVRInput.Get(OVRInput.Button.PrimaryTouchpad)*/
			m_discButton.color = buttonColorPress;
		if(OVRInput.Get(OVRInput.Button.Two) && OVRInput.Get(OVRInput.Button.Back))
			m_backButton.color = buttonColorPress;
		else
			m_backButton.color = buttonColor;

		//if(OVRInput.Get(OVRInput.Button.DpadRight))
		//	m_homeButton.color = buttonColorOver;
		//else
		//	m_homeButton.color = buttonColor;
		//if(OVRInput.Get(OVRInput.Button.DpadLeft))
		//	m_centerButton.color = buttonColorPress;
		//else
		//	m_centerButton.color = buttonColor;
	}
}
