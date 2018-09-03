using UnityEngine;
using System.Collections;
using System;

public class Gun :Equipament {
	public GameObject Bullet;
	public GameObject BulletEmitter;
	public float Bullet_Forward_Force = 1f;
	public float FireRate = 0.1f;
	private bool FireAble = true;
	public float precisionValue = 0.1f;
	public bool automatic;
	public GameObject muzzleFlash;
	public GameObject muzzleLight;
	//private FirstPersonController player;
	private Camera camera;
	private Animator animator;
	private float aimDistance;
	private float f;
	private int CountBullets;
	// private GameObject Temporary_Bullet_Handler;

	void Start() {
		FireAble = true;
		muzzleFlash.SetActive(false);
		muzzleLight.SetActive(false);
		//player = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		animator = GetComponent<Animator>();
		f = gameObject.transform.localPosition.x;
		CountBullets = 20; //transform.FindChild("MunitionScreen").FindChild("NumBalas").GetComponent<TextMesh>().text;
    }
	public void TweenedSomeValue(float val) {
		aimDistance = val;
	}
	void Update() {
		if (automatic) {
			if ((Input.GetButton("Fire1")) && (FireAble == true)) {
				atirar();
			}
		} else {
			if ((Input.GetButton("Fire1"))) {
				atirar();
			}
		}
		if (Input.GetButtonDown("Aim")) {
			
			Debug.Log(gameObject.transform.localPosition);
		}
		if (Input.GetButtonUp("Aim")) {
			float f = gameObject.transform.localPosition.x;
			transform.localPosition = new Vector3(0, transform.localPosition.y, transform.localPosition.z);
			Debug.Log(gameObject.transform.localPosition);
		}

		if ((Input.GetButton("Aim"))) {
			//player.m_MouseLook.YSensitivity = 2;
			//player.m_MouseLook.XSensitivity = 2;
			if (camera.fieldOfView > 45) {
				camera.fieldOfView -= 1 * Time.deltaTime * 80;
			}

			if (transform.localPosition.z > -0.18f) {
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.05f * Time.deltaTime * 50);
			}
			if (transform.localPosition.z < -0.18f) {
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -0.18f);
			}
			//animator.SetBool("Aim", true);
		} else {
			//player.m_MouseLook.YSensitivity = 5;
			//player.m_MouseLook.XSensitivity = 5;
			if (camera.fieldOfView < 60) {
				camera.fieldOfView += 1 * Time.deltaTime * 80;
			}

			if (transform.localPosition.z > 0f) {
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 0.05f * Time.deltaTime * 50);
			} else if (transform.localPosition.z < 0f) {
				transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0f);
			}

		}
	}
	void atirar() {
		if (CountBullets>0) {
			
			GetComponent<AudioSource>().Play();

			FireAble = false;
			StartCoroutine(chronometer(FireRate));
			StartCoroutine(chronometer2(0.01f));

			//GetComponent<Animator>().Play("Recoil");
			//GetComponent<Animator>().SetBool("fire", true);

			CountBullets--;
			transform.Find("MunitionScreen").Find("NumBalas").GetComponent<TextMesh>().text = "" + CountBullets;

			GameObject Temporary_Bullet_Handler;
			Temporary_Bullet_Handler = Instantiate(Bullet, BulletEmitter.transform.position, BulletEmitter.transform.rotation) as GameObject;

			Rigidbody Temporary_RigidBody;
			Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();

			Vector3 precisionFire = new Vector3(UnityEngine.Random.Range(-precisionValue, precisionValue), UnityEngine.Random.Range(-precisionValue, precisionValue), 0f);
			Temporary_RigidBody.AddForce((-BulletEmitter.transform.right + precisionFire) * Bullet_Forward_Force);

			Destroy(Temporary_Bullet_Handler, 3.0f);
		}
	}

	public IEnumerator chronometer2(float seconds) {
		muzzleFlash.SetActive(true);
		muzzleLight.SetActive(true);
		yield return new WaitForSeconds(seconds);
		muzzleFlash.SetActive(false);
		muzzleLight.SetActive(false);
	}
	public IEnumerator chronometer(float seconds) {
		yield return new WaitForSeconds(seconds);
		FireAble = true;
		//GetComponent<Animator>().SetBool("fire", false);
	}
	void AddBullets(int bullets) {
		CountBullets += bullets;
		transform.Find("MunitionScreen").Find("NumBalas").GetComponent<TextMesh>().text = "" + CountBullets;
	}
	//public override void action(int value, int sender) {
	//	AddBullets(value);
 //   }
}
