using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour, IPersonaje
{

    #region Variables

    #region Variables de Salud
    private Health salud;                            //Variable para obtener la clase Health
    private float vida_actual;                       //Variable para guardar la cantidad de vida actual    
    private float vida_maxima;                       //Variable para determinar la cantidad de vida máxima posible que puede tener el personaje
    private float vida_anterior;                     //Variable para guardar la cantidad de vida que tenía antes de ser golpeado
    public float alerta_vida;	                    //Variable para determinar cuando se activará la animación para alertar al jugador que su vida corre peligro
    #endregion

    #region Variables de Ataques
    private bool esta_atacando;                      //Variable para saber si el personaje está atacando
    private bool tiene_arma;                         //Variable para saber si el personaje tiene algún tipo de arma
    private string tipo_arma;                        //Variable para guardar el tipo de arma que esta usando actualmente    
    private int contador_apoyo;                      //Variable para guardar la cantidad de poder acumulado para llamar a la habilidad especial
    public float intervalo_ataque = 0.5f;           //Variable para determinar la cantidad de tiempo de retroceso antes de poder volver a atacar    
    public float reactivacion_ataque;               //Variable para manejar la velocidad de ataque
    private GameObject arma_jugador;                  //Variable para guardar la posición y rotación del arma
    public Transform punto_disparo;                 //Variable para guardar la posición y rotación de punto del disparo
    #region Puños
    private bool atacando_golpes;                     //Variable para saber si el personaje está atacando con puños
    public float danio_golpe;                       //Variable para determinar la cantidad de daño que tendrán los golpes del personaje
    private GameObject prefab_golpe;                 //Variable para guardar el prefab del efecto del golpe
    
    #endregion
    #region Pistola
    public Sprite sprite_pistola;                      //Variable para guardar el sprite de la pistola
    private bool tiene_pistola;                      //Variable para saber si el personaje tiene una pistola
    private bool atacando_pistola;                   //Variable para saber si el personaje está atacando con una pistola    
    private Pistola pistola;                         //Variable para guardar la clase Pistola
    public Sprite mano_pistola;                     //Variable para guardar el sprite de la mano con la pistola
    public GameObject prefab_bala_pistola;          //Variable para guardar el prefab de la Bala de la pistola
    #endregion
    #region Metralleta
    public Sprite sprite_metralleta;                      //Variable para guardar el sprite de la metralleta
    private bool tiene_metralleta;                   //Variable para saber si el personaje tiene una metralleta
    private bool atacando_metralleta;                //Variable para saber si el personaje está atacando con una metralleta
    private Metralleta metralleta;                   //Variable para guardar la clase Metralleta
    public Sprite mano_metralleta;                  //Variable para guardar el sprite de la mano con la metralleta
    public GameObject prefab_bala_metralleta;       //Variable para guardar el prefab de la Bala de la metralleta
    #endregion
    #endregion

    #region Variables de Movimiento
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el personaje
    public float veloidad_correr = 10;              //Variable para determinar la velocidad con la que corerrá el personaje
    public float velocidad_con_arma = 2;            //Variable para determinar la velocidad con la que se moverá el personaje cuando sostenga un arma
    public float velocidad_retrocediendo = 1;        //Variable para determinar la velocidad con la que retrocederá el personaje
    private float axis_horizontal;                   //Variable para guardar la cantidad del Axis en horizontal (-1,1)
    private float axis_vertical;                     //Variable para guardar la cantidad del Axis en vertical (-1,1)
    private bool correr;                             //Variable para saber si el personaje está corriendo
    private bool caminar;                            //Variable para saber si el personaje está caminando
    private bool fijar_camara;                       //Variable para fijar la mirada del personaje
    #endregion

    #region Variables de Colisiones
    private bool colision_abajo;                     //Variable para saber si el personaje aun se puede hacia abajo
    private bool colision_arriba;                    //Variable para saber si el personaje aun se puede hacia arriba
    private bool colision_derecha;                   //Variable para saber si el personaje aun se puede hacia derecha
    private bool colision_izquierda;                 //Variable para saber si el personaje aun se puede hacia izquierda
    public float distancia_colision = 0.6f;         //Variable para detectar la distancia entre el personaje y el objeto solido a colisionar
    public Vector3 caja_colision;                   //Variable para determinar la caja de colisión del personaje
    RaycastHit rayCast_informacion;                 //Variable para guardar la información del objeto con el que colisiona
    public LayerMask mascara_objeto;                //Variable para guardar y modificar la máscara del Personaje
    #endregion

    #region Variables de Retroceso (KnockBack)
    public float retroceso;                         //Variable para guardar la velocidad con la que retrocederá el personaje (KnockBack)
    private bool retroceder_derecha;                 //Variable para saber si el personaje va a retroceder a la derecha
    private bool retrocediendo;                      //Variable para saber si el personaje está retrocediendo, usado para la animación
    #endregion

    #region Variables de Animacion
    private Animator _animator;                      //Variable para guardar el Animator del personaje
    public Sprite[] sprite_mano_arma;                 //Arreglo que para guardar los sprites de la mano derecha con arma
    private float transparencia_objetivo;            //Variable para determinar el nivel de transparencia (Alpha)
    #endregion

    #region Variables de Personaje
    private Recurso[] inventario;	                //Variable de tipo "Arreglo" para almacenar los recursos del personaje
    private Rigidbody rigidbody_Jugador;             //Variable para guardar el RigidBody del personaje
    private GameObject Prefab_Explosion;             //Variable para guardar el prefab de la explosión
    public bool puede_controlar = true;             //Variable para determinar si el jugador puede controlar al personaje
    private int armaActualEnMano=-1;                   //variable que representa el arma actual que posee alessio en su marno, sacada del inventario rapido   
    #region Partes del Cuerpo
    private GameObject cabeza;                       //Variable para guardar el GameObject de la cabeza del jugador
    private GameObject cuerpo;                       //Variable para guardar el GameObject del cuerpo del jugador
    private GameObject AnteBrazoD;                   //Variable para guardar el GameObject del Ante Brazo Derecho del jugador
    private GameObject BrazoD;                       //Variable para guardar el GameObject del Brazo Derecho del jugador
    public GameObject BrazoDerechoGirable;          //Variable para guardar el GameObject del Brazo Derecho Variable del jugador
    private GameObject AnteBrazoI;                   //Variable para guardar el GameObject del Ante Brazo Izquierdo del jugador
    private GameObject BrazoI;                       //Variable para guardar el GameObject del Brazo Izquierdo del jugador
    private GameObject MusloD;                       //Variable para guardar el GameObject del Muslo Derecho del jugador
    private GameObject PiernaD;                      //Variable para guardar el GameObject de la Pierna Derecha del jugador
    private GameObject PieD;                         //Variable para guardar el GameObject del Pie Derecho del jugador
    private GameObject MusloI;                       //Variable para guardar el GameObject del Muslo Izquierdo del jugador
    private GameObject PiernaI;                      //Variable para guardar el GameObject de la Pierna Izquierdo del jugador
    private GameObject PieI;                         //Variable para guardar el GameObject del Pie Izquierdo del jugador
    #endregion
    #endregion


    #endregion

    #region Funciones de Unity
    void Start()
    {
        retroceso = 0;                                  //Iniciamos la variable "retroceso con 0", porque al inicio tiene un número positivo y eso genera errores en la animación del inicio
        danio_golpe = 5;                                //le instanciamos un valor de golpe
        #region Declarando Variables
        _animator = GameObject.Find("Alessio").GetComponent<Animator>();
        salud = GameObject.Find("Alessio").GetComponent<Health>();
        rigidbody_Jugador = GameObject.Find("Alessio").GetComponent<Rigidbody>();
        #endregion
        #region Asignando el cuerpo del Personaje
        cuerpo = GameObject.Find("Alessio/Cuerpo");
        cabeza = GameObject.Find("Alessio/Cuerpo/Cabeza");        
        AnteBrazoD = GameObject.Find("Alessio/Cuerpo/AnteBrazoD");        
        BrazoD = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD");
        AnteBrazoI = GameObject.Find("Alessio/Cuerpo/AnteBrazoI");
        BrazoI = GameObject.Find("Alessio/Cuerpo/AnteBrazoI/BrazoI");
        MusloD = GameObject.Find("Alessio/Cuerpo/cuerpoB/MusloD");
        PiernaD = GameObject.Find("Alessio/Cuerpo/cuerpoB/MusloD/PiernaD");
        PieD = GameObject.Find("Alessio/Cuerpo/cuerpoB/MusloD/PiernaD/PieD");
        MusloI = GameObject.Find("Alessio/Cuerpo/cuerpoB/MusloI");
        PiernaI = GameObject.Find("Alessio/Cuerpo/cuerpoB/MusloI/PiernaI");
        PieI = GameObject.Find("Alessio/Cuerpo/cuerpoB/MusloI/PiernaI/PieI");
        #endregion
    }

    void Update()
    {
        axis_horizontal = Input.GetAxis("Horizontal");          //Guardamos la variable Horizontal del Axis
        axis_vertical = Input.GetAxis("Vertical");              //Guardamos la variable Vertical del Axis
        GestorAnimaciones(axis_vertical, axis_horizontal);      //Función para gestionar las animaciones del objeto
        JugadorHerido();                                        //Función para gestionar todos los cambios que se implementan en las caracteristicas del jugador cuando es herido por un elemento del juego
        GestorVida();                                           //Función para gestionar la vida del objeto y sus respectivas acciones
        GestorTeclado();                                        //Función para recibir los Inputs del teclado
        GestorMouse();                                          //Función para recibir los Inputs del mouse
        GestorAtaques();                                        //Función para gestionar los Ataques y tipos de Ataques
        GestorRetroceso();                                      //Función para gestionar el retroceso del jugador
        GestorParpadeo();                                       //Función para gestionar el parpadeo del personaje
        seleccionarItem();                                      //Función que permite el intercambio los items que se encuentran en el inventario rapido
       
    }

    void FixedUpdate()
    {
        GestorMovimiento();                             //Función para manejar el movimiendo con fisica
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))                   //Verificamos que el objeto colisionado sea un arma (Tag: Arma)            
        {                                               
            DetectarObjetoArma(other);                  //Función para detectar el arma con el que ha colisionado
        }
        if (other.CompareTag("ObjetoHiriente"))         //Verificamos que el objeto colisionado sea un item (Tag: ObjetoHiriente)
        {
            DetectarObjetoHiriente(other);              //Función para detectar el objeto que le disminuirá vida
        }
        if (other.CompareTag("Cura"))                   //Verificamos que el objeto colisionado sea un item (Tag: Cura)
        {
            Debug.Log("Accedi a cura");
            DetectarObjetoCura(other);                  //Función para detectar el objeto que le aumentará vida
        }
        if (other.CompareTag("Municion"))                   //Verificamos que el objeto colisionado sea un item (Tag: Cura)
        {
            Debug.Log("Accedi a municion");
            DetectarMuniciones(other);                 //Función para detectar el objeto que le aumentará vida
        }
        ColissionParedes();
    }
    #endregion

    #region Funciones del Update
    void GestorVida()
    {
        vida_actual = salud._vidaActual;                        //Guardamos la vida actual del objeto
        if (vida_actual <= alerta_vida)                         //Verificamos si la vida del objeto está en riesgo
        {
            if (vida_actual > 0)                                //Verificamos si el objeto aun tiene vida
            {
                Debug.Log("Alerta de vida baja");               //Disparamos la animación de alerta de vida baja
            }
            else
            {
                Debug.Log("Dead End");                          //Disparamos la animación de la muerte del personaje
                Destroy(gameObject);
            }
        }
    }

    public void GestorTeclado()
    {
        if (puede_controlar)
        {
            axis_horizontal = Input.GetAxis("Horizontal");      //Guardamos la variable Horizontal del Axis
            axis_vertical = Input.GetAxis("Vertical");          //Guardamos la variable Vertical del Axis

            if ((Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D)) &&
                !Input.GetKey(KeyCode.LeftShift))               //Verificamos si el personaje está en movimiento
            {
                caminar = true;                                 //Activamos la variable "caminar" para decirle a la animación que ejecute el "caminar"
            }
            else if (caminar)                                   //Verificamos si el personaje ya no está caminando, pero la variable sigue activada
            {
                caminar = false;                                //Desactivamos la variable "caminar" para decirle a la animación que está detenido
            }
            if((Input.GetKey(KeyCode.W) ||
                Input.GetKey(KeyCode.A) ||
                Input.GetKey(KeyCode.S) ||
                Input.GetKey(KeyCode.D)) &&
                Input.GetKey(KeyCode.LeftShift))                //Verificamos si el personaje esta corriendo            
            {
                correr = true;                                  //Activamos la variable "correr" para decirle a la animación que ejecute el "correr"
            }
            else if (correr)                                    //Verificamos si el personaje ya no está corriendo, pero la variable sigue activada
            {
                correr = false;                                 //Desactivamos la variable "correr" para decirle a la animación que deje de ejecutar el "correr"
            }
            if (Input.GetKey(KeyCode.Q))
                fijar_camara = true;
            else
                fijar_camara = false;

           
        }
    }

   

    public void GestorMouse()
    {        
        if (esta_atacando)                                      //Verificamos si el personaje está atacando
        {
            reactivacion_ataque = intervalo_ataque;             //Reiniciamos el tiempo de reactivación para atacar
            esta_atacando = false;                              //Desactivamos la variable "esta_atacando", si no, se reiniciaría el tiempo en cada frame
            atacando_pistola = false;
            atacando_golpes = false;

        }
        if (puede_controlar)
        {
            if (reactivacion_ataque >= 0)                       //Verificamos si el tiempo de reactivación es mayor o igual a 0
            {
                reactivacion_ataque -= Time.deltaTime;          //Disinuimos el valor del tiempo de reactivación hasta que sea menor a 0               
            }

            if (reactivacion_ataque <= 0)                       //Verificamos si es menor o igual a 0 para reactivar los ataques
            {
                if (Input.GetMouseButton(0))                    //Verificamos que si hay presión sobre el clic izquierdo
                {
                    esta_atacando = true;                       //Activamos la variable de que está atacando con el clic Izquierdo                    
                }
                if (Input.GetMouseButton(1))                    //Verificamos que si hay presión sobre el clic Derecho
                {
                    
                }
                if (Input.GetMouseButton(2))                    //Verificamos que si hay presión sobre el clic del Centro
                {
                    
                }
            }
        }
    }

    public void GestorAtaques()
    {
        if (esta_atacando)                                      //Verificamos si el personaje está atacando
        {
            if (!tiene_arma)                                    //Verificamos si el personaje no tiene armas
            {
                atacando_golpes = true;                          //Activamos la variable "atacando_golpes" para decirle a la animación que debe ejecutar 
                //Debug.Log("Se activa golpe");
                _animator.SetTrigger("golpear");
               
                //Instantiate(prefab_golpe,
                //    punto_disparo.transform.position,
                //    punto_disparo.transform.rotation);          //Creamos el efecto del golpe con las mismas caracteristicas del GameObject vacío "Arma_Player" del personaje
            }
            Debug.Log("Tipo de arma ahora: " + tipo_arma+ " y tiene_pistola=" + tiene_pistola);
            if (tiene_pistola && tipo_arma=="Pistola")                                  //Verificamos si el personaje tiene pistola
            {

                if (armaActualEnMano > -1)
                {
                    Debug.Log("Disparando con Tipo de arma ahora: " + tipo_arma + " y tiene_pistola=" + tiene_pistola);
                    //verificar cuantas balas quedan
                    int balasRestantes = GetComponent<GestionInventario>().items[armaActualEnMano].objeto.GetComponent<Pistola>().cantidadBalas;
                    if (balasRestantes > 0)//mientras la municion sea mayor que cero, se puede disparar
                    {
                        Debug.Log("Disparando con la pistola");
                        atacando_pistola = true;                        //Activamos la variable "atacando_pistola" para decirle a la animación que debe ejecutar
                        Debug.Log(punto_disparo.name);
                        Instantiate(prefab_bala_pistola,
                        punto_disparo.transform.position,
                        punto_disparo.transform.rotation);          //Creamos la bala de la pistola con las mismas caracteristicas del GameObject vacío "Arma_Player" del personaje


                        Debug.Log("Se reduce municion: " + balasRestantes);
                        GetComponent<GestionInventario>().restarMunicion(armaActualEnMano, balasRestantes - 1);
                    }
                    
                }
                
            }
            if (tiene_metralleta && tipo_arma == "Ametralladora")
            {
                if (armaActualEnMano > -1)
                {
                    int balasRestantes = GetComponent<GestionInventario>().items[armaActualEnMano].objeto.GetComponent<Pistola>().cantidadBalas;
                    if (balasRestantes > 0) //mientras la municion sea mayor que cero, se puede disparar
                    {
                        Debug.Log("Disparando con la metraca");
                        atacando_metralleta = true;                     //Activamos la variable "atacando_pistola" para decirle a la animación que debe ejecutar
                        Instantiate(prefab_bala_metralleta,
                        punto_disparo.transform.position,
                        punto_disparo.transform.rotation);          //Creamos la bala de la metralleta con las mismas caracteristicas del GameObject vacío "Arma_Player" del personaje


                        Debug.Log("Se reduce municion: " + balasRestantes);
                        GetComponent<GestionInventario>().restarMunicion(armaActualEnMano, balasRestantes - 1);
                    }
                       
                }
               
            }
        }
    }

    public void GestorAnimaciones(float axis_vertical, float axis_horizontal)
    {
        /*_animator.SetBool("Caminar", caminar);                          //Asignamos el valor del estado "caminar" a la animación
        _animator.SetBool("Correr", correr);                            //Asignamos el valor del estado "caminar" a la animación
        _animator.SetBool("Golpeando", atacando_golpes);                //Asignamos el valor de "atacando_golpes" a la animación
        _animator.SetBool("AtacandoPistola", atacando_pistola);         //Asignamos el valor de "atacando_pistola" a la animación
        _animator.SetBool("TienePistola", tiene_pistola);               //Asignamos el valor de "tiene_pistola" a la animación
        _animator.SetBool("TieneArma", tiene_arma);                     //Asignamos el valor de "tiene_arma" a la animación                          
        _animator.SetBool("EsHerido", retrocediendo);                   //Asignamos el valor de "EsHerido" a la animación
        */

        float end;                                                      //Variable para determinar si el limite es 0, 1 o 2  
        if (axis_vertical != 0 || axis_horizontal != 0)                 //Detectamos el movimiento
        {
            if (Input.GetKey(KeyCode.LeftShift))                        //Detectamos si está corriendo
                end = 2;                                                //Asignamos el valor de correr a la variable
            else
                end = 1;                                                //Asignamos el valor de caminar a la variable
        }
        else
        {
            end = 0;                                                    //Asignamos el valor de espera a la variable
        }
        float start = _animator.GetFloat("Velocidad");                  //Obtenemos el valor de velocidad para modificarlo
        float result = Mathf.Lerp(start, end, Time.deltaTime * 5);      //Modificamos el valor inicial gradualmente hasta llegar al valor final
        _animator.SetFloat("Velocidad", result);                        //Asignamos el valor de "Velocidad" para definir si está corriendo o caminando o espera
        _animator.SetBool("Pistola", tiene_pistola);               //Asignamos el valor de "tiene_pistola" a la animación
        _animator.SetBool("Metralleta", tiene_metralleta);               //Asignamos el valor de "tiene_pistola" a la animación
    }

    void GestorParpadeo()
    {
        if (gameObject.layer == 12)                                         //Verificamos en que capa está (Layer 12: Capa de invulnerabilidad)
        {
            Color newColor = cabeza.GetComponent<SpriteRenderer>().color;   //Creamos una variable newColor para manejar la transparencia (alpha)
            newColor.a = Mathf.Lerp(newColor.a,
                transparencia_objetivo, Time.deltaTime * 20);               //Cambiamos el valor de la transparencia con el tiempo
            cabeza.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            cuerpo.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            AnteBrazoD.GetComponent<SpriteRenderer>().color = newColor;     //Asignamos la nueva transparencia al objeto
            BrazoD.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            AnteBrazoI.GetComponent<SpriteRenderer>().color = newColor;     //Asignamos la nueva transparencia al objeto
            BrazoI.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            MusloD.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            PiernaD.GetComponent<SpriteRenderer>().color = newColor;        //Asignamos la nueva transparencia al objeto
            PieD.GetComponent<SpriteRenderer>().color = newColor;           //Asignamos la nueva transparencia al objeto
            MusloI.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            PiernaI.GetComponent<SpriteRenderer>().color = newColor;        //Asignamos la nueva transparencia al objeto
            PieI.GetComponent<SpriteRenderer>().color = newColor;           //Asignamos la nueva transparencia al objeto

            if (newColor.a > 0.9f)                                          //Revisamos el valor de la transparencia
            {
                transparencia_objetivo = 0;                                 //Le decimos que se haga invisible
            }
            else if (newColor.a < 0.1f)                                     //Revisamos el valor de la transparencia
            {
                transparencia_objetivo = 1;                                 //Le decimos que se haga visible
            }

        }
        if (gameObject.layer != 12)                                         //Verificamos en que capa está (Si no está  invulnerable)
        {
            Color newColor = cabeza.GetComponent<SpriteRenderer>().color;   //Creamos una variable newColor para manejar la transparencia (alpha)
            newColor.a = 1;                                                 //Le decimos que se haga visible
            cabeza.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            cuerpo.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            AnteBrazoD.GetComponent<SpriteRenderer>().color = newColor;     //Asignamos la nueva transparencia al objeto
            BrazoD.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            AnteBrazoI.GetComponent<SpriteRenderer>().color = newColor;     //Asignamos la nueva transparencia al objeto
            BrazoI.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            MusloD.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            PiernaD.GetComponent<SpriteRenderer>().color = newColor;        //Asignamos la nueva transparencia al objeto
            PieD.GetComponent<SpriteRenderer>().color = newColor;           //Asignamos la nueva transparencia al objeto
            MusloI.GetComponent<SpriteRenderer>().color = newColor;         //Asignamos la nueva transparencia al objeto
            PiernaI.GetComponent<SpriteRenderer>().color = newColor;        //Asignamos la nueva transparencia al objeto
            PieI.GetComponent<SpriteRenderer>().color = newColor;           //Asignamos la nueva transparencia al objeto
        }
    }
    
    void JugadorHerido()
    {
        if (vida_actual < vida_anterior)                                    //Verificamos que haya un cambio en la vida
        {
            gameObject.layer = 12;                                          //Activamos la capa de invulnerabilidad
            puede_controlar = false;                                        //Desactivmos el control del jugador
            retrocediendo = true;                                           //Activamos la variable "retrocediendo" para la animación
            retroceso = 2;                                                  //Le damos una velocidad al retroceso
            if (salud._ultimoAtacante != null)                              //Verificamos que haya un atacante
            {
                if (transform.position.x <
                    salud._ultimoAtacante.transform.position.x)             //Verificamos la posición del atacante y del personaje
                {
                    retroceder_derecha = false;                             //Le decimos de dónde nos está atacando para saber de donde retroceder
                }
                else retroceder_derecha = true;                             //Le decimos de dónde nos está atacando para saber de donde retroceder
            }
            Invoke("RestaurarCapa", 0.5f);                                  //Reactivamos la capa del jugador en 0.5 segundos
        }
        vida_anterior = vida_actual;                                        //Actualizamos el dato de la vida anterior
    }

    void RestaurarCapa()
    {
        gameObject.layer = 8;                                               //Activamos la capa "Jugador" al personaje
    }

    void GestorRetroceso()
    {
        if (retroceso > 0)                                                  //Verificamos que exista una velocidad de retroceso
        {
            retroceso -= Time.deltaTime * 5.5f;                             //Reducimos la velocidad con el tiempo
            if (retroceso <= 0)                                             //Verificamos que la velocidad sea 0 o negativa
            {
                reactivacion_ataque = intervalo_ataque;                     //Reactivamos el ataque en un intervalo dado
                puede_controlar = true;                                     //Reactivamos el control al personaje
                retrocediendo = false;                                      //Le decimos a la animación que ya no está retrocediendo
            }
        }
    }

    
    public void seleccionarItem()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))    //Recibimos la información de los números del teclado
        {
            if (Input.GetKeyDown(vKey))                                     //Verificamos si se está presionado una tecla
            {
                switch (vKey.ToString())                                    //Obtenemos el nombre de la tecla que se está presionando
                {
                    //POR AHORA TIENEN UNA VALIDACION IF PREVIA con el sprite.name, de alli continuo con esta logica para el intercambio de armas
                    case "Alpha1":
                        Debug.Log("Seleccione item 1:");
                        //Debug.Log("Nombre:" + GetComponent<GestionInventario>().items[0].objeto);
                        if (GetComponent<GestionInventario>().items[0] != null)
                        {
                            Debug.Log("Nombre:" + GetComponent<GestionInventario>().items[0].objeto.name);
                            if (GetComponent<GestionInventario>().items[0].objeto.name == "Curacion")
                            {
                                Sanacion sanaAux = GetComponent<GestionInventario>().items[0].objeto.GetComponent<Sanacion>();
                                salud.ModificarVida(sanaAux.vidaExtra, GetComponent<GestionInventario>().items[0].objeto);
                                Debug.Log("Ha sido curado");
                                GetComponent<GestionInventario>().desasignarItemACasilla(0);
                            }
                            if (GetComponent<GestionInventario>().items[0].objeto.name == "Pistola")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                Debug.Log("Nombre del sprite: " + GetComponent<GestionInventario>().items[0].accion.name);
                                arma_jugador.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                tipo_arma = "Pistola";
                                tiene_pistola = true;
                                armaActualEnMano = 0;
                                Debug.Log("Arma en Mano: Pistola");
                            }
                            if (GetComponent<GestionInventario>().items[0].objeto.name == "Ametralladora")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                Debug.Log("Nombre del sprite: " + GetComponent<GestionInventario>().items[0].accion.name);
                                arma_jugador.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                tipo_arma = "Ametralladora";
                                tiene_metralleta = true;
                                armaActualEnMano = 0;
                            }

                            if (GetComponent<GestionInventario>().items[0].objeto.name == "MunicionAmetralladora")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                GetComponent<GestionInventario>().recargarMunicion(GetComponent<GestionInventario>().items[0].objeto.gameObject, GetComponent<GestionInventario>().items[0].accion);
                               
                            }

                        }
                        
                    break;
                    case "Alpha2":
                        Debug.Log("Seleccione item 2:");
                        if (GetComponent<GestionInventario>().items[1] != null)
                        {
                            Debug.Log("Nombre:" + GetComponent<GestionInventario>().items[1].objeto.name);
                            if (GetComponent<GestionInventario>().items[1].objeto.name == "Curacion")
                            {
                                Sanacion sanaAux = GetComponent<GestionInventario>().items[1].objeto.GetComponent<Sanacion>();
                                salud.ModificarVida(sanaAux.vidaExtra, GetComponent<GestionInventario>().items[1].objeto);
                                Debug.Log("Ha sido curado");
                                GetComponent<GestionInventario>().desasignarItemACasilla(1);
                            }
                            if (GetComponent<GestionInventario>().items[1].objeto.name == "Pistola")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                Debug.Log("Nombre del sprite: " + GetComponent<GestionInventario>().items[1].accion.name);
                                arma_jugador.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[1].accion;
                                tipo_arma = "Pistola";
                                tiene_pistola = true;
                                armaActualEnMano = 1;
                            }
                            if (GetComponent<GestionInventario>().items[1].objeto.name == "Ametralladora")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                Debug.Log("Nombre del sprite: " + GetComponent<GestionInventario>().items[1].accion.name);
                                arma_jugador.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[1].accion;
                                tipo_arma = "Ametralladora";
                                tiene_metralleta = true;
                                armaActualEnMano = 1;
                            }
                            if (GetComponent<GestionInventario>().items[1].objeto.name == "MunicionAmetralladora")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                GetComponent<GestionInventario>().recargarMunicion(GetComponent<GestionInventario>().items[1].objeto.gameObject, GetComponent<GestionInventario>().items[1].accion);

                            }
                        }
                    break;
                    case "Alpha3":
                        Debug.Log("Seleccione item 3:");
                        if (GetComponent<GestionInventario>().items[2] != null)
                        {
                            if (GetComponent<GestionInventario>().items[2].objeto.name == "Curacion")
                            {
                                Sanacion sanaAux = GetComponent<GestionInventario>().items[2].objeto.GetComponent<Sanacion>();
                                salud.ModificarVida(sanaAux.vidaExtra, GetComponent<GestionInventario>().items[2].objeto);
                                Debug.Log("Ha sido curado");
                                GetComponent<GestionInventario>().desasignarItemACasilla(2);
                            }
                            if (GetComponent<GestionInventario>().items[2].objeto.name == "Pistola")
                            {
                                BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[2].accion;
                                tipo_arma = "Pistola";
                                tiene_pistola = true;
                                armaActualEnMano = 2;
                            }
                            if (GetComponent<GestionInventario>().items[2].objeto.name == "Ametralladora")
                            {
                                BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[2].accion;
                                tipo_arma = "Ametralladora";
                                tiene_metralleta = true;
                                armaActualEnMano = 2;
                            }

                            if (GetComponent<GestionInventario>().items[2].objeto.name == "MunicionAmetralladora")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                GetComponent<GestionInventario>().recargarMunicion(GetComponent<GestionInventario>().items[2].objeto.gameObject, GetComponent<GestionInventario>().items[2].accion);

                            }
                        }
                    break;

                    case "Alpha4":
                        Debug.Log("Seleccione item 4:");
                        if (GetComponent<GestionInventario>().items[3] != null)
                        {
                            if (GetComponent<GestionInventario>().items[3].objeto.name == "Curacion")
                            {
                                Sanacion sanaAux = GetComponent<GestionInventario>().items[3].objeto.GetComponent<Sanacion>();
                                salud.ModificarVida(sanaAux.vidaExtra, GetComponent<GestionInventario>().items[3].objeto);
                                Debug.Log("Ha sido curado");
                                GetComponent<GestionInventario>().desasignarItemACasilla(3);
                            }
                            if (GetComponent<GestionInventario>().items[3].objeto.name == "Pistola")
                            {
                                BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[3].accion;
                                tipo_arma = "Pistola";
                                tiene_pistola = true;
                                armaActualEnMano = 3;
                            }
                            if (GetComponent<GestionInventario>().items[3].objeto.name == "Ametralladora")
                            {
                                BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[3].accion;
                                tipo_arma = "Ametralladora";
                                tiene_metralleta = true;
                                armaActualEnMano = 3;
                            }
                            if (GetComponent<GestionInventario>().items[3].objeto.name == "MunicionAmetralladora")
                            {
                                //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;
                                //arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
                                GetComponent<GestionInventario>().recargarMunicion(GetComponent<GestionInventario>().items[3].objeto.gameObject, GetComponent<GestionInventario>().items[3].accion);

                            }
                        }
                   break;
                }

            }
        }
    }
    #endregion

    #region Funciones del FixedUpdate
    public void GestorMovimiento()
    {
        Vector3 moveVector = new Vector3(0, 0, 0);                          //Creamos un Vector 3, el cual nos serirá para definir el movimiento
        float velocidad_movimiento = 0;                                     //Asignamos una variable para la velocidad de movimiento
        if (!fijar_camara)                                                  //Verificamos que no esté retrocediendo para aplicar los giros
        {
            if (axis_horizontal > 0)                                        //Verificamos si el personaje se mueve hacia la derecha
            {
                transform.rotation = Quaternion.identity;                   //Giramos el cuerpo del personaje hacia la derecha
            }
            if (axis_horizontal < 0)                                        //Verificamos si el personaje se mueve hacia la izquierda
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);          //Giramos el cuerpo del personaje hacia la izquierda
            }
        }
        if (retroceso > 0)
        {                                                                   //Verificamos que exista un retroceso mayor a 0                        
            if (retroceder_derecha)
            {                                                               //Verificamos si el personaje retrocerá hacia la derecha
                moveVector.x = retroceso;                                   //Hacemos retroceder al personaje hacia la derecha
            }
            else
            {
                moveVector.x = -retroceso;                                  //Hacemos retroceder al personaje hacia la derecha
            }
        }
        else
        {
            velocidad_movimiento = velocidad_normal;                        //Asignamos la velocidad para caminar

            if (correr)                                                     //Verificamos si el personaje está corriendo
                velocidad_movimiento = veloidad_correr;                     //Asignamos la velocidad para correr

            if (fijar_camara)                                               //Verificamos si el personaje está retrocediendo
                velocidad_movimiento = velocidad_retrocediendo;             //Asignamos la velocidad para retroceder

            moveVector.x = axis_horizontal * velocidad_movimiento;          //Definioms el vector de movimiento en el eje X
            moveVector.y = axis_vertical * velocidad_movimiento;            //Definioms el vector de movimiento en el eje y
        }
        rigidbody_Jugador.velocity = moveVector;                            //Le damos la velocidad al personaje con el moveVector
    }
    #endregion

    #region Funciones del onTrigger (Colisiones)

    public void ColissionParedes()
    {
        caja_colision = transform.localScale*0.99f;                             //Variable en forma de una caja para detectar las colisiones del personaje
        colision_izquierda =                                                    //Le asignamos un valor
            Physics.BoxCast(
                transform.position, 
                caja_colision / 2, 
                Vector3.left, 
                out rayCast_informacion, 
                Quaternion.identity, 
                distancia_colision, 
                mascara_objeto.value);

        if (colision_izquierda)                                                 //Verificamos si hay colisión por el lado de la izquierda
        {
            Debug.Log("Colision conizq");
            //si al player le impacta una bala enemiga
            if (rayCast_informacion.collider.gameObject.CompareTag("BalaEnemigo"))
            {
                Debug.Log("Colision con bala enemiga");
            }
        }        
    }

    
    private int ActualizarInventarioRapido(GameObject ga,Sprite accion)        //Permite el ingreso de items al inventario rapido que se encuentra en la pantalla
    {
        return GetComponent<GestionInventario>().asignarItemACasilla(ga,accion);       //Asignamos un item a la última casilla disponible
    }
    public void DetectarObjetoArma(Collider arma)
    {
        tiene_metralleta = false;                                               //Desactivamos la variable "tiene_metralleta"
        tiene_pistola = false;                                                  //Desactivamos la variable "tiene_pistola"
        BrazoD.SetActive(false);                                                //Desactivamos el brazo derecho sin arma controlado por animator
        tipo_arma = arma.name;                                                  //Asignamos el nombre del arma a la variable "tipo_arma"
        BrazoDerechoGirable.SetActive(true);                                    //Activamos el brazo Derecho Girable
                                                                                //BrazoD = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD");
        arma_jugador= GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
        tiene_arma = true;
                                                           //Activamos la variable "tiene_arma" para decirle a la animación que muestre las animaciones con arma
        if (tipo_arma == "Pistola")                                             //Verificamos que el objeto detectado sea una pistola
        {
            //armaActualEnMano = 1;
            //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite =         //Asignamos el sprite de la mano con pistola al brazo derecho girable
            //    mano_pistola;
            arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
            arma_jugador.GetComponent<SpriteRenderer>().sprite = sprite_pistola;    //Le damos la sprite de la pistola al objeto vacio llamado "arma_jugador" para que aparezca en la mano del jugador
            tiene_pistola = true;                                              //Activamos la variable "tiene_pistola" para decirle a la animación que muestre las animaciones con pistola
            sprite_mano_arma[0] = mano_pistola;
            armaActualEnMano = ActualizarInventarioRapido(arma.gameObject, sprite_mano_arma[0]);       //Actualizamos el inventario rapido con el sprite que contiene el sprite_mano_arma
        }

        if (tipo_arma == "Ametralladora")                                       //Verificamos que el objeto detectado sea una pistola
        {
            //armaActualEnMano = 1;
            //BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite =         //Asignamos el sprite de la mano con metralleta al brazo derecho girable
            //    mano_pistola;
            arma_jugador = GameObject.Find("Alessio/Cuerpo/AnteBrazoD/BrazoD_Arma/Arma");
            arma_jugador.GetComponent<SpriteRenderer>().sprite = sprite_metralleta;     //Le damos la sprite de la metralleta al objeto vacio llamado "arma_jugador" para que aparezca en la mano del jugador
            tiene_metralleta = true;                                            //Activamos la variable "tiene_metralleta" para decirle a la animación que muestre las animaciones con pistola
            sprite_mano_arma[1] = mano_metralleta;                                 
            armaActualEnMano = ActualizarInventarioRapido(arma.gameObject, sprite_mano_arma[1]);       //Actualizamos el inventario rapido con el sprite que contiene el sprite_mano_arma
        }
        Destroy(arma.gameObject);                                               //Destruimos el arma que está en el suelo
    }

    public void DetectarMuniciones(Collider objeto_municion)
    {
        
        GetComponent<GestionInventario>().recargarMunicion(objeto_municion.gameObject, objeto_municion.GetComponentInChildren<SpriteRenderer>().sprite);
        Destroy(objeto_municion.gameObject);
    }
    public void DetectarObjetoHiriente(Collider objeto_hiriente)
    {
        if (objeto_hiriente.name == "Bala_Rufianes(Clone)")                     //Verificamos que el objeto detectado sea una Bala del enemigo
        {                                                                       
            salud.ModificarVida(                                                //Activamos la acción de modificar la vida del personaje
                objeto_hiriente.GetComponent<Bala>().danio_bala, 
                objeto_hiriente.gameObject);
        }
    }
    public void DetectarObjetoCura(Collider objeto_cura)
    {
        Debug.Log("objeto_cura.name:"+objeto_cura.name);
        objeto_cura.name = objeto_cura.name.Replace("(Clone)",string.Empty);

        if (objeto_cura.name== "Curacion")                              //Verificamos que el objeto detectado sea primerosAuxilios
        {
            Debug.Log("accedi a curacion");
            if (salud._vidaActual == salud._vidaMaxima)                         //Verificamos si la cantidad de la vida actual es la vida máxima del jugador
            {
                ActualizarInventarioRapido(                                     //Guardamos los primeros axuilios en el inventario
                    objeto_cura.gameObject, 
                    objeto_cura.GetComponentInChildren<SpriteRenderer>().sprite);
            }
            else
            {
                salud.ModificarVida(                                            //Curamos al personaje si tiene menos vida que la vida máxima
                    objeto_cura.GetComponent<Sanacion>().vidaExtra,
                    objeto_cura.gameObject);            
               
            }
            Destroy(objeto_cura.gameObject);                                    //Destruimos el objeto que está curando
        }
    }
    #endregion

    #region Funcion Gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 caja_colision = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        Gizmos.DrawWireCube(transform.position, caja_colision);
        Vector3 down = new Vector3(0, -1, 0);
        Vector3 pos = transform.position + (down * distancia_colision);
        Gizmos.DrawWireCube(pos, caja_colision);
    }
    #endregion

    #region Funciones Alessio Interface
    //He dejado esto a un lado por cuestión de nombres, más adelante tocará cambiar el nombre a las interfaces
    //paolo: vas a cambiar los nombres de estas funciones? recuerda que son funciones comunes,la interfaz va a ser usada por todos los personajes
    public void Atacar()
    {



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