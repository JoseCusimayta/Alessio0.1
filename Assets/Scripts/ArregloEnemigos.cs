using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArregloEnemigos : MonoBehaviour {
	public GameObject[] enemigos;
	public GameObject portal;
	bool desactivarMuros;
	public GameObject pantallaFinal;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void muereEnemigo() {
		for (int i = 0; i < enemigos.Length; i++) {
			if (enemigos[i]==null) {
				desactivarMuros = true;
				break;
			}
		}
	}

	public void desactivar_Muros() {
		if (desactivarMuros) {
			pantallaFinal.SetActive (true);
		}
	}

}
