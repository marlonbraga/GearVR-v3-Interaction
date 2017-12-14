using System.Collections;
using System;
using UnityEngine;

public class Laser : MonoBehaviour {
	public Color corLaser = Color.red;
	public int DistanciaDoLaser = 5;
	public float LarguraInicial = 0.02f, LarguraFinal = 0.1f;
	private GameObject luzColisao;
	private Vector3 posicLuz;
	public bool ligado = true;
	void Start() {
		luzColisao = new GameObject();
		luzColisao.AddComponent<Light>();
		luzColisao.GetComponent<Light>().intensity = 8;
		luzColisao.GetComponent<Light>().bounceIntensity = 8;
		luzColisao.GetComponent<Light>().range = LarguraFinal * 2;
		luzColisao.GetComponent<Light>().color = corLaser;
		//
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.startColor = corLaser;
		lineRenderer.endColor = corLaser;
		lineRenderer.startWidth = LarguraInicial;
		lineRenderer.endWidth = LarguraFinal;
		lineRenderer.positionCount=2;
	}
	//Instantiate(bulletTexture[Random.Range(0,3)], hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
	void LateUpdate(){
		if (ligado == true) {
			luzColisao.SetActive(true);
			Vector3 PontoFinalDoLaser = transform.position + transform.forward * DistanciaDoLaser;
			RaycastHit PontoDeColisao;
			Vector3 wandLight;
			if (Physics.Raycast(transform.position, transform.forward, out PontoDeColisao, DistanciaDoLaser)) {
				GetComponent<LineRenderer>().SetPosition(1, PontoDeColisao.point);
				wandLight = (PontoDeColisao.point - transform.position);
				//luzColisao.transform.position = (PontoDeColisao.point - wandLight / (Vector3.Distance(PontoDeColisao.point, transform.position)* 350));
				luzColisao.transform.position = PontoDeColisao.point + PontoDeColisao.normal/50;
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
}
