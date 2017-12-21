using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class VRAvatar:MonoBehaviour {

	public TextMesh textmash;

	[SerializeField]
	private float minDistance = 1.5f;
	private NavMeshAgent navMeshAgent;
	private Color overColor = new Color(0.4f, 0.4f, 0.0f, 0.5f);
	private Color pressColor = new Color(0.5f, 0.2f, 0.5f, 0.8f);
	public static VRAvatar _VRAvatar;
	private Coroutine coroutine;

	void Start() {
		navMeshAgent = GetComponent<NavMeshAgent>();
		_VRAvatar = this;
	}

	public void Movement(Vector3 newTarget) {
		if(coroutine!=null)
			StopCoroutine(coroutine);
		coroutine = StartCoroutine(Teleport(newTarget));
		
	}
	private IEnumerator Walk(Vector3 newTarget) {
		while(Vector3.Distance(transform.position, newTarget) >= minDistance) {
			navMeshAgent.destination = newTarget;
			yield return new WaitForSeconds(0.01f);
			textmash.text = Vector3.Distance(transform.position, newTarget).ToString();
		}
	}
	private IEnumerator Teleport(Vector3 newTarget) {
		transform.position = newTarget;
		yield return new WaitForSeconds(0.01f);
		textmash.text = Vector3.Distance(transform.position, newTarget).ToString();
	}
}
