using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objeto : MonoBehaviour {
    public GameObject[] itemsDesprendibles;         //items que se desprende cuando el objeto es roto
    public Health salud;                            //Variable para obtener la clase Health y controlar la salud
    public float vida_actual;                       //Variable para guardar la cantidad de vida actual    
    public LayerMask mascara_objeto;                //Variable para guardar y modificar la máscara del objeto
    private AudioSource sonido;                      //variable para el sonido de la lata al ser impactada

    // Use this for initialization
    void Start () {
        salud = GetComponent<Health>();
        sonido = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if(gameObject.layer != 12)
        {
            GestorVida();
        }
        

    }

    public void GestorVida()
    {

        vida_actual = salud._vidaActual;
        //Debug.Log("Vida enemigo:" + vida_actual);//Actualizamos la vida del objeto
        if (vida_actual <= 0) //Verificamos si el tacho se quedo sin vida y que ya no fue volteado alguna vez
        {
            VoltearTacho();
            //Morir();
            // Nuevo_Rufian();                                     //Creamos un nuevo Rufian
        }
    }

    public void DesprenderItem()
    {
        //GetComponent<MeshRenderer>().enabled = false;
        Debug.Log("Desprendera un item al morir");
        //el item se elige aleatoriamente
        int itemElegido = Random.Range(0, itemsDesprendibles.Length - 1);
        Debug.Log("itemElegido=" + itemElegido);
        itemsDesprendibles[itemElegido].transform.position = gameObject.transform.position;
        itemsDesprendibles[itemElegido].transform.rotation = gameObject.transform.rotation;

        //gameObject.SetActive(false);
        Instantiate(itemsDesprendibles[itemElegido], itemsDesprendibles[itemElegido].transform.position, itemsDesprendibles[itemElegido].transform.rotation);
       // return itemsDesprendibles[itemElegido];
    }

    void VoltearTacho()
    {
        vida_actual = salud._vidaMaxima;
        transform.Rotate(0, 0, 90);
        gameObject.layer = 12; //luego de que se voltee, el tacho es invulnerable
        DesprenderItem();
        sonido.Play();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Se cruzo");
        if (other.CompareTag("Golpe") && gameObject.layer!=12)                   //Verificamos que el objeto colisionado sea un arma (Tag: Arma)            
        {
            Debug.Log("Impacto de alessio");                  //Función para detectar el arma con el que ha colisionado
            salud.ModificarVida(other.GetComponentInParent<Jugador>().danio_golpe, other.gameObject);
        }
        if((other.CompareTag("BalaPlayer") || other.CompareTag("ObjetoHiriente")) && gameObject.layer != 12)
        {
            VoltearTacho();
        }

    }

    
}
