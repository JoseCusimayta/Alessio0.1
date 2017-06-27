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
	public Recurso[] inventario;    //Arreglo que almacena los recursos de Alessio

    ////Variable para saber si está atacando
    private bool isAttack;          

    //rigid body para gestionar movimiento
    Rigidbody _rbAlessio;
    //variables para gestionar los raycast contra otro elementos del juego
    private bool isGrounded;
    private bool isTecho;
    private bool isIzq;
    private bool isDer;
    public float rayLength = 0.6f;
    //se carga el componente box collider del player
    
    //variable que permite gestionar sobre que layer del player no le afectara las colisiones
    public LayerMask _mask;

    //variable para determinar si el jugador puede controlar al personaje o no
    private bool puedeControlar;
    //variables para controlar el retroceso
    public float knockback;
    private bool knockbackToRight;

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
    public Sprite _spriteWeapon;        //Variable para guardar el sprite de la mano derecha con arma
    private bool tieneArma; //variable para determinar si tiene arma el jugador

    //sprite de las partes del player
    private GameObject cabeza;
    private GameObject cuerpo;
    private GameObject AnteBrazoD;
    private GameObject BrazoD;
    private GameObject AnteBrazoI;
    private GameObject BrazoI;
    private GameObject MusloD;
    private GameObject PiernaD;
    private GameObject PieD;
    private GameObject MusloI;
    private GameObject PiernaI;
    private GameObject PieI;
    //variable para gestionar el color del sprite cuando es herido
    private float targetAlpha;

    // Use this for initialization
    void Start()
    {
        
        puedeControlar = true;
        _rbAlessio = GetComponent<Rigidbody>();
        salud = GetComponent<Health>(); 
        _animator = GetComponent<Animator>();
        Prefab_Bala.transform.Rotate(0, 180, 0);
        cabeza = GameObject.Find("Cabeza");
        cuerpo = GameObject.Find("Cuerpo");
        AnteBrazoD = GameObject.Find("AnteBrazoD");
        BrazoD = GameObject.Find("BrazoD");
        AnteBrazoI = GameObject.Find("AnteBrazoI");
        BrazoI = GameObject.Find("BrazoI");
        MusloD = GameObject.Find("MusloD");
        PiernaD = GameObject.Find("PiernaD");
        PieD = GameObject.Find("PieD");
        MusloI = GameObject.Find("MusloI");
        PiernaI = GameObject.Find("PiernaI");
        PieI = GameObject.Find("PieI");
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
        Hurt();
        ManageBlinking();
        HandleKnockBack();
        ManageAnimation();
        ManejarGiros();

    }

    //aqui se gestiona la fisica del player
	void FixedUpdate () {

		//se crea vector 
		Vector3 moveVector=new Vector3(0,0,0);
        //el knock back es el empuje que se le hace al player cuando recibe daño
        if (knockback > 0)
        {
            if (knockbackToRight)
            {
                moveVector.x = knockback;
            }
            else moveVector.x = -knockback;

        }
        else
        {
            //se carga la informacion para saber cuanto avanzara alessio
            moveVector.x = h1 * velocidadAlterada;
            moveVector.y = h2 * velocidadAlterada;
        }

        //se cargan elementos para gestionar raycast
        Vector3 boxSize = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        boxSize *= 0.99f;
        RaycastHit raycastInfo;

        //raycast para controlar si hay colision desde la izquierda
        isIzq = Physics.BoxCast(transform.position, boxSize / 2, Vector3.left, out raycastInfo, Quaternion.identity, rayLength, _mask.value);
        if (isIzq)
        {
            Debug.Log("Colision conizq");
            //si al player le impacta una bala enemiga
            if (raycastInfo.collider.gameObject.CompareTag("BalaEnemigo"))
            {
                Debug.Log("Colision con bala enemiga");
            }
            
        }

        //permite que el player se muev
        _rbAlessio.velocity = moveVector;


	}

    public void Agarrar_Pistola() //Metodo para que Alessio pueda agarrar la pistola y cambiar de arma
    {
        //cargamos la pistola por defecto del jugador
        Debug.Log("Se guardo pistola en inventario pistola:"+pistola.ToString());
        pistola.transform.position = Arma_Player.transform.position; //La pistola adopta la posición del objeto vacío
        pistola.transform.parent = Arma_Player.transform.parent;   //La pistola y el objeto vacio comparten el mismo objeto padre = ALessio
        Debug.Log("Se guardo pistola en inventario pistola:" + pistola.ToString());
        //Tipo_Arma = pistola.getPistola(); //Cambiamos el tipo de arma
        //ataque_Alessio.setAtaque_Alessio(Prefab_Bala, Prefab_Golpe, Empty_Alessio, Tipo_Arma);  //Le damos los datos al ataque  de Alessio
    }

    public void Mover (){
        if (puedeControlar)
        {
            h1 = Input.GetAxis("Horizontal");
            h2 = Input.GetAxis("Vertical");
        }
        else
        {
            h1 = 0;
            h2 = 0;
        }
		
	}

    //gestionar las animaciones del player
    void ManageAnimation()
    {
        if (h1 == 0)
        {
            _animator.SetFloat("MoveX", Mathf.Abs(h2));
        }
        if (h2 == 0)
        {
            _animator.SetFloat("MoveX", Mathf.Abs(h1));        //asignamos el valor absoluto de h1 a la variable MoveX del animator    
        }

        if (tieneArma)
        {
            _animator.SetBool("tieneArma",true);
        }
        if (!tieneArma)
        {
            _animator.SetBool("tieneArma", false);
        }

        if (isAttack)
        {
            //Si está atacando, activamos el Trigger del ataque y apagamos el isAttack
            _animator.SetTrigger("Attack");
            isAttack = false;
        }

        if (knockback > 0)
        {
            _animator.SetBool("hurt", true);
        }
        if (knockback < 0)
        {
            _animator.SetBool("hurt", false);
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
                Debug.Log("Evaluando si tengo pistola="+ tieneArma);
                if (tieneArma)
                {
                    Debug.Log("Tengo pistola y disparo la bala");
                    isAttack = true;
                    Instantiate(Prefab_Bala, Arma_Player.transform.position, Arma_Player.transform.rotation); //si la pistola no es nula, se crea sus balas
                    
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
    void Hurt()
    {
        //si la vida actual es menor a la vida que teniamos antes significa que hemos recibido daño
        if (salud.healht < previousHealth)
        {
            //Layer para ser invulnerable
            gameObject.layer = 12;
            puedeControlar = false;
            knockback = 2;
            //verticalSpeed = -1;
            if (salud.lastAttacker != null)
            {
                if (transform.position.x < salud.lastAttacker.transform.position.x)
                {
                    knockbackToRight = false;
                }
                else knockbackToRight = true;
            }
            //el player se vuelve invulnerable
            Invoke("RestaurarCapa", 2);
        }
        previousHealth = salud.healht;
    }

    //se restaura al layer player
    void RestaurarCapa()
    {
        gameObject.layer = 8;
    }

    void HandleKnockBack()
    {
        if (knockback > 0)
        {

            knockback -= Time.deltaTime * 5.5f;
            if (knockback <= 0)
            {
                puedeControlar = true;
            }
        }
    }

    void ManageBlinking()
    {

        //si esta en estado invulnerable
        if (gameObject.layer == 12)
        {
            Color newColor = cabeza.GetComponent<SpriteRenderer>().color;
            newColor.a = Mathf.Lerp(newColor.a, targetAlpha, Time.deltaTime * 20);
            Debug.Log("newColor.a=" + newColor.a);
            if (newColor.a > 0.9f)
            {
                targetAlpha = 0;
            }
            else if (newColor.a < 0.1f)
            {
                targetAlpha = 1;
            }
            cabeza.GetComponent<SpriteRenderer>().color = newColor;
            cuerpo.GetComponent<SpriteRenderer>().color = newColor;
            AnteBrazoD.GetComponent<SpriteRenderer>().color = newColor;
            BrazoD.GetComponent<SpriteRenderer>().color = newColor;
            AnteBrazoI.GetComponent<SpriteRenderer>().color = newColor;
            BrazoI.GetComponent<SpriteRenderer>().color = newColor;
            MusloD.GetComponent<SpriteRenderer>().color = newColor;
            PiernaD.GetComponent<SpriteRenderer>().color = newColor;
            PieD.GetComponent<SpriteRenderer>().color = newColor;
            MusloI.GetComponent<SpriteRenderer>().color = newColor;
            PiernaI.GetComponent<SpriteRenderer>().color = newColor;
            PieI.GetComponent<SpriteRenderer>().color = newColor;

        }
        if (gameObject.layer != 12)
        {
            Color newColor = cabeza.GetComponent<SpriteRenderer>().color;
            newColor.a = 1;
            cabeza.GetComponent<SpriteRenderer>().color = newColor;
            cuerpo.GetComponent<SpriteRenderer>().color = newColor;
            AnteBrazoD.GetComponent<SpriteRenderer>().color = newColor;
            BrazoD.GetComponent<SpriteRenderer>().color = newColor;
            AnteBrazoI.GetComponent<SpriteRenderer>().color = newColor;
            BrazoI.GetComponent<SpriteRenderer>().color = newColor;
            MusloD.GetComponent<SpriteRenderer>().color = newColor;
            PiernaD.GetComponent<SpriteRenderer>().color = newColor;
            PieD.GetComponent<SpriteRenderer>().color = newColor;
            MusloI.GetComponent<SpriteRenderer>().color = newColor;
            PiernaI.GetComponent<SpriteRenderer>().color = newColor;
            PieI.GetComponent<SpriteRenderer>().color = newColor;
        }

    }

    //en el metodo para las colisiones se usara el metodo coger
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pistola")
        {
            Debug.Log("Tengo una pistola");
            tieneArma = true;
            _spriteBrazoDerecho.sprite = _spriteWeapon;         //Signamos _spriteWeapon como sprite para el brazo derecho
            Destroy(other.gameObject);
           
            //Agarrar_Pistola();
            //Coger();
        }
        //uso del metodo coger
      
        if (other.CompareTag("BalaEnemigo"))
        {
            //al impactarnos la bala
            salud.ChangeHealth(other.GetComponent<Bala>().danio_bala,other.gameObject);
            Hurt();
            
        }

        if (other.CompareTag("Enemigo"))
        {
            salud.ChangeHealth(20, other.gameObject);

        }
    }

    void OnDrawGizmos()
    {
       
            Gizmos.color = Color.green;


        Vector3 boxSize = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Gizmos.DrawWireCube(transform.position, boxSize);

        Vector3 down = new Vector3(0, -1, 0);
        Vector3 pos = transform.position + (down * rayLength);
        Gizmos.DrawWireCube(pos, boxSize);
    }
}
