using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoEnemigo : MonoBehaviour
{

    #region Variables
    public Enemigo Script_Enemigo;                             //Variable para manejar el script del Enemigo
    public bool personaje_detectado;                            //Variable para saber si se ha detectado al jugador
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
        if (other.CompareTag("Player"))
        {
            personaje_detectado = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            personaje_detectado = false;
        }
    }
    #endregion
}