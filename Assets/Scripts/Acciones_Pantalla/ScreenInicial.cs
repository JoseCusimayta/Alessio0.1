using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenInicial : MonoBehaviour {
	bool activa;
	public RectTransform panelInicio;
    public float velocidadTraslado = 10f;
	// Use this for initialization
	void Start () {
		//ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
		//panelInicio.localPosition = Vector3.one * 999;
		panelInicio.localPosition = Vector3.zero;
		Invoke ("irSalida", 2);
	}

	void irSalida(){
        //ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
        //for(int i = 1; i <= 100; i++)
        //{
        //    panelInicio.transform.Translate(Vector3.right*Time.deltaTime*i);
        //}
        if (panelInicio.transform.position.x < 999)
        {
            panelInicio.GetComponent<Rigidbody2D>().velocity = Vector3.right * velocidadTraslado;
        }
        
        //panelInicio.localPosition = Vector3.one * 999;
        activa =true;


	}

	// Update is called once per frame
	void Update () {
//		if (activa) {
//			transform.Translate (Vector3.right);
//		}
	}

}
