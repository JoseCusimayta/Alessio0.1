using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour,IPersonaje {

	public float vida=100;			//La vida actual
	public string tipo_arma;	//Indica que arma se esta usando actualmente
	public float alerta_vida;	//Avisa al jugador que Alessio tiene poca vida
	public float velocidad=5;		//Velocidad normal de enemigo
	public float velocidadAlterada=5;		//Velocidad alterada por algun otro tipo de Alessio
	public float danio_golpe;	//Cantidad de daño que realizan los golpes de Alessio a los enemigos

    public GameObject Prefab_Bala, Prefab_Rufian, Prefab_Explosion, Empty_Rufianes;
    public Transform player;
    public Pistola pistolaEnemigo;
    float x, y, z;

    public bool reposo;		
	//truo  --> Reposo (El enemigo está quieto, si detecta a Alessio dispara, pero no lo persigue)
	//false --> Patrullaje (El enemigo patrulla, si detecta a Alessio dispara y pesigue hasta que salga de su rango)
	public bool alerta;
	//true  --> Persigue a Alessio y dispara hasta matarlo o morir
	//false --> Solo dispara y deja de disparar cuando Alessio sale del rango
	public float radio_Alcance;		//Perimetro de Vision: Si detecta a Alessio, dispara y/o persigue - Depende del reposo y Alerta

	private PatronMovimiento patronMovimiento;


	// Use this for initialization
	void Start () {
        reposo = true;
		patronMovimiento = GetComponent<PatronMovimiento> ();
        Coger();

    }
	
	// Update is called once per frame
	void Update () {
        Mover();
        Atacar();

    }

	public void Mover (){
        if (player != null && reposo==false)
        {
            //calculamos el vector entre la bala y el player
            Vector3 direccion = player.position - transform.position;

            //normalizamos el vector para que su longitud sea 1
            direccion.Normalize();

            //movemos el objeto usando el vector
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
    }

   public void Atacar(){
        if (pistolaEnemigo != null && reposo==false)
        {
            Invoke("Disparo",2 * Time.deltaTime);
           
        }
       
    }

	public void Morir(){
        Instantiate(Prefab_Explosion, transform.position, transform.rotation);
        Destroy(gameObject); //Destruir el objeto Rufian
    }

	public void Coger(){
        if (pistolaEnemigo != null)
        {
            Agarrar_Pistola();
        }

    }

	public void Saltar(){
	}

	public void Correr(){
	}

	public void Curar(){
	}

	public void Lanzar(){
	}

	public void Abrir(){
	}

    void Disparo()
    {
        Instantiate(Prefab_Bala, Empty_Rufianes.transform.position, Empty_Rufianes.transform.rotation); //Comienza a disparar desde el objeto vacio
    }

    public void Agarrar_Pistola() //Metodo para que Alessio pueda agarrar la pistola y cambiar de arma
    {
        pistolaEnemigo.transform.position = Empty_Rufianes.transform.position; //La pistola adopta la posición del objeto vacío
        pistolaEnemigo.transform.parent = Empty_Rufianes.transform.parent;   //La pistola y el objeto vacio comparten el mismo objeto padre = ALessio
        //Tipo_Arma = pistola.getPistola(); //Cambiamos el tipo de arma
        //ataque_Alessio.setAtaque_Alessio(Prefab_Bala, Prefab_Golpe, Empty_Alessio, Tipo_Arma);  //Le damos los datos al ataque  de Alessio
    }

    public void Nuevo_Rufian()
    {
        x = Random.Range(10f, 20f); //Posición del eje Y al azar, entre 10 y 20
        y = Random.Range(-4f, 5f); //Posición del eje X al azar, entre -4 y 5
        z = 0.0f; //Posición del eje Z en 0
        Vector3 vector3 = new Vector3(x, y, z); //Se crea un vector para guardar la posición en los ejes
        vida = 10; //Establecer cantidad de vida del rufian
        Instantiate(Prefab_Rufian, vector3, transform.rotation); //Crear un nuevo rufian con los anteriores valores 
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Se detecto algo");
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
