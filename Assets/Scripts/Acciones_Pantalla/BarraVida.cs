using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour {
    private Health jugador_salud;      //variable que representa al componente de salud del player
    private float nivelActualBarra;    //barra de vida 
    private Image barraVida;
    // Use this for initialization
    void Start () {
        jugador_salud=GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        barraVida = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {
        modificarBarra(jugador_salud._vidaMaxima, jugador_salud._vidaActual);
        //Debug.Log("Nivel de barra actual=" + nivelActualBarra);
    }

    //permite representar la variacion de la vida del jugador en pantalla, manipulando la imagen de la barra de vida
    void modificarBarra(float saludMaxima, float salud)
    {
        nivelActualBarra = salud / saludMaxima;
        //Debug.Log ("Nivel de barra actual=" + nivelActualBarra);
        barraVida.fillAmount = nivelActualBarra;
    }
}
