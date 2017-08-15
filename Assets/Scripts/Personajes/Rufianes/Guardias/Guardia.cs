using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardia : MonoBehaviour {

    #region Variables

    #region Variables de Salud
    public Health salud;                            //Variable para obtener la clase Health
    public float vida_actual;                       //Variable para guardar la cantidad de vida actual    
    public float vida_maxima;                       //Variable para determinar la cantidad de vida máxima posible que puede tener el Objeto
    public float vida_anterior;                     //Variable para guardar la cantidad de vida que tenía antes de ser golpeado    
    #endregion

    #region Patron de Movimiento    
    public Transform[] lista_coordenadas; 		    //Variable para guardar la Cantidad de Coordenadas que se guardará
    public Transform coordenada_objetivo; 		    //Variable para guardar las Coordenadas del punto al cual se dirigirá
    public int indice_coordenada;                   //Variable para guardar el indice de la lista_coordenadas para conseguir las Coordenadas a la cual dirigirse
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el objeto
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
    public Transform player;                        //Variable para guardar la posición de jugador, para saber hacia donde disparar
    #endregion

    #region Variables de Rufian
    public GameObject Prefab_Bala, Prefab_Rufian, Prefab_Explosion;     //Lista de prefabs
    public string Tipo_Guardia;                                         //Elegir tipo de guardia, por ahora solo hay "Patrullaje"
    public GameObject[] itemsDesprendibles;         //items que se despenden cuando muera el enemigo
    #endregion

    #endregion

    #region Funciones de Unity
    // Use this for initialization
    void Start () {
        if (Tipo_Guardia == "Patrullaje")                                   //Verificamos el tipo de guardia, en este caso, patrullaje
        {
            coordenada_objetivo = lista_coordenadas[indice_coordenada];     //Definimos cual será el primer punto a dirigirse para patrullar
        }
        salud = GetComponent<Health>();                                     //Guardamos el script de vida en una variable
        rango_vision = GetComponentInChildren<CampoEnemigo>();              //Guardamos el script del campo de visión, como CampoEnemigo
    }

    // Update is called once per frame
    void Update() {
        if (Tipo_Guardia == "Patrullaje" && !rango_vision.personaje_detectado)  //Verificamos si esta en tipo patrullaje y no ha detectado al jugador para hacerlo patrullar
        {
            GestorPatrullaje();                                                 //Patrullamos
        }
        if (rango_vision.personaje_detectado)                                   //Verificamos si se ha tectado al enemigo en el rango de visión del ataque
        {
            GestorGiros();                                                      //Establecemos la orientación hacía el jugador
            GestorAtaques();                                                    //Establecemos el ataque del Guardia
        }
        GestorVida();                                                           //Actualizamos la vida
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BalaPlayer"))                                     //Verificamos si la bala del jugador le ha dado
        {
            salud.ModificarVida(other.GetComponent<Bala>().danio_bala, other.gameObject);   //Modificamos la vida, en este caso, la disminuimos
        }
    }
    #endregion

    #region Funciones para Update
    void GestorPatrullaje()                                                 //Función de patrullaje
    {
        transform.position = Vector3.MoveTowards(                        //Funcion para mover al objeto
            transform.position,	                                        //Definimos la posición del objeto
            coordenada_objetivo.position,			                            //Definimos la posición a la cual se dirigirá (punto destino)
            Time.deltaTime * velocidad_normal			                        //Definimos la velocidad de movimiento (caminar) para patrullar
        );
        if (transform.position == coordenada_objetivo.position)          //Verificamos si el objeto llego al punto destino
        {
            indice_coordenada += 1;                                             //Indicamos que ahora debe ir al siguiente punto
            if (indice_coordenada == lista_coordenadas.Length)                  //Verificamos si llego al último punto
            {
                indice_coordenada = 0;                                          //Regresamos al punto inicial
            }
            coordenada_objetivo = lista_coordenadas[indice_coordenada];         //Definimos las coordenadas del punto al cual se moverá el objeto
        }
        if (coordenada_objetivo.position.x > transform.position.x)          //Verificamos hacia donde se esta movimiendo
        {
            transform.rotation = Quaternion.identity;                   //Giramos el cuerpo del personaje hacia la derecha
        }
        else
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);          //Giramos el cuerpo del personaje hacia la izquierda
        }
    }

    public void GestorVida()
    {
        vida_actual = salud._vidaActual;
    }
    public void GestorGiros()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>() ;   //Guardamos la posición actual del jugador
        if (player)
        {
            if (player.position.x > transform.position.x)                   //Verificamos en donde esta el jugador
            {
                transform.rotation = Quaternion.identity;                   //Giramos el cuerpo del personaje hacia la derecha
            }
            else
            {
                transform.rotation = new Quaternion(0, 180, 0, 0);          //Giramos el cuerpo del personaje hacia la izquierda
            }
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
                Debug.Log("sds");
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
    #endregion

    #region Funciones para OnTriggerEnter

    public void Morir()
    {
        //el item se elige aleatoriamente
        int itemElegido = Random.Range(0, itemsDesprendibles.Length);//Random.Range(0, itemsDesprendibles.Length);
        itemsDesprendibles[itemElegido].transform.position = gameObject.transform.position;
        itemsDesprendibles[itemElegido].transform.rotation = gameObject.transform.rotation;

        //Al morir el enemigo desprende un item al azar;
        string nombreAux = itemsDesprendibles[itemElegido].name;
        GameObject g = Instantiate(itemsDesprendibles[itemElegido], itemsDesprendibles[itemElegido].transform.position, itemsDesprendibles[itemElegido].transform.rotation);
        //se guarda la variable nombreAux, ya que al instanciar un objeto aparece con el nombre seguido de un "(clone)" y eso no permite su busqueda para añadirlo a los items
        g.name = nombreAux;
        Destroy(gameObject);
    }
    public GameObject DesprenderItem()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        //el item se elige aleatoriamente
        int itemElegido = Random.Range(0, itemsDesprendibles.Length - 1);
        itemsDesprendibles[itemElegido].transform.position = gameObject.transform.position;
        itemsDesprendibles[itemElegido].transform.rotation = gameObject.transform.rotation;

        //gameObject.SetActive(false);
        Instantiate(itemsDesprendibles[itemElegido], itemsDesprendibles[itemElegido].transform.position, itemsDesprendibles[itemElegido].transform.rotation);
        return itemsDesprendibles[itemElegido];
    }
    #endregion
}
