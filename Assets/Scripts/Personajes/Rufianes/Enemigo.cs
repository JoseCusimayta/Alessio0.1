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

    public GameObject Prefab_Bala, Prefab_Rufian, Prefab_Explosion;
    public float frecDisparo = 1f;
    public Transform Empty_Rufianes;
    float x, y, z;
    public GameObject player;
    float  velocidad_rufian = 1;
    bool Alessio_Detectado = false;
    float Intervalo_Ataque = 0;
    Pistola pistolaEnemigo;

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
		patronMovimiento = GetComponent<PatronMovimiento> ();
        
    }
	
	// Update is called once per frame
	void Update () {
        DetectarAlessio();

    }

	public void Mover (){
	}

	public void Atacar(){
        if (pistolaEnemigo != null)
        {
            Instantiate(Prefab_Bala, Prefab_Rufian.transform.position, Prefab_Rufian.transform.rotation); //y se crea el objeto dependiendo del arma, en este caso, una bala
            
        }
       
    }

	public void Morir(){
        Instantiate(Prefab_Explosion, transform.position, transform.rotation);
        Destroy(gameObject); //Destruir el objeto Rufian
    }

	public void Coger(){
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
        Instantiate(Prefab_Bala, Empty_Rufianes.position, Empty_Rufianes.rotation); //Comienza a disparar desde el objeto vacio
    }
    void DetectarAlessio()
    {
        if (player != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 10)
            {
                transform.position = Vector2.Lerp(transform.position, player.transform.position, velocidad_rufian * Time.deltaTime);
                Intervalo_Ataque -= Time.deltaTime;
                if (Intervalo_Ataque <= 0)
                {
                    Atacar();
                    Intervalo_Ataque = 0.2f;
                }
            }
        }
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
