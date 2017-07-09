using System.Collections;
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

    #region Variables de Ataques
    public bool esta_atacando;                      //Variable para saber si el personaje está atacando
    public bool tiene_arma;                         //Variable para saber si el personaje tiene algún tipo de arma
    public string tipo_arma;                        //Variable para guardar el tipo de arma que esta usando actualmente
    public float danio_golpe;                       //Variable para determinar la cantidad de daño que tendrán los golpes del personaje
    public int contador_apoyo;                      //Variable para guardar la cantidad de poder acumulado para llamar a la habilidad especial
    public float intervalo_ataque = 0.5f;           //Variable para determinar la cantidad de tiempo de retroceso antes de poder volver a atacar    
    public float reactivacion_ataque;               //Variable para manejar la velocidad de ataque
    public Transform punto_disparo;                 //Variable para guardar la posición y rotación de punto del disparo
    #region Pistola
    public bool tiene_pistola;                      //Variable para saber si el personaje tiene una pistola
    public bool atacando_pistola;                   //Variable para saber si el personaje está atacando con una pistola
    public Transform arma_jugador;                  //Variable para guardar la posición y rotación del arma
    public Pistola pistola;                         //Variable para guardar la clase Pistola
    #endregion
    #endregion

    #region Variables de Movimiento
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el personaje
    public float veloidad_correr = 10;              //Variable para determinar la velocidad con la que corerrá el personaje
    public float velocidad_con_arma = 2;            //Variable para determinar la velocidad con la que se moverá el personaje cuando sostenga un arma
    public float velocidad_retrocediendo = 1;        //Variable para determinar la velocidad con la que retrocederá el personaje
    public float axis_horizontal;                   //Variable para guardar la cantidad del Axis en horizontal (-1,1)
    public float axis_vertical;                     //Variable para guardar la cantidad del Axis en vertical (-1,1)
    public bool correr;                             //Variable para saber si el personaje está corriendo
    public bool caminar;                            //Variable para saber si el personaje está caminando
    public bool fijar_camara;                       //Variable para fijar la mirada del personaje
    #endregion

    #region Variables de Colisiones
    public bool colision_abajo;                     //Variable para saber si el personaje aun se puede hacia abajo
    public bool colision_arriba;                    //Variable para saber si el personaje aun se puede hacia arriba
    public bool colision_derecha;                   //Variable para saber si el personaje aun se puede hacia derecha
    public bool colision_izquierda;                 //Variable para saber si el personaje aun se puede hacia izquierda
    public float distancia_colision = 0.6f;         //Variable para detectar la distancia entre el personaje y el objeto solido a colisionar
    public Vector3 caja_colision;                   //Variable para determinar la caja de colisión del personaje
    #endregion

    #region Variables de Retroceso (KnockBack)
    public float retroceso;                         //Variable para guardar la velocidad con la que retrocederá el personaje (KnockBack)
    public bool retroceder_derecha;                 //Variable para saber si el personaje va a retroceder a la derecha
    public bool retrocediendo;                      //Variable para saber si el personaje está retrocediendo, usado para la animación
    #endregion

    #region Variables de Animacion
    public Animator _animator;                      //Variable para guardar el Animator del personaje
    public Sprite[] sprite_mano_arma;                 //Arreglo que para guardar los sprites de la mano derecha con arma

    public SpriteRenderer sprite_brazoDerecho;      //Variable de Tipo Sprite para guardar el sprite del BrazoDerecho
    public float transparencia_objetivo;            //Variable para determinar el nivel de transparencia (Alpha)
    #endregion

    #region Variables de Personaje
    public Recurso[] inventario;	                //Variable de tipo "Arreglo" para almacenar los recursos del personaje
    public Rigidbody rigidbody_Jugador;             //Variable para guardar el RigidBody del personaje
    public GameObject Prefab_Bala;                  //Variable para guardar el prefab de la Bala
    public GameObject Prefab_Golpe;                 //Variable para guardar el prefab del golpe
    public GameObject Prefab_Explosion;             //Variable para guardar el prefab de la explosión
    public LayerMask _mask;                         //Variable para guardar y modificar la máscara del Personaje
    public bool puede_controlar = true;             //Variable para determinar si el jugador puede controlar al personaje    
    #region Partes del Cuerpo
    public GameObject cabeza;                       //Variable para guardar el GameObject de la cabeza del jugador
    public GameObject cuerpo;                       //Variable para guardar el GameObject del cuerpo del jugador
    public GameObject AnteBrazoD;                   //Variable para guardar el GameObject del Ante Brazo Derecho del jugador
    public GameObject BrazoD;                       //Variable para guardar el GameObject del Brazo Derecho del jugador
    public GameObject BrazoDerechoGirable;          //Variable para guardar el GameObject del Brazo Derecho Variable del jugador
    public GameObject AnteBrazoI;                   //Variable para guardar el GameObject del Ante Brazo Izquierdo del jugador
    public GameObject BrazoI;                       //Variable para guardar el GameObject del Brazo Izquierdo del jugador
    public GameObject MusloD;                       //Variable para guardar el GameObject del Muslo Derecho del jugador
    public GameObject PiernaD;                      //Variable para guardar el GameObject de la Pierna Derecha del jugador
    public GameObject PieD;                         //Variable para guardar el GameObject del Pie Derecho del jugador
    public GameObject MusloI;                       //Variable para guardar el GameObject del Muslo Izquierdo del jugador
    public GameObject PiernaI;                      //Variable para guardar el GameObject de la Pierna Izquierdo del jugador
    public GameObject PieI;                         //Variable para guardar el GameObject del Pie Izquierdo del jugador
    #endregion
    #endregion

  
    #endregion

    #region Funciones de Unity
    void Start()
    {
        retroceso = 0;                                  //Iniciamos la variable "retroceso con 0", porque al inicio tiene un número positivo y eso genera errores en la animación del inicio        
        #region Asignando el cuerpo del Personaje
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
        #endregion
    }

    void Update()
    {
        JugadorHerido();
        GestorVida();                               //Función para gestionar la vida del objeto y sus respectivas acciones
        GestorTeclado();                            //Función para recibir los Inputs del teclado
        GestorMouse();                              //Función para recibir los Inputs del mouse
        GestorAtaques();                            //Función para gestionar los Ataques y tipos de Ataques
        GestorAnimaciones();                        //Función para gestionar las animaciones del objeto
        GestorRetroceso();                          //Función para gestionar el retroceso del jugador
        GestorParpadeo();                           //Función para gestionar el parpadeo del personaje
        seleccionarItem();
    }

    void FixedUpdate()
    {
        GestorMovimiento();                         //Función para manejar el movimiendo con fisica
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Arma"))
        {                                           //Verificamos que el objeto colisionado sea un arma (Tag: Arma)            
            DetectarObjetoArma(other);              //Función para detectar el arma con el que ha colisionado
        }
        if (other.CompareTag("Item"))
        {                                           //Verificamos que el objeto colisionado sea un item (Tag: Item)            
            DetectarObjetoCura(other);             //Función para detectar el objeto que le aumentará vida
        }
        DetectarObjetoHiriente(other);              //Función para detectar el objeto que le disminuirá vida
                         
        ColissionParedes();


    }

    #endregion

    #region Funciones del Update
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
                    Debug.Log("Se ha presionado el botón 0: Clic Izquierdo, esta_atacando = true");
                }
                if (Input.GetMouseButton(1))                    //Verificamos que si hay presión sobre el clic Derecho
                {
                    Debug.Log("Se ha presionado el botón 1: Clic Derecho");
                }
                if (Input.GetMouseButton(2))                    //Verificamos que si hay presión sobre el clic del Centro
                {
                    Debug.Log("Se ha presionado el botón 2: Clic del Centro");
                }
            }
        }
    }
    public void GestorAtaques()
    {
        if (atacando_pistola)                                   //Verificamos si el personaje está atacando con una pistola
        {
            atacando_pistola = false;                           //Desactivamos la variable "atacando_pistola"
        }
        if (esta_atacando && tiene_pistola)                     //Verificamos si el personaje está atacando y si el tipo de arma que sostiene es una Pistola
        {
            atacando_pistola = true;                            //Activamos la variable "atacando_pistola" para decirle a la animación que debe ejecutar
            Instantiate(Prefab_Bala,
                punto_disparo.transform.position,
                punto_disparo.transform.rotation);               //Creamos la bala con las mismas caracteristicas del GameObject vacío "Arma_Player" del personaje
        }

    }
    public void GestorAnimaciones()
    {
        _animator.SetBool("Caminar", caminar);                  //Asignamos el valor del estado "caminar" a la animación
        _animator.SetBool("Correr", correr);                    //Asignamos el valor del estado "caminar" a la animación
        _animator.SetBool("AtacandoPistola", atacando_pistola); //Asignamos el valor de "atacando_pistola" a la animación
        _animator.SetBool("TienePistola", tiene_pistola);       //Asignamos el valor de "tiene_pistola" a la animación
        _animator.SetBool("TieneArma", tiene_arma);             //Asignamos el valor de "tiene_arma" a la animación                          
        _animator.SetBool("EsHerido", retrocediendo);
    }
    #endregion

    #region Funciones del FixedUpdate
    public void GestorMovimiento()
    {
        Vector3 moveVector = new Vector3(0, 0, 0);                  //Creamos un Vector 3, el cual nos serirá para definir el movimiento
        float velocidad_movimiento = 0;                               //Asignamos una variable para la velocidad de movimiento
        if (!fijar_camara)                                         //Verificamos que no esté retrocediendo para aplicar los giros
        {
            if (axis_horizontal > 0)                                    //Verificamos si el personaje se mueve hacia la derecha
            {
                transform.rotation = Quaternion.identity;               //Giramos el cuerpo del personaje hacia la derecha
            }
            if (axis_horizontal < 0)                                    //Verificamos si el personaje se mueve hacia la izquierda
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);      //Giramos el cuerpo del personaje hacia la izquierda
            }
        }
        if (retroceso > 0)
        {                                                           //Verificamos que exista un retroceso mayor a 0                        
            if (retroceder_derecha)
            {                                                       //Verificamos si el personaje retrocerá hacia la derecha
                moveVector.x = retroceso;                           //Hacemos retroceder al personaje hacia la derecha
            }
            else
            {
                moveVector.x = -retroceso;                          //Hacemos retroceder al personaje hacia la derecha
            }
        }
        else
        {
            velocidad_movimiento = velocidad_normal;                //Asignamos la velocidad para caminar

            if (correr)                                             //Verificamos si el personaje está corriendo
                velocidad_movimiento = veloidad_correr;             //Asignamos la velocidad para correr

            if (fijar_camara)                                       //Verificamos si el personaje está retrocediendo
                velocidad_movimiento = velocidad_retrocediendo;     //Asignamos la velocidad para retroceder

            moveVector.x = axis_horizontal * velocidad_movimiento;  //Definioms el vector de movimiento en el eje X
            moveVector.y = axis_vertical * velocidad_movimiento;    //Definioms el vector de movimiento en el eje y
        }
        rigidbody_Jugador.velocity = moveVector;                    //Le damos la velocidad al personaje con el moveVector
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
                //Debug.Log("color=" + newColor.a);
                transparencia_objetivo = 0;                                 //Le decimos que se haga invisible
            }
            else if (newColor.a < 0.1f)                                     //Revisamos el valor de la transparencia
            {
                //Debug.Log("color2=" + newColor.a);
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

    //funcion que gestiona todos los cambios que se implementan en las caracteristicas del jugador cuando es herido por un elemento del juego
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

    //permite el intercambiar los items que se encuentran en el inventario rapido
    public void seleccionarItem()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(vKey))
            {
                //Debug.Log("Presione " + vKey.ToString());
                switch (vKey.ToString())
                {
                    //POR AHORA TIENEN UNA VALIDACION IF PREVIA con el sprite.name, de alli continuo con esta logica para el intercambio de armas
                    case "Alpha2":
                        Debug.Log("Seleccione item 2:");
                        Debug.Log("Nombre:" + GetComponent<GestionInventario>().items[1]);
                        if (GetComponent<GestionInventario>().items[1].objeto.name == "Curacion")
                        {
                            Sanacion sanaAux = GetComponent<GestionInventario>().items[1].objeto.GetComponent<Sanacion>();
                            salud.ModificarVida(sanaAux.vidaExtra, GetComponent<GestionInventario>().items[1].objeto);
                        }
                            //GetComponent<GestionInventario>().itemRapido[0].GetComponent<Image>().sprite.name == "Alessio-SPRITE-PISTOLA_0"
                         else   BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[1].accion;
                       
                        break;
                    case "Alpha1":
                          Debug.Log("Seleccione item 1:");
                        BrazoDerechoGirable.GetComponent<SpriteRenderer>().sprite = GetComponent<GestionInventario>().items[0].accion;

                        break;
                }

            }
        }
    }
    #endregion

    #region Funciones del onTrigger (Colisiones)

    public void ColissionParedes()
    {
        caja_colision = transform.localScale;                           //Asignamos el valor del tramaño del objeto a la caja de colisión
        caja_colision *= 0.99f;                                         //y le multiplicamos por un número pequeño para que la caja sea más grande que el objeto
        RaycastHit raycastInfo;

        //raycast para controlar si hay colision desde la izquierda
        colision_izquierda = Physics.BoxCast(transform.position, caja_colision / 2, Vector3.left, out raycastInfo, Quaternion.identity, distancia_colision, _mask.value);
        if (colision_izquierda)
        {
            Debug.Log("Colision conizq");
            //si al player le impacta una bala enemiga
            if (raycastInfo.collider.gameObject.CompareTag("BalaEnemigo"))
            {
                Debug.Log("Colision con bala enemiga");
            }

        }
    }

    //permite el ingreso de items al inventario rapido que se encuentra en la pantalla
    private void ActualizarInventarioRapido(GameObject ga,Sprite accion)
    {
        GetComponent<GestionInventario>().asignarItemACasilla(ga,accion);
    }
    public void DetectarObjetoArma(Collider objeto)
    {
        if (objeto.tag == "Arma")
        {
          
                BrazoD.SetActive(false);                    //Desactivamos el brazo derecho controlado por animator
                BrazoDerechoGirable.SetActive(true);      //Activamos el brazo Derecho Girable
                tipo_arma = objeto.name;               //Reconocemos el tipo de arma como "Pistola" (Nombre: Pistola)
                                                       //sprite_brazoDerecho.sprite =
                                                       //    sprite_mano_arma;                       //Cambiamos el brazo sin arma, por un brazo con Arma
                tiene_pistola = true;                       //Activamos la variable "tiene_pistola" para decirle a la animación que muestre las animaciones con pistola
                tiene_arma = true;                          //Activamos la variable "tiene_arma" para decirle a la animación que muestre las animaciones con arma
                ActualizarInventarioRapido(objeto.gameObject, sprite_mano_arma[0]);
                Destroy(objeto.gameObject);            //Destruimos el arma que está en el suelo

            }

            if (objeto.name == "Ametralladora")        //Verificamos que tipo de arma encontramos
            {
                BrazoD.SetActive(false);
                BrazoDerechoGirable.SetActive(true);
                tipo_arma = objeto.name;               //Reconocemos el tipo de arma como "Pistola" (Nombre: Pistola)
                                                       //sprite_brazoDerecho.sprite =
                                                       //    sprite_mano_arma;                       //Cambiamos el brazo sin arma, por un brazo con Arma
                tiene_pistola = true;                       //Activamos la variable "tiene_pistola" para decirle a la animación que muestre las animaciones con pistola
                tiene_arma = true;                          //Activamos la variable "tiene_arma" para decirle a la animación que muestre las animaciones con arma
                ActualizarInventarioRapido(objeto.gameObject, sprite_mano_arma[1]);
                Destroy(objeto.gameObject);            //Destruimos el arma que está en el suelo

            }

        

        //if (objeto_arma.name == "Ametralladora")        //Verificamos que tipo de arma encontramos
        //{
        //    BrazoD.SetActive(false);
        //    BrazoDerechoGirable.SetActive(true);
        //    tipo_arma = objeto_arma.name;               //Reconocemos el tipo de arma como "Pistola" (Nombre: Pistola)
        //    //sprite_brazoDerecho.sprite =
        //    //    sprite_mano_arma;                       //Cambiamos el brazo sin arma, por un brazo con Arma
        //    tiene_pistola = true;                       //Activamos la variable "tiene_pistola" para decirle a la animación que muestre las animaciones con pistola
        //    tiene_arma = true;                          //Activamos la variable "tiene_arma" para decirle a la animación que muestre las animaciones con arma
        //    ActualizarInventarioRapido(objeto_arma.gameObject, sprite_mano_arma[1]);
        //    Destroy(objeto_arma.gameObject);            //Destruimos el arma que está en el suelo

        //}
    }
    public void DetectarObjetoHiriente(Collider objeto_hiriente)
    {
        if (objeto_hiriente.CompareTag("BalaEnemigo"))
        {                                               //Verificamos que el objeto sea una Bala del enemigo
            salud.ModificarVida(                        //Activamos la acción de modificar la vida del personaje
                objeto_hiriente.GetComponent<Bala>().danio_bala, 
                objeto_hiriente.gameObject);
        }
        if (objeto_hiriente.CompareTag("Enemigo"))
        {                                               //Verificamos que el objeto sea un enemigo
            salud.ModificarVida(20, 
                objeto_hiriente.gameObject);            //Activamos la acción de modificar la vida del personaje

        }
    }
    public void DetectarObjetoCura(Collider objeto_cura)
    {
        if (objeto_cura.name=="Curacion")
        {
            //si la salud del jugador esta completa, que la cura se guarde en inventario
            if (salud._vidaActual == salud._vidaMaxima)
            {
                ActualizarInventarioRapido(objeto_cura.gameObject, objeto_cura.GetComponentInChildren<SpriteRenderer>().sprite);
               
            }
            //si el jugador esta herido, que la cura se use de inmediato
            else
            {
                Debug.Log("Se curo");
                salud.ModificarVida(objeto_cura.GetComponent<Sanacion>().vidaExtra,
                    objeto_cura.gameObject);            //Activamos la acción de modificar la vida del personaje
               
            }
            Destroy(objeto_cura.gameObject);
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