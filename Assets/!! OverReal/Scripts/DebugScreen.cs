using TMPro;
using UnityEngine;

public class DebugScreen : MonoBehaviour {

	private TextMeshProUGUI screen;
	public static DebugScreen debugScreen;
	void Awake () {
		debugScreen = GetComponent<DebugScreen>();
		screen = GetComponent<TextMeshProUGUI>();
		Write("###### OVERLAB is RUNNING! ###### \n\n ");
	}
	public void Write(string text) {
		screen.text = text;
	}
	public void AddWrite(string text) {
		screen.text += text+"\n";
	}
}
