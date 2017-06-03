using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Personaje:MonoBehaviour, IPersonaje {

	public float vida;			//La vida actual
	public string tipo_arma;	//Indica que arma se esta usando actualmente
	public float alerta_vida;	//Avisa al jugador que Alessio tiene poca vida
	public float velocidad;		//Velocidad normal de Alessio
	public float danio_golpe;	//Cantidad de daño que realizan los golpes de Alessio a los enemigos

	void Start(){
	}
	void Update(){
	}
	void FixedUpdate(){
	}

	public void Mover (){
	}

	public void Atacar(){
	}

	public void Morir(){
	}

	public void Coger(){
	}

	public void Saltar(){
	}

	public void Correr(){
	}

	public void Curar(){
	}

	public void Lanzar(){
	}

	public void Abrir(){
	}
}
