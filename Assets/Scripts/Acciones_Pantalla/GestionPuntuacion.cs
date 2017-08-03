using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GestionPuntuacion : MonoBehaviour {
    private Text textoPuntuacion; //El puntaje del usuario (texto a presentar en la pantalla)
    public int numeroPuntuacion; //El puntaje del usuario (requerido para el calculo interno en el juego)
    public RectTransform botonCombo;
    private bool comboActivado;
    // Use this for initialization
    void Start () {
        numeroPuntuacion = 0;
        textoPuntuacion = GetComponent<Text>();
        //ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
        botonCombo.localPosition = Vector3.one * 999;
    }
	
	// Update is called once per frame
	void Update () {
        textoPuntuacion.text = "Puntaje: " + numeroPuntuacion;
        if (numeroPuntuacion % 100 == 0 && numeroPuntuacion>0 && !comboActivado)
        {
            Debug.Log("Ya cumpli 100 puntos, es hora de un extra");
            botonCombo.localPosition = Vector3.zero;
            comboActivado = true;
            validarCombo();
            
        }
        else comboActivado = false;

    }

    private void validarCombo()
    {
        if (Input.GetKeyUp(KeyCode.E) && comboActivado)
        {
            Debug.Log("aCTIVE COMBO");
            botonCombo.localPosition = Vector3.one * 999;
            
        }
        

    }
}
