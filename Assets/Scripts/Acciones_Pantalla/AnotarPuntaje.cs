﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnotarPuntaje : MonoBehaviour
{
    #region Variables
    public int cantidad_puntaje = 100;                                      //game__object_puntaje por defecto
    public GameObject game_object_puntaje;                                        //Variable para cargar el objeto que se encarga de gestionar los puntos
    private GameObject item;
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
                numeroPuntuacion += cantidad_puntaje;

            //al llegar la vida del enemigo a menos que cero, se procede a matarlo, dejando a su paso un item para el jugador
            if (GetComponent<Enemigo>())
            {
                GetComponent<Enemigo>().Morir();
            }
            if (GetComponent<Guardia>())
            {
                GetComponent<Guardia>().Morir();
            }
			if (GetComponent<Chef>())
			{
				GetComponent<Chef>().Morir();
			}
			if (GetComponent<Chef>())
			{
				GetComponent<Luissini>().Morir();
			}
            //////////////////////
            //this.enabled = false;

        }
    }
    #endregion

}
