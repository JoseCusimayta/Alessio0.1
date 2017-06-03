using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour, IPersonaje  {

	public float vida;			//La vida actual
	public string tipo_arma;	//Indica que arma se esta usando actualmente
	public float alerta_vida;	//Avisa al jugador que Alessio tiene poca vida
	public float velocidad=5;		//Velocidad normal de Alessio
	public float velocidadAlterada=5;		//Velocidad alterada por algun otro tipo de Alessio
	public float danio_golpe;	//Cantidad de daño que realizan los golpes de Alessio a los enemigos

	public int contadorApoyo;		//Es una barra que al llegar a 100 da la posibilidad de llamar apoyo del gobierno y aparecen rodeando a Alessio
	public float vidaMaxima;		//Cantidad de vida máxima posible que puede tener el personaje
	public Recurso[] inventario;	//Arreglo que almacena los recursos de Alessio

	//variables para detectar colision en el suelo, en el lado derecho, izquiero y arriba
	private bool isSuelo;
	private bool isTecho;
	private bool isDer;
	private bool isIzq;

	//rigid body para gestionar movimiento
	Rigidbody _rbAlessio;

	//variable para el movimiento horizontal
	private float h1;
	//variable para el movimiento vertical
	private float h2;



	// Use this for initialization
	void Start () {
		_rbAlessio = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Mover ();
		Correr ();
	}

	void FixedUpdate () {

		//se crea vector 
		Vector3 moveVector=new Vector3(0,0,0);
		//se carga la informacion para saber cuanto avanzara alessio
		moveVector.x = h1 * velocidadAlterada;
		moveVector.y = h2 * velocidadAlterada;
		_rbAlessio.velocity = moveVector;


	}

	public void Mover (){

		h1=Input.GetAxis("Horizontal");
		h2=Input.GetAxis("Vertical");
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
		if(Input.GetKey(KeyCode.LeftShift)){
			velocidadAlterada = velocidad * 2;
		}
		else{
			velocidadAlterada = velocidad;
		}
	}

	public void Curar(){
	}

	public void Lanzar(){
	}

	public void Abrir(){
	}
	
        
}
