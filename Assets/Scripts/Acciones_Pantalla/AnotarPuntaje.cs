using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotarPuntaje : MonoBehaviour {
    public int score = 100;     //puntaje por defecto
    private GameObject Puntaje; //Variable para cargar el objeto que se encarga de gestionar los puntos
    // Use this for initialization
    void Start () {
        Puntaje =GameObject.Find("Puntaje");
        Debug.Log("Puntaje="+ Puntaje);
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Health>()._vidaActual <= 0)
        {
            Debug.Log("murio enemigo");
            Puntaje.GetComponent<GestionPuntuacion>().numeroPuntuacion += score;
            this.enabled = false;
        }
    }
}
