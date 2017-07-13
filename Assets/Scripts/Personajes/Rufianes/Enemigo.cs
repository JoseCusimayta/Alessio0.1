using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour, IPersonaje
{

    #region Variables

    #region Variables de Salud
    public Health salud;                            //Variable para obtener la clase Health
    public float vida_actual;                       //Variable para guardar la cantidad de vida actual    
    public float vida_maxima;                       //Variable para determinar la cantidad de vida máxima posible que puede tener el Objeto
    public float vida_anterior;                     //Variable para guardar la cantidad de vida que tenía antes de ser golpeado    
    #endregion

    #region Variables de Ataques
    public GameObject punto_disparo;                //Variable para determinar cual será el puntode disparo del Rufian
    public float radio_Alcance;		                //Variable para determinar el radio de visión del objeto para activar sus funciones de Ataque    
    public bool persecucion;                        //Variable para determinarsi el Rufín tendrá la acción de perseguir al jugador cuando lo detecte
    public bool persiguiendo;                       //Variable para saber si el objeto está persiguiendo a Alessio
    public PatronMovimiento patronMovimiento;       //Variable para guardar el patrón de movimiento del objeto    
    public float intervalo_ataque;                  //Variable para determinar la cantidad de tiempo de retroceso antes de poder volver a atacar
    public float reactivacion_ataque = 2;           //Variable para manejar la velocidad de ataque
    public bool jugador_detectado;                  //Variable para determinar si se ha detectado un jugador
    public CampoEnemigo rango_vision;               //Variable para guardar los datos que tenga el Campo de Visión
    public bool esta_atacando;                      //Variable para saber si el personaje está atacando
    #endregion

    #region Variables de Movimiento
    public bool mirando_derecha;                    //Variable para determinar si el objeto está mirando a la derecha
    public bool patrullaje;                         //Variable para determinar si el objeto estará en reposo o en patrullaje constante
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el objeto
    Vector3 direccion_movimiento;                   //Variable para determinar hacia dónde se moverá
    float x, y, z;                                  //Variable para determinar dónde aparecerán los objetos
    public Vector3 posicion_original;             //Variable para guardar la posición inicial del objeto
    #region Patron de Movimiento
    public GameObject rufian;		                //Variable para guardar al objeto Rufian, para controlar su movimiento en patrullaje
    public Transform[] lista_coordenadas; 		    //Variable para guardar la Cantidad de Coordenadas que se guardará
    public Transform coordenada_objetivo; 		    //Variable para guardar las Coordenadas del punto al cual se dirigirá
    public int indice_coordenada;			        //Variable para guardar el indice de la lista_coordenadas para conseguir las Coordenadas a la cual dirigirse
    #endregion
    #endregion

    #region Variables de Personaje
    public GameObject jugador;                       //Variable para guardar al objeto "Player/Jugador"
    #endregion

    #region Variables de Rufian
    public GameObject Prefab_Bala, Prefab_Rufian, Prefab_Explosion;
    public GameObject[] itemsDesprendibles;         //items que se despenden cuando muera el enemigo
    #endregion

    #endregion

    #region Funciones de Unity
    void Start()
    {
        //Debug.Log("itemsDesprendibles[0]" + itemsDesprendibles[0]);
        jugador = GameObject.FindGameObjectWithTag("Player");            //Buscamos al jugador en la escena
        coordenada_objetivo = lista_coordenadas[indice_coordenada];     //Definimos cual será el primer punto a dirigirse para patrullar
        posicion_original = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       
        GestorVida();                               //Función para gestionar la vida del objeto y sus respectivas acciones        
        GestorAtaques();                            //Función para gestionar los Ataques y tipos de Ataques
        GestorAnimaciones();                        //Función para gestionar las animaciones del objeto
        GestorRetroceso();                          //Función para gestionar el retroceso del jugador
        GestorParpadeo();                           //Función para gestionar el parpadeo del personaje
        GestorGiros();                              //Funcion para gestionar los giros de personaje con respecto al jugador
    }
    void FixedUpdate()
    {
        GestorMovimiento();                         //Función para manejar el movimiendo con fisica
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaPlayer"))
        {
            salud.ModificarVida(other.GetComponent<Bala>().danio_bala, other.gameObject);
        }
    }
    #endregion

    #region Funciones del Update
   
    public void GestorVida()
    {
        vida_actual = salud._vidaActual;
        //Debug.Log("Vida enemigo:" + vida_actual);//Actualizamos la vida del objeto
        if (vida_actual <= 0) //Verificamos si el personaje está sin vida
        {
            
            //Morir();
            // Nuevo_Rufian();                                     //Creamos un nuevo Rufian
        }
    }
    public void GestorAtaques()
    {
        if (esta_atacando)                                      //Verificamos si el personaje está atacando
        {
            intervalo_ataque = reactivacion_ataque;             //Reiniciamos el tiempo de reactivación para atacar
            esta_atacando = false;                              //Desactivamos la variable "esta_atacando", si no, se reiniciaría el tiempo en cada frame
        }

        if (intervalo_ataque > 0)                               //Verificamos el intervalo de ataques
        {
            intervalo_ataque -= Time.deltaTime;                 //Reducimos el tiempo del intervalo en cada segundo hasta llegar a 0
        }
        else
        {
            if (rango_vision.personaje_detectado && !persiguiendo)               //Verificamos si el personaje está cerca
            {
                Instantiate(Prefab_Bala,
                    punto_disparo.transform.position,
                    punto_disparo.transform.rotation);          //Comienza a disparar desde el objeto vacio
                esta_atacando = true;                           //Activamos esta variable para decirle a la animación que hacer
            }
            else if (persiguiendo)
            {
                Instantiate(Prefab_Bala,
                    punto_disparo.transform.position,
                    punto_disparo.transform.rotation);          //Comienza a disparar desde el objeto vacio
                esta_atacando = true;                           //Activamos esta variable para decirle a la animación que hacer
            }
        }
    }
    public void GestorAnimaciones()
    {
    }
    public void GestorRetroceso()
    {
    }
    public void GestorParpadeo()
    {
    }
    public void Nuevo_Rufian()
    {
        x = Random.Range(10f, 20f);                                         //Posición del eje Y al azar, entre 10 y 20
        y = Random.Range(-4f, 5f);                                          //Posición del eje X al azar, entre -4 y 5
        z = 0.0f;                                                           //Posición del eje Z en 0
        Vector3 vector3 = new Vector3(x, y, z);                             //Se crea un vector para guardar la posición en los ejes
        vida_actual = 10;                                                   //Establecer cantidad de vida del rufian
        Instantiate(Prefab_Rufian, vector3, transform.rotation);            //Crear un nuevo rufian con los anteriores valores 
    }
    public void GestorGiros()
    {
        if (jugador)
        {
            if (transform.position.x > jugador.transform.position.x)        //Calculamos quien esta a la derecha
            {
                mirando_derecha = false;                                    //Desactivamos el "mirando_derecha" porque el jugador está a la izquierda
                transform.rotation = new Quaternion(0, 180, 0, 0);          //Giramos al objeto hacia la izquierda
            }
            else
            {
                mirando_derecha = true;                                     //Activamos el "mirando_derecha" porque el jugador está a la derecha
                transform.rotation = Quaternion.identity;                   //Giramos al objeto hacia la derecha
            }
        }
    }
    #endregion

    #region Funciones del FixedUpdate
    public void GestorMovimiento()
    {
        if (persiguiendo)                                                       //Verificamos si el objeto está persiguiendo
        {
            GestorAtaquePersecucion();                                          //Activamos el gestor de persecicion
            GestorAtaques();                                                    //Tambien activamos el gestor de ataques
        }
        else
        {
            if (rango_vision.personaje_detectado)                               //Verificamos si ha detectado al jugador
            {
                if (persecucion)                                                //Verificamos que tipo de movimiento fue asignado para cuando detecta al jugador
                {
                    persiguiendo = true;
                    GestorAtaquePersecucion();                                  //Iniciamos la persecución
                }
                else
                {
                    persiguiendo = false;
                    GestorAtaqueReposo();                                       //Iniciamos el disparo normal sin perseguir
                }
            }
            else
            {
                if (patrullaje)                                                 //Verificamos que tipo de movimiento fue asignado para cuando NO detecta al jugador
                {
                    GestorPatronPatrullaje();                                   //Iniciamos el patrullaje
                }
                else
                {
                    GestorPatronReposo();                                       //Nos quedamos en espera
                }
            }
        }
    }
    public void GestorPatronPatrullaje()
    {
        rufian.transform.position = Vector3.MoveTowards(                        //Funcion para mover al objeto
            rufian.transform.position,	                                        //Definimos la posición del objeto
            coordenada_objetivo.position,			                            //Definimos la posición a la cual se dirigirá (punto destino)
            Time.deltaTime * velocidad_normal			                        //Definimos la velocidad de movimiento (caminar) para patrullar
        );

        if (rufian.transform.position == coordenada_objetivo.position)          //Verificamos si el objeto llego al punto destino
        {
            indice_coordenada += 1;                                             //Indicamos que ahora debe ir al siguiente punto
            if (indice_coordenada == lista_coordenadas.Length)                  //Verificamos si llego al último punto
            {
                indice_coordenada = 0;                                          //Regresamos al punto inicial
            }
            coordenada_objetivo = lista_coordenadas[indice_coordenada];         //Definimos las coordenadas del punto al cual se moverá el objeto
        }
    }
    public void GestorPatronReposo()
    {

        rufian.transform.position = Vector3.MoveTowards(                                    //Funcion para mover al objeto
        rufian.transform.position,	                                                        //Definimos la posición del objeto
        posicion_original,                          			                            //Definimos la posición a la cual se dirigirá (punto destino)
        Time.deltaTime * velocidad_normal			                                        //Definimos la velocidad de movimiento (caminar) para patrullar
        );
    }
    public void GestorAtaquePersecucion()
    {
        if (jugador)                                                            //Verificamos la existencia del jugador
        {            
            if (mirando_derecha)                                                                //Verificamos si el objeto está mirando a la derecha
            {
                transform.Translate(Vector3.right * velocidad_normal * Time.deltaTime);         //Movemos el objeto al a derecha
            }
            else
            {
                transform.Translate(Vector3.left * -velocidad_normal * Time.deltaTime);         //Movemos el objeto a la izquierda
            }
            if (transform.position.y > jugador.transform.position.y)                            //Verificamos si el objeto está arriba del personaje
            {
                transform.Translate(Vector3.down * velocidad_normal * Time.deltaTime);          //Movemos el objeto hacia abajo
            }
            else
            {
                transform.Translate(Vector3.up * velocidad_normal * Time.deltaTime);            //Movemos el objeto hacia arriba
            }
        }
    }
    public void GestorAtaqueReposo()
    {
        if (jugador)                                                                            //Verificamos la existencia del jugador
        {
            if (mirando_derecha)                                                                //Verificamos si el objeto está mirando a la derecha
            {
                transform.Translate(Vector3.right * velocidad_normal * Time.deltaTime);         //Movemos el objeto al a derecha
            }
            else
            {
                transform.Translate(Vector3.left * -velocidad_normal * Time.deltaTime);         //Movemos el objeto a la izquierda
            }
            if (transform.position.y > jugador.transform.position.y)                            //Verificamos si el objeto está arriba del personaje
            {
                transform.Translate(Vector3.down * velocidad_normal * Time.deltaTime);          //Movemos el objeto hacia abajo
            }
            else
            {
                transform.Translate(Vector3.up * velocidad_normal * Time.deltaTime);            //Movemos el objeto hacia arriba
            }
        }
    }

    public GameObject desprenderItem()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Desprendera un item al morir");
        //el item se elige aleatoriamente
        int itemElegido = Random.Range(0, itemsDesprendibles.Length - 1);
        Debug.Log("itemElegido="+ itemElegido);
        itemsDesprendibles[itemElegido].transform.position = gameObject.transform.position;
        itemsDesprendibles[itemElegido].transform.rotation = gameObject.transform.rotation;

        //gameObject.SetActive(false);
        Instantiate(itemsDesprendibles[itemElegido], itemsDesprendibles[itemElegido].transform.position, itemsDesprendibles[itemElegido].transform.rotation);
        return itemsDesprendibles[itemElegido];
    }
    #endregion

    #region Funciones del onTrigger
    #endregion

    #region Funciones Interface

    public void Atacar()
    {
    }

    public void Morir()
    {
        Debug.Log("Desprendera un item al morir");
        //el item se elige aleatoriamente
        int itemElegido = Random.Range(0, itemsDesprendibles.Length);
        Debug.Log("itemElegido=" + itemElegido);
        itemsDesprendibles[itemElegido].transform.position = gameObject.transform.position;
        itemsDesprendibles[itemElegido].transform.rotation = gameObject.transform.rotation;

        //Al morir el enemigo desprende un item al azar;
        string nombreAux = itemsDesprendibles[2].name;
        Debug.Log("nombre del item=" + nombreAux);
        GameObject g= Instantiate(itemsDesprendibles[2], itemsDesprendibles[itemElegido].transform.position, itemsDesprendibles[itemElegido].transform.rotation);
        //se guarda la variable nombreAux, ya que al instanciar un objeto aparece con el nombre seguido de un "(clone)" y eso no permite su busqueda para añadirlo a los items
        g.name= nombreAux;
        Destroy(gameObject);
    }
    public void Mover()
    {
    }
    public void Coger()
    {
    }

    public void Saltar()
    {
    }

    public void Correr()
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
    #endregion
}