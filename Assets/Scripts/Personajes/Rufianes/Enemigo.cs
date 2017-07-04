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
    public bool patrullaje;                         //Variable para determinar si el objeto estará en reposo o en patrullaje constante
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el objeto
    Vector3 direccion_movimiento;                   //Variable para determinar hacia dónde se moverá
    float x, y, z;                                  //Variable para determinar dónde aparecerán los objetos
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
    #endregion

    #endregion

    #region Funciones de Unity
    void Start()
    {
        jugador= GameObject.FindGameObjectWithTag("Player");            //Buscamos al jugador en la escena
        coordenada_objetivo = lista_coordenadas[indice_coordenada];     //Definimos cual será el primer punto a dirigirse para patrullar
    }

    // Update is called once per frame
    void Update()
    {
        Hurt();
        GestorVida();                               //Función para gestionar la vida del objeto y sus respectivas acciones        
        GestorAtaques();                            //Función para gestionar los Ataques y tipos de Ataques
        GestorAnimaciones();                        //Función para gestionar las animaciones del objeto
        GestorRetroceso();                          //Función para gestionar el retroceso del jugador
        GestorParpadeo();                           //Función para gestionar el parpadeo del personaje
    }
    void FixedUpdate()
    {
        GestorMovimiento();                         //Función para manejar el movimiendo con fisica
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().ModificarVida(20, gameObject);

        }
        if (other.CompareTag("BalaPlayer"))
        {
            salud.ModificarVida(other.GetComponent<Bala>().danio_bala, other.gameObject);
        }
    }
    #endregion

    #region Funciones del Update
    public void Hurt()
    {
    }
    public void GestorVida()
    {
        vida_actual = salud._vidaActual;                        //Actualizamos la vida del objeto
        if (vida_actual <= 0)
        {                                                       //Verificamos si el personaje está sin vida
            Nuevo_Rufian();                                     //Creamos un nuevo Rufian
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
            if (rango_vision.personaje_detectado)               //Verificamos si el personaje está cerca
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
    #endregion

    #region Funciones del FixedUpdate
    public void GestorMovimiento()
    {
        if (persiguiendo)
        {
            GestorAtaquePersecucion();
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
        rufian.transform.position = Vector3.MoveTowards(                    //Funcion para mover al objeto
            rufian.transform.position,	                                    //Definimos la posición del objeto
            coordenada_objetivo.position,			                        //Definimos la posición a la cual se dirigirá (punto destino)
            Time.deltaTime * velocidad_normal			                    //Definimos la velocidad de movimiento (caminar) para patrullar
        );

        if (rufian.transform.position == coordenada_objetivo.position)      //Verificamos si el objeto llego al punto destino
        {
            indice_coordenada += 1;                                         //Indicamos que ahora debe ir al siguiente punto
            if (indice_coordenada == lista_coordenadas.Length)              //Verificamos si llego al último punto
            {
                indice_coordenada = 0;                                      //Regresamos al punto inicial
            }
            coordenada_objetivo = lista_coordenadas[indice_coordenada];     //Definimos las coordenadas del punto al cual se moverá el objeto
        } 
    }
    public void GestorPatronReposo()
    {
    }
    public void GestorAtaquePersecucion()
    {
        direccion_movimiento = 
            jugador.transform.position - transform.position;                //Calculamos la distancia entre ambos y lo guardamos en el Vector
        direccion_movimiento.Normalize();                                   //Obtenemos la dirección del Vector
        transform.Translate(direccion_movimiento 
            * velocidad_normal * Time.deltaTime);                           //Movemos al objeto hacia al jugador  
    }
    public void GestorAtaqueReposo()
    {
        direccion_movimiento =
           jugador.transform.position - transform.position;                //Calculamos la distancia entre ambos y lo guardamos en el Vector
        direccion_movimiento.Normalize();                                   //Obtenemos la dirección del Vector
        transform.Translate(direccion_movimiento
            * velocidad_normal * Time.deltaTime);                           //Movemos al objeto hacia al jugador  
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