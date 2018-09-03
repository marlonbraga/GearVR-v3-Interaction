using UnityEngine;

[RequireComponent(typeof(AudioSource))]
abstract public class InteractiveObject:MonoBehaviour {

	public AudioClip select;
	public AudioClip click;

	virtual public void LaserEnter(RaycastHit hit) { }
	virtual public void LaserExit() { }
	virtual public void PointClick(Vector3 target) { }
	virtual public void PointClick() { }
	virtual public void PointPress() { }
	virtual public void PointDepress() { }

	void Awake() {
		if(gameObject.tag != "InteractiveObject") {
			//Debug.LogError(transform.name + " has a '" + transform.tag + "' tag. InteractiveObject class needs a 'InteractiveObject' tag");
		}
	}
}
