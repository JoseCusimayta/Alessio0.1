using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour, IPersonaje  {

	
    private Health salud;
    private float previousHealth;
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
    private bool isAttack;          //Variable para saber si está atacando

    //rigid body para gestionar movimiento
    Rigidbody _rbAlessio;

	//variable para el movimiento horizontal
	private float h1;
	//variable para el movimiento vertical
	private float h2;

    //GameObject para contener un arma
    public Transform Arma_Player;
    public Pistola pistola;

    //gameobjects de la bala y su explosion al impactar
    public GameObject Prefab_Bala, Prefab_Golpe, Prefab_Explosion;
    //para controlar la cadencia del disparo
    private float Intervalo_Ataque = 0;

    public Animator _animator;
    public SpriteRenderer _spriteBrazoDerecho;      //Variable de Tipo Sprite para guardar el sprite del BrazoDerecho
    public SpriteRenderer _spritePlayer;            //Sprite del Jugador

    // Use this for initialization
    void Start()
    {
        _rbAlessio = GetComponent<Rigidbody>();
        salud = GetComponent<Health>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        //destruir al enemigo
        float saludActual = salud.healht;
        //Debug.Log("saludActual:" + saludActual);
        if (saludActual <= 0)
        {
            Morir();
        }
        Mover ();
		Correr ();
        Atacar();
        //ManageAnimation();
        ManejarGiros();

    }

	void FixedUpdate () {

		//se crea vector 
		Vector3 moveVector=new Vector3(0,0,0);
		//se carga la informacion para saber cuanto avanzara alessio
		moveVector.x = h1 * velocidadAlterada;
		moveVector.y = h2 * velocidadAlterada;
		_rbAlessio.velocity = moveVector;


	}

    public void Agarrar_Pistola() //Metodo para que Alessio pueda agarrar la pistola y cambiar de arma
    {
        pistola.transform.position = Arma_Player.position; //La pistola adopta la posición del objeto vacío
        pistola.transform.parent = Arma_Player.parent;   //La pistola y el objeto vacio comparten el mismo objeto padre = ALessio
        //Tipo_Arma = pistola.getPistola(); //Cambiamos el tipo de arma
        //ataque_Alessio.setAtaque_Alessio(Prefab_Bala, Prefab_Golpe, Empty_Alessio, Tipo_Arma);  //Le damos los datos al ataque  de Alessio
    }

    public void Mover (){

		h1=Input.GetAxis("Horizontal");
		h2=Input.GetAxis("Vertical");
	}

    //gestionar las animaciones del player
    void ManageAnimation()
    {
        _animator.SetFloat("MoveX", Mathf.Abs(h1));        //asignamos el valor absoluto de h1 a la variable MoveX del animator    
        if (isAttack)
        {
            //Si está atacando, activamos el Trigger del ataque y apagamos el isAttack
            _animator.SetTrigger("Attack");
            isAttack = false;
        }

    }

    //Para manejar la ida de izq a der del personaje
    void ManejarGiros()
    {
        if (h1 > 0)
        {
            transform.rotation = Quaternion.identity;
        }
        if (h1 < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void Atacar(){
        if (Input.GetMouseButtonDown(0)) //Al hacer clic derecho...
        {
            //if (pistola != null)
            //{
            //    Instantiate(Prefab_Bala, Arma_Player.position, Arma_Player.rotation); //si la pistola no es nula, se crea sus balas
            //}
            Intervalo_Ataque -= Time.deltaTime;
            if (Intervalo_Ataque <= 0)
            {
                if (pistola != null)
                {
                    Instantiate(Prefab_Bala, Arma_Player.position, Arma_Player.rotation); //si la pistola no es nula, se crea sus balas
                }
                //if (pistola.tipoArma == "Pistola") //Se obtiene el tipo de Arma...
                //{
                //    Instantiate(Prefab_Bala, Empty_Alessio.position, Empty_Alessio.rotation); //y se crea el objeto dependiendo del arma, en este caso, una bala
                //}
                //else if (Tipo_Arma == "Golpear")
                //{
                //    Instantiate(Prefab_Golpe, Empty_Alessio.position, Empty_Alessio.rotation, Empty_Alessio.parent);
                //}
                Intervalo_Ataque = 0.02f;
            }

        }
    }

	public void Morir(){
        Destroy(gameObject);
	}

	public void Coger(){
        if (pistola != null)
        {
            Agarrar_Pistola();
        }
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

    ////esto se encarga de cuando te hacen daño
    //void Hurt()
    //{
    //    //si la vida actual es menor a la vidaque teniamos antes significa que hemos recibido daño
    //    if (salud.healht < previousHealth)
    //    {

    //        salud.healht -= 10;
    //        //Layer para ser invulnerable
    //        //gameObject.layer = 10;
    //        //canControl = false;
    //        //knockback = 2;
    //        //verticalSpeed = -1;
    //        //if (transform.position.x < salud.lastAttacker.transform.position.x)
    //        //{
    //        //    knockbackToRight = false;
    //        //}
    //        //else knockbackToRight = true;

    //        //Invoke("restaurarCapa", 2);
    //    }
    //    previousHealth = salud.healht;
    //}

    //en el metodo para las colisiones se usara el metodo coger
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistola")
        {
            Debug.Log("Tengo una pistola");
            pistola = other.gameObject.GetComponent<Pistola>();

            //Agarrar_Pistola();
            Coger();
        }
        //uso del metodo coger
        
        //if (other.tag == "Suelo")
        //{
        //    rigiBody = GetComponent<Rigidbody>();
        //    rigiBody.velocity = new Vector3(0, 0, 0);
        //    rigiBody.useGravity = false;

        //}
        if (other.CompareTag("BalaEnemigo"))
        {
            salud.ChangeHealth(other.GetComponent<Bala>().danio_bala,other.gameObject);
            
        }

        if (other.CompareTag("Enemigo"))
        {
            salud.ChangeHealth(20, other.gameObject);

        }
    }
}
