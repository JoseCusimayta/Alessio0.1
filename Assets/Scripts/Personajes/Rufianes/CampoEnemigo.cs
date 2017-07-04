using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoEnemigo : MonoBehaviour
{

    #region Variables
    public Enemigo Script_Enemigo;                             //Variable para manejar el script del Enemigo
    public bool personaje_detectado;                           //Variable para saber si se ha detectado al jugador
    #endregion

    #region Funciones de Unity
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))                         //Verificamos si el personaje se acerca
        {
            personaje_detectado = true;                         //Activamos la variable para decir que se ha detectado al personaje
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))                         //Verificamos si el personaje se aleja
        {
            personaje_detectado = false;                        //Desactivamos la variable para decir que se ha detectado al personaje
        }
    }
    #endregion
}