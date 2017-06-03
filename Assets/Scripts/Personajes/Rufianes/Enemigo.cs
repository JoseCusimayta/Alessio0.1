using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour,IPersonaje {

	public float vida;			//La vida actual
	public string tipo_arma;	//Indica que arma se esta usando actualmente
	public float alerta_vida;	//Avisa al jugador que Alessio tiene poca vida
	public float velocidad=5;		//Velocidad normal de Alessio
	public float velocidadAlterada=5;		//Velocidad alterada por algun otro tipo de Alessio
	public float danio_golpe;	//Cantidad de daño que realizan los golpes de Alessio a los enemigos

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
	void OnTriggerEnter(Collider other)
    {
        Debug.Log("Se detecto algo");
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
