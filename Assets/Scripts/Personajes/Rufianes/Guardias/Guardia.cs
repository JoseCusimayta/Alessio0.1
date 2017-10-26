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
	public string nombreRufian;						//nombre del rufian y de su tipo
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
    public string Tipo_Guardia;                                         //Elegir tipo de guardia, por ahora solo hay "Patrullaje" y "Reposo"
    public GameObject[] itemsDesprendibles;         //items que se despenden cuando muera el enemigo
    #endregion

    #endregion
	private float transparencia_objetivo;
	private GameObject sombrero;  
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
	private GameObject PieI;
	private GameObject Arma;



    #region Funciones de Unity
    // Use this for initialization
    void Start () {

		#region Asignando el cuerpo del Personaje
		Debug.Log("Nombre del rufian:"+this.gameObject.name);
		//sombrero = GameObject.Find("Rufian_Pistola/Cuerpo/Cabeza/Sombrero");
		sombrero = GameObject.Find(this.gameObject.name+"/Cuerpo/Cabeza/Sombrero");
		cuerpo = GameObject.Find(this.gameObject.name+"/Cuerpo");
		cabeza = GameObject.Find(this.gameObject.name+"/Cuerpo/Cabeza");        
		AnteBrazoD = GameObject.Find(this.gameObject.name+"/Cuerpo/AnteBrazoD");        
		BrazoD = GameObject.Find(this.gameObject.name+"/Cuerpo/AnteBrazoD/BrazoD");
		Arma = GameObject.Find(this.gameObject.name+"/Cuerpo/AnteBrazoD/BrazoD/Arma");
		AnteBrazoI = GameObject.Find(this.gameObject.name+"/Cuerpo/AnteBrazoI");
		BrazoI = GameObject.Find(this.gameObject.name+"/Cuerpo/AnteBrazoI/BrazoI");
		MusloD = GameObject.Find(this.gameObject.name+"/Cuerpo/cuerpoB/MusloD");
		PiernaD = GameObject.Find(this.gameObject.name+"/Cuerpo/cuerpoB/MusloD/PiernaD");
		PieD = GameObject.Find(this.gameObject.name+"/Cuerpo/cuerpoB/MusloD/PiernaD/PieD");
		MusloI = GameObject.Find(this.gameObject.name+"/Cuerpo/cuerpoB/MusloI");
		PiernaI = GameObject.Find(this.gameObject.name+"/Cuerpo/cuerpoB/MusloI/PiernaI");
		PieI = GameObject.Find(this.gameObject.name+"/Cuerpo/cuerpoB/MusloI/PiernaI/PieI");
		#endregion

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
		GestorVida();//Actualizamos la vida
//		if (gameObject!=null) {
//			GestorParpadeo();
//		}

		JugadorHerido();                                        //Función para gestionar todos los cambios que se implementan en las caracteristicas del jugador cuando es herido por un elemento del juego

	}   
    void OnTriggerEnter2D(Collider2D other)
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
		//Debug.Log("Posicion actual del jugador: "+player);
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
		Debug.Log("GestorAtaques() ");
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
                Debug.Log("Disparando a player!!");
                Instantiate(Prefab_Bala,
                    punto_disparo.transform.position,
                    punto_disparo.transform.rotation);          //Comienza a disparar desde el objeto vacio
                esta_atacando = true;                           //Activamos esta variable para decirle a la animación que hacer
            }
            else if (persiguiendo)
            {
				Debug.Log("Disparando a player!!");
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

        Instantiate(Prefab_Explosion,transform.position,transform.rotation);
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
	void JugadorHerido()
	{
		//Debug.Log ("Ella no te ama :'v 2 ");
		if (vida_actual < vida_anterior)                                    //Verificamos que haya un cambio en la vida
		{
			//Debug.Log ("Ella no te ama :'v 3 ");
			gameObject.layer = 12;                                          //Activamos la capa de invulnerabilidad
//			puede_controlar = false;                                        //Desactivmos el control del jugador
//			retrocediendo = true;                                           //Activamos la variable "retrocediendo" para la animación
//			retroceso = 2;                                                  //Le damos una velocidad al retroceso
//			if (salud._ultimoAtacante != null)                              //Verificamos que haya un atacante
//			{
//				if (transform.position.x <
//					salud._ultimoAtacante.transform.position.x)             //Verificamos la posición del atacante y del personaje
//				{
////					retroceder_derecha = false;                             //Le decimos de dónde nos está atacando para saber de donde retroceder
//				}
////				else retroceder_derecha = true;                             //Le decimos de dónde nos está atacando para saber de donde retroceder
//			}
			Invoke("RestaurarCapa", 0.5f);                                  //Reactivamos la capa del jugador en 0.5 segundos
            if (vida_actual <= 0) //Verificamos si el personaje está sin vida
            {
                Debug.Log("Muriendo");
                Morir();
                // Nuevo_Rufian();                                     //Creamos un nuevo Rufian
            }
        }
		vida_anterior = vida_actual;                                        //Actualizamos el dato de la vida anterior
	}

	void RestaurarCapa()
	{
		gameObject.layer = 8;                                               //Activamos la capa "Jugador" al personaje
	}
	void GestorParpadeo()
	{
		
		//Debug.Log ("Ella no te ama :'v");
		if (gameObject.layer == 12)                                         //Verificamos en que capa está (Layer 12: Capa de invulnerabilidad)
		{
			Color newColor = cabeza.GetComponent<SpriteRenderer>().color;   //Creamos una variable newColor para manejar la transparencia (alpha)
			newColor.a = Mathf.Lerp(newColor.a,
				transparencia_objetivo, Time.deltaTime * 20);               //Cambiamos el valor de la transparencia con el tiempo
			sombrero.GetComponent<SpriteRenderer>().color = newColor;
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
			Arma.GetComponent<SpriteRenderer>().color = newColor;
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
			newColor.a = 1;//Le decimos que se haga visible
			sombrero.GetComponent<SpriteRenderer>().color = newColor;
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
			Arma.GetComponent<SpriteRenderer>().color = newColor;
		}
	}

}
