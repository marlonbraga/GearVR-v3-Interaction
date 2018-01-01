using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class VRAvatar:MonoBehaviour {
	[SerializeField]
	private float minDistance = 1.5f;
	public Material SceneCurtain;
	public static VRAvatar _VRAvatar;
	public bool canMove = false;
	public AudioClip[] steps;
	private AudioSource audioSource;
	void Start() {
		_VRAvatar = this;
		StartCoroutine(Teleport(Vector3.zero));
		audioSource = GetComponent<AudioSource>();
	}
	public void Movement(Vector3 newTarget) {
		if(canMove)
			StartCoroutine(Teleport(newTarget));
	}
	private IEnumerator Teleport(Vector3 newTarget) {
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
}
