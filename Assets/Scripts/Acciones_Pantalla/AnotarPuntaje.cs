using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotarPuntaje : MonoBehaviour
{
    #region Variables
    public int cantidad_puntaje = 100;                                      //game__object_puntaje por defecto
    public GameObject game_object_puntaje;                                        //Variable para cargar el objeto que se encarga de gestionar los puntos
    
    #endregion
    
    #region Funciones de Unity
    void Start()
    {
        game_object_puntaje = GameObject.Find("Puntaje");                  //Se busca al objeto "Puntaje"
    }

    void Update()
    {
        if (GetComponent<Health>()._vidaActual <= 0)                        //Verificamos que el objeto no tenga vida
        {
            game_object_puntaje.GetComponent<GestionPuntuacion>().          //Obtenemos el Script de "GestionPuntuacion"
                numeroPuntuacion += cantidad_puntaje;                       //Agregamos la puntuación del objeto a la puntuación del personaje
            ///////////////////
            //
            Destroy(gameObject); //Destruir el objeto Rufian
            //////////////////////
            this.enabled = false;

        }
    }
    #endregion

}
