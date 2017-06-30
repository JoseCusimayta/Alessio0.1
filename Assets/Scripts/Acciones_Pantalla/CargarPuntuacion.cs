using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargarPuntuacion : MonoBehaviour {
    private Text _text;
   
    void Start () {
        _text = GetComponentInChildren<Text>();

        int score = PlayerPrefs.GetInt("puntajeJugador", 0);
        _text.text = "Puntaje: " + score;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
