using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
public class Gun:Equipament {
	[SerializeField] private GameObject Bullet;
	[SerializeField] private GameObject BulletEmitter;
	[SerializeField] private AudioClip ShootSound;
	[SerializeField] private AudioClip TriggerSound;
	[SerializeField] private float Bullet_Forward_Force = 1f;
	[SerializeField] private float FireRate = 0.1f;
	[SerializeField] private float precisionValue = 0.1f;
	[HideInInspector] public bool FireAble = true;
	[SerializeField] private bool automatic;
	[SerializeField] private GameObject muzzleFlash;
	[SerializeField] private GameObject muzzleLight;
	[SerializeField] private GameObject trigger;
	float x = 0;
	float y = 0;
	float z = 0;
	void Start() {
		FireAble = true;
		muzzleFlash.SetActive(false);
		muzzleLight.SetActive(false);
		x = trigger.transform.localEulerAngles.x;
		y = trigger.transform.localEulerAngles.y;
		z = trigger.transform.localEulerAngles.z;
	}
	private void OnEnable() {
		StartCoroutine(chronometer(1.5F));
		muzzleFlash.SetActive(false);
		muzzleLight.SetActive(false);
		muzzleFlash.GetComponent<ParticleSystem>().Stop();
	}
	void Update() {
		if(automatic) {
			if(GameConfiguration._VRInput.TriggerButton() && (FireAble == true)) {
				Fire();
			}
		} else {
			if(GameConfiguration._VRInput.TriggerButtonDown()) {
				Fire();
			}
		}
		if(GameConfiguration._VRInput.TriggerButton() || Input.anyKey)
			trigger.transform.localEulerAngles = new Vector3(x, y, z + 20);
		else
			trigger.transform.localEulerAngles = new Vector3(x, y, z);
	}
	[ContextMenu("FIRE!")]
	public void Fire() {
		DebugScreen.debugScreen.AddWrite("Gun: <color=red>Fire()</color>");
		if(VRAvatar._VRAvatar.inventary.bullets > 0) {
			FireAble = false;
			StartCoroutine(chronometer(FireRate));
			StartCoroutine(chronometer2(0.3f));
			GetComponent<AudioSource>().clip = ShootSound;
			GetComponent<AudioSource>().Play();
			VRAvatar._VRAvatar.inventary.bullets--;
			DebugScreen.debugScreen.AddWrite("Bullets: <#aa3333>" + VRAvatar._VRAvatar.inventary.bullets + "</color>");

			GameObject Temporary_Bullet_Handler;
			Temporary_Bullet_Handler = Instantiate(Bullet, BulletEmitter.transform.position, BulletEmitter.transform.rotation) as GameObject;

			//Rigidbody Temporary_RigidBody;
			//Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

			//Vector3 precisionFire = new Vector3(UnityEngine.Random.Range(-precisionValue, precisionValue), UnityEngine.Random.Range(-precisionValue, precisionValue), 0f);
			//Temporary_RigidBody.AddForce((-BulletEmitter.transform.right + precisionFire) * Bullet_Forward_Force);

			Destroy(Temporary_Bullet_Handler, 3.0f);
		} else {
			GetComponent<AudioSource>().clip = TriggerSound;
			GetComponent<AudioSource>().Play();
		}
	}
	public IEnumerator chronometer2(float seconds) {
		muzzleFlash.GetComponent<ParticleSystem>().Play();
		muzzleFlash.SetActive(true);
		muzzleLight.SetActive(true);
		yield return new WaitForSeconds(seconds);
		muzzleFlash.SetActive(false);
		muzzleLight.SetActive(false);
		muzzleFlash.GetComponent<ParticleSystem>().Stop();
	}
	public IEnumerator chronometer(float seconds) {
		yield return new WaitForSeconds(seconds);
		FireAble = true;
	}
}
