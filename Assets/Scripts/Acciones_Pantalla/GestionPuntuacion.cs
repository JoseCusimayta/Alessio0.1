using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GestionPuntuacion : MonoBehaviour {
    private Text textoPuntuacion; //El puntaje del usuario (texto a presentar en la pantalla)
    public int numeroPuntuacion; //El puntaje del usuario (requerido para el calculo interno en el juego)
    public RectTransform botonCombo;
    private bool comboActivado;
    private int contadorBarraCombo; //variable para rellenar barra de combo
    private int numeroPuntuacionAux; //variable auxiliar, hecha para apoyar en la logica de activacion de combo
    // Use this for initialization
    void Start () {
        numeroPuntuacion = 0;
        textoPuntuacion = GetComponent<Text>();
        //ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
        botonCombo.localPosition = Vector3.one * 999;
        comboActivado = true;
    }
	
	// Update is called once per frame
	void Update () {
        textoPuntuacion.text = "Puntaje: " + numeroPuntuacion;
           if(numeroPuntuacion> numeroPuntuacionAux)
            {
			Debug.Log ("numeroPuntuacion="+numeroPuntuacion);
			Debug.Log ("numeroPuntuacionAux="+numeroPuntuacionAux);
                if (numeroPuntuacion % 100 == 0 && numeroPuntuacion > 0)
                {
                    contadorBarraCombo = numeroPuntuacion;
                    Debug.Log("Ya cumpli 100 puntos, es hora de un extra");
                    botonCombo.localPosition = Vector3.zero;
                    comboActivado = true;

                }
            
        }
        numeroPuntuacionAux = numeroPuntuacion;//se guarda la ultima puntuacion en la variable auxiliar    
        validarCombo();

       
    }

    private void validarCombo()
    {
        if (Input.GetKeyUp(KeyCode.E) && comboActivado)
        {
            Debug.Log("aCTIVE COMBO");
            botonCombo.localPosition = Vector3.one * 999;
            contadorBarraCombo = 0;
            comboActivado = false;
        }
        

    }
}
