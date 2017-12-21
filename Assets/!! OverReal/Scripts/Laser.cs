using System.Collections;
using System;
using UnityEngine;

public class Laser : MonoBehaviour {
	public static Laser _Laser;
	[SerializeField]
	private Color corLaser;
	public float DistanciaDoLaser = 5;
	public float LarguraInicial = 0.02f, LarguraFinal = 0.1f;
	public bool ligado = true;
	private Color defaultColor;
	private GameObject luzColisao;
	private Vector3 posicLuz;
	private LineRenderer lineRenderer;
	private Light colisionLight;
	void Start() {
		_Laser = this;

		defaultColor = corLaser;

		luzColisao = new GameObject();
		luzColisao.AddComponent<Light>();
		colisionLight = luzColisao.GetComponent<Light>();
		colisionLight.bounceIntensity = 8;
		colisionLight.range = LarguraFinal * 2;
		colisionLight.intensity = 8;

		lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startWidth = LarguraInicial;
		lineRenderer.endWidth = LarguraFinal;
		lineRenderer.positionCount=2;

		ChangeLaserColor(corLaser);
	}
	void LateUpdate(){
		DrawLaser();
	}

	public void ChangeLaserColor(Color color) {
		corLaser = color;
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;
		colisionLight.color = color;
	}
	void DrawLaser() {
		if(ligado == true) {
			luzColisao.SetActive(true);
			Vector3 PontoFinalDoLaser = transform.position + transform.forward * DistanciaDoLaser;
			RaycastHit PontoDeColisao;
			Vector3 wandLight;
			if(Physics.Raycast(transform.position, transform.forward, out PontoDeColisao, DistanciaDoLaser)) {
				GetComponent<LineRenderer>().SetPosition(1, PontoDeColisao.point);
				wandLight = (PontoDeColisao.point - transform.position);
				luzColisao.transform.position = PontoDeColisao.point + PontoDeColisao.normal / 50;
			} else {
				GetComponent<LineRenderer>().SetPosition(1, PontoFinalDoLaser);
				wandLight = (PontoFinalDoLaser - transform.position);
				luzColisao.transform.position = (PontoFinalDoLaser - wandLight / 1);
			}
		} else {
			GetComponent<LineRenderer>().SetPosition(1, transform.position);
			luzColisao.SetActive(false);
		}
		GetComponent<LineRenderer>().SetPosition(0, transform.position);
	}
	public void ResetColor() {
		ChangeLaserColor(defaultColor);
	}
	public Color GetLaserColor() {
		return corLaser;
	}
}
