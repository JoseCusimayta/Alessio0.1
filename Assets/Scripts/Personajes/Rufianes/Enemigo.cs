using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour {

	public bool reposo;		
	//truo  --> Reposo (El enemigo está quieto, si detecta a Alessio dispara, pero no lo persigue)
	//false --> Patrullaje (El enemigo patrulla, si detecta a Alessio dispara y pesigue hasta que salga de su rango)
	public bool alerta;
	//true  --> Persigue a Alessio y dispara hasta matarlo o morir
	//false --> Solo dispara y deja de disparar cuando Alessio sale del rango
	public float radio_Alcance;		//Perimetro de Vision: Si detecta a Alessio, dispara y/o persigue - Depende del reposo y Alerta

	public PatronMovimiento patronMovimiento;


	// Use this for initialization
	void Start () {
		patronMovimiento = GetComponent<PatronMovimiento> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
