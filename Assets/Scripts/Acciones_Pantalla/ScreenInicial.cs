using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInicial : MonoBehaviour {
	bool activa;
	public RectTransform panelInicio;
	// Use this for initialization
	void Start () {
		//ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
		//panelInicio.localPosition = Vector3.one * 999;
		panelInicio.localPosition = Vector3.zero;
		Invoke ("irSalida", 2);
	}

	void irSalida(){
		//ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
		panelInicio.localPosition = Vector3.one * 999;
		activa=true;


	}

	// Update is called once per frame
	void Update () {
//		if (activa) {
//			transform.Translate (Vector3.right);
//		}
	}

}
