using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalirDeNivel : MonoBehaviour {
    private GameObject player;
    private GameObject Puntaje; //Variable para cargar el objeto que se encarga de gestionar los puntos
    private bool indicadorSalida; //variable que permite irse a otro nivel cuando cambia su valor a verdadero

    // Use this for initialization
    void Start() {
        player = GameObject.Find("Player");
        Puntaje = GameObject.Find("Puntaje");
    }

    // Update is called once per frame
    void Update() {
        if (indicadorSalida)
        {
            irseDeNivel();
        }
       

    }

    void irseDeNivel()
    {
        Debug.Log("Me voy...");
        PlayerPrefs.SetInt("puntajeJugador", Puntaje.GetComponent<GestionPuntuacion>().numeroPuntuacion);
        SceneManager.LoadScene("PantallaPostNivel");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colisiono con algo");
        if (other.gameObject.CompareTag("Player"))
        {
            indicadorSalida = true;
            player.GetComponent<Jugador>().puedeControlar = false;
            Debug.Log("Colisiono con player, proxima accion se ira del nivel");


        }
    }
}
