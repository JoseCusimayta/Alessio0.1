﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour, IPersonaje
{

    #region Variables

    #region Variables de Salud
    public Health salud;                            //Variable para obtener la clase Health
    public float vida_actual;                       //Variable para guardar la cantidad de vida actual    
    public float vida_maxima;                       //Variable para determinar la cantidad de vida máxima posible que puede tener el personaje
    public float vida_anterior;                     //Variable para guardar la cantidad de vida que tenía antes de ser golpeado
    public float alerta_vida;	                    //Variable para determinar cuando se activará la animación para alertar al jugador que su vida corre peligro
    #endregion

    #region armas
    public bool esta_atacando;                      //Variable para saber si el personaje está atacando
    public bool tiene_arma;                         //Variable para saber si el personaje tiene algún tipo de arma
    public bool tiene_pistola;                      //Variable para saber si el personaje tiene una pistola
    public string tipo_arma;                        //Variable para guardar el tipo de arma que esta usando actualmente
    public float danio_golpe;                       //Variable para determinar la cantidad de daño que tendrán los golpes del personaje
    public int contador_apoyo;                      //Variable para guardar la cantidad de poder acumulado para llamar a la habilidad especial
    public bool atacando_pistola;                   //Variable para saber si el personaje está atacando con una pistola
    public float intervalo_ataque = 0.5f;           //Variable para determinar la cantidad de tiempo de retroceso antes de poder volver a atacar    
    public float reactivacion_ataque;               //Variable para manejar la velocidad de ataque
    public Transform arma_jugador;                  //Variable para guardar la posición y rotación del arma
    public Pistola pistola;                         //Variable para guardar la clase Pistola

    #endregion

    #region movimiento
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el personaje
    public float veloidad_correr = 10;              //Variable para determinar la velocidad con la que corerrá el personaje
    public float velocidad_con_arma = 2;            //Variable para determinar la velocidad con la que se moverá el personaje cuando sostenga un arma
    public float axis_horizontal;                   //Variable para guardar la cantidad del Axis en horizontal (-1,1)
    public float axis_vertical;                     //Variable para guardar la cantidad del Axis en vertical (-1,1)
    public bool correr;                             //Variable para saber si el personaje está corriendo
    public bool caminar;                            //Variable para saber si el personaje está caminando
    #endregion

    #region colisiones
    public bool colision_suelo;                     //Variable para saber si el personaje aun se puede hacia abajo
    public bool colision_arriba;                    //Variable para saber si el personaje aun se puede hacia arriba
    public bool colision_derecha;                   //Variable para saber si el personaje aun se puede hacia derecha
    public bool colision_izquierda;                 //Variable para saber si el personaje aun se puede hacia izquierda
    #endregion

    #region Animacion
    public Animator _animator;                      //Variable
    public Sprite sprite_mano_arma;                 //Variable para guardar el sprite de la mano derecha con arma
    public SpriteRenderer sprite_brazoDerecho;      //Variable de Tipo Sprite para guardar el sprite del BrazoDerecho
    #endregion

    #region Personaje
    public Recurso[] inventario;	                //Variable de tipo "Arreglo" para almacenar los recursos del personaje
    public Rigidbody rigidbody_Jugador;             //Variable para guardar el RigidBody del personaje
    public GameObject Prefab_Bala;                  //Variable para guardar el prefab de la Bala
    public GameObject Prefab_Golpe;                 //Variable para guardar el prefab del golpe
    public GameObject Prefab_Explosion;             //Variable para guardar el prefab de la explosión
    #endregion

    #region Falta Ordenar

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
    public bool puedeControlar;
    //variables para controlar el retroceso
    public float knockback;
    private bool knockbackToRight;


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
    #endregion
    #endregion



    #region Funciones de Unity
    void Start()
    {
        puedeControlar = true;
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

    void Update()
    {
        Hurt();
        ManageBlinking();
        HandleKnockBack();
        Atacar();

        GestorVida();                               //Función para gestionar la vida del objeto y sus respectivas acciones
        GestorTeclado();                            //Función para recibir los Inputs del teclado
        GestorMouse();                              //Función para recibir los Inputs del mouse
        GestorAtaques();                            //Función para gestionar los Ataques y tipos de Ataques
        GestorAnimaciones();                        //Función para gestionar las animaciones del objeto

    }

    void FixedUpdate()
    {
        GestorMovimiento();                         //Función para manejar el movimiendo con fisica
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))
        {                                         //Verificamos que el objeto colisionado sea un arma (Tag: Arma)
            Debug.Log("Tengo un arma");
            DetectarObjetoArma(other);              //Función para detectar el arma con el que ha colisionado
        }
        DetectarObjetoHiriente(other);              //Función para detectar el objeto que le disminuirá vida
        DetectarObjetoCura(other);                  //Función para detectar el objeto que le aumentará vida
        ColissionParedes();


    }

    #endregion

    #region Funciones Update
    void GestorVida()
    {
        vida_actual = salud._vidaActual;                    //Guardamos la vida actual del objeto
        if (vida_actual <= alerta_vida)                     //Verificamos si la vida del objeto está en riesgo
        {
            if (vida_actual > 0)                            //Verificamos si el objeto aun tiene vida
            {
                Debug.Log("Alerta de vida baja");           //Disparamos la animación de alerta de vida baja
            }
            else
            {
                Debug.Log("Dead End");                      //Disparamos la animación de la muerte del personaje
                Destroy(gameObject);
            }
        }
    }
    public void GestorTeclado()
    {
        axis_horizontal = Input.GetAxis("Horizontal");  //Guardamos la variable Horizontal del Axis
        axis_vertical = Input.GetAxis("Vertical");      //Guardamos la variable Vertical del Axis

        if ((Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)) &&
            !Input.GetKey(KeyCode.LeftShift))           //Verificamos si el personaje está en movimiento
        {
            caminar = true;                             //Activamos la variable "caminar" para decirle a la animación que ejecute el "caminar"
        }
        else if (caminar)                               //Verificamos si el personaje ya no está caminando, pero la variable sigue activada
        {
            caminar = false;                            //Desactivamos la variable "caminar" para decirle a la animación que está detenido
        }

        if (Input.GetKey(KeyCode.LeftShift))            //Verificamos si el personaje esta corriendo
        {
            correr = true;                              //Activamos la variable "correr" para decirle a la animación que ejecute el "correr"
        }
        else if (correr)                                //Verificamos si el personaje ya no está corriendo, pero la variable sigue activada
        {
            correr = false;                             //Desactivamos la variable "correr" para decirle a la animación que deje de ejecutar el "correr"
        }
    }
    public void GestorMouse()
    {
        if (esta_atacando)                              //Verificamos si el personaje está atacando
        {
            reactivacion_ataque = intervalo_ataque;     //Reiniciamos el tiempo de reactivación para atacar
            esta_atacando = false;                      //Desactivamos la variable "esta_atacando", si no, se reiniciaría el tiempo en cada frame
        }
        if (reactivacion_ataque >= 0)                   //Verificamos si el tiempo de reactivación es mayor o igual a 0
        {
            reactivacion_ataque -= Time.deltaTime;      //Disinuimos el valor del tiempo de reactivación hasta que sea menor a 0
        }

        if (reactivacion_ataque <= 0)                   //Verificamos si es menor o igual a 0 para reactivar los ataques
        {
            if (Input.GetMouseButton(0))
            {
                esta_atacando = true;                   //Activamos la variable de que está atacando con el clic izquierdo
                Debug.Log("Se ha presionado el botón 0: Clic Izquierdo, esta_atacando = true");
            }
            if (Input.GetMouseButton(1))
            {
                Debug.Log("Se ha presionado el botón 1: Clic Derecho");
            }
            if (Input.GetMouseButton(2))
            {
                Debug.Log("Se ha presionado el botón 2: Clic del medio");
            }
        }
    }
    public void GestorAtaques()
    {
        if (atacando_pistola)                       //Verificamos si el personaje está atacando con una pistola
        {
            atacando_pistola = false;               //Desactivamos la variable "atacando_pistola"
        }
        if (esta_atacando && tipo_arma == "Pistola")//Verificamos si el personaje está atacando y si el tipo de arma que sostiene es una Pistola
        {
            atacando_pistola = true;                //Activamos la variable "atacando_pistola" para decirle a la animación que debe ejecutar
        }
    }
    public void GestorAnimaciones()
    {
        _animator.SetBool("Caminar", caminar);      //Asignamos el valor del estado "caminar" a la animación
        _animator.SetBool("Correr", correr);        //Asignamos el valor del estado "caminar" a la animación
        _animator.SetBool("AtacandoPistola", atacando_pistola); //Asignamos el valor de "atacando_pistola" a la animación
        _animator.SetBool("TienePistola", tiene_pistola);       //Asignamos el valor de "tiene_pistola" a la animación
        _animator.SetBool("TieneArma", tiene_arma);             //Asignamos el valor de "tiene_arma" a la animación


        if (knockback > 0)
        {
            _animator.SetBool("hurt", true);
        }
        if (knockback < 0)
        {
            _animator.SetBool("hurt", false);
        }

    }
    #endregion

    #region FixedUpdate
    public void GestorMovimiento()
    {

        Vector3 moveVector = new Vector3(0, 0, 0);                  //Creamos un Vector 3, el cual nos serirá para definir el movimiento
        if (axis_horizontal > 0)                                    //Verificamos si el personaje se mueve hacia la derecha
        {
            transform.rotation = Quaternion.identity;               //Giramos el cuerpo del personaje hacia la derecha
        }
        if (axis_horizontal < 0)                                    //Verificamos si el personaje se mueve hacia la izquierda
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);      //Giramos el cuerpo del personaje hacia la izquierda
        }
        if (correr)                                                 //Verificamos si el personaje está corriendo
        {
            moveVector.x = axis_horizontal * veloidad_correr;       //Asignamos la velocidad de movimiento al correr en el eje x
            moveVector.y = axis_vertical * veloidad_correr;         //Asignamos la velocidad de movimiento al correr en el eje y
        }
        else
        {
            moveVector.x = axis_horizontal * velocidad_normal;      //Asignamos la velocidad de movimiento al caminar en el eje x
            moveVector.y = axis_vertical * velocidad_normal;        //Asignamos la velocidad de movimiento al caminar en el eje y
        }

        rigidbody_Jugador.velocity = moveVector;                    //Le damos la velocidad al personaje con el moveVector





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
            moveVector.x = axis_horizontal * velocidad_normal;
            moveVector.y = axis_vertical * velocidad_normal;
        }


        //permite que el player se muev
        rigidbody_Jugador.velocity = moveVector;

    }
    #endregion

    #region Colisiones

    public void ColissionParedes()
    {
        //Area movida a esta sección para saber las colisiones
        

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
    }

    public void DetectarObjetoArma(Collider objeto_arma)
    {
        if (objeto_arma.name == "Pistola")              //Verificamos que tipo de arma encontramos
        {
            tipo_arma = objeto_arma.name;               //Reconocemos el tipo de arma como "Pistola" (Nombre: Pistola)
            sprite_brazoDerecho.sprite =
                sprite_mano_arma;                       //Cambiamos el brazo sin arma, por un brazo con Arma
            tiene_pistola = true;                       //Activamos la variable "tiene_pistola" para decirle a la animación que muestre las animaciones con pistola
            tiene_arma = true;                          //Activamos la variable "tiene_arma" para decirle a la animación que muestre las animaciones con arma
            Destroy(objeto_arma.gameObject);            //Destruimos el arma que está en el suelo
            
        }
    }
    public void DetectarObjetoHiriente(Collider objeto_hiriente)
    {
        if (objeto_hiriente.CompareTag("BalaEnemigo"))
        {
            salud.ModificarVida(objeto_hiriente.GetComponent<Bala>().danio_bala, objeto_hiriente.gameObject);
        }
        if (objeto_hiriente.CompareTag("Enemigo"))
        {
            salud.ModificarVida(20, objeto_hiriente.gameObject);

        }
    }
    public void DetectarObjetoCura(Collider objeto_cura)
    {

    }
    #endregion

    


    #region Funciones nuevas
    void ManageBlinking()
    {
        //si esta en estado invulnerable
        if (gameObject.layer == 12)
        {
            Color newColor = cabeza.GetComponent<SpriteRenderer>().color;
            newColor.a = Mathf.Lerp(newColor.a, targetAlpha, Time.deltaTime * 20);
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

            if (newColor.a > 0.9f)
            {
                targetAlpha = 0;
            }
            else if (newColor.a < 0.1f)
            {
                targetAlpha = 1;
            }

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

    ////esto se encarga de cuando te hacen daño
    void Hurt()
    {
        //si la vida actual es menor a la vida que teniamos antes significa que hemos recibido daño
        if (salud._vidaActual < vida_anterior)
        {
            //Layer para ser invulnerable
            gameObject.layer = 12;
            puedeControlar = false;
            knockback = 2;
            //verticalSpeed = -1;
            if (salud._ultimoAtacante != null)
            {
                if (transform.position.x < salud._ultimoAtacante.transform.position.x)
                {
                    knockbackToRight = false;
                }
                else knockbackToRight = true;
            }
            //el player se vuelve invulnerable
            Invoke("RestaurarCapa", 2);
        }
        vida_anterior = salud._vidaActual;
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



    //en el metodo para las colisiones se usara el metodo coger


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 boxSize = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Gizmos.DrawWireCube(transform.position, boxSize);
        Vector3 down = new Vector3(0, -1, 0);
        Vector3 pos = transform.position + (down * rayLength);
        Gizmos.DrawWireCube(pos, boxSize);
    }
    #endregion


    #region Funciones Alessio Interface
    //He dejado esto a un lado por cuestión de nombres, más adelante tocará cambiar el nombre a las interfaces
    //paolo: vas a cambiar los nombres de estas funciones? recuerda que son funciones comunes,la interfaz va a ser usada por todos los personajes
    public void Atacar()
    {
        // el player tiene dos tipos de ataques:
        //1. con golpes (a mano limpia o armas blancas) 
        //2. con armas de fuego (pistola, metraca, etc.)
        //por ahora solo esta la opcion de arma de fuego
        if (esta_atacando)
        {
            if (tiene_pistola)
            {
                Debug.Log("Disparando...");
                Instantiate(Prefab_Bala, arma_jugador.transform.position, arma_jugador.transform.rotation);
            }
        }
        
       
    }

    public void Saltar()
    {
    }


    public void Curar()
    {
    }

    public void Lanzar()
    {
    }

    public void Abrir()
    {
    }
    public void Correr()
    {
    }
   

    public void Morir()
    {
        Destroy(gameObject);
    }
    public void Mover()
    {
    }
    public void Coger()
    {
    }
    #endregion
}