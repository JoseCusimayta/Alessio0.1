using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GestionPuntuacion : MonoBehaviour {
    private Text textoPuntuacion; //El puntaje del usuario (texto a presentar en la pantalla)
    public int numeroPuntuacion; //El puntaje del usuario (requerido para el calculo interno en el juego)
    // Use this for initialization
    void Start () {
        numeroPuntuacion = 0;
        textoPuntuacion = GetComponent<Text>();

    }
	
	// Update is called once per frame
	void Update () {
        textoPuntuacion.text = "Puntaje: " + numeroPuntuacion;
    }
}
