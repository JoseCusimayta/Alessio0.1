using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraJugador : MonoBehaviour {

    #region Variables
    public GameObject jugador;                                  //Variable para guardar el objeto  "Player"
    public Vector3 distancia_camara_jugador;                    //Variable para determinar la distancia entre el jugador y la cámara
    Record record;
    #endregion

    #region Start & Update
    void Start()
    {        
        distancia_camara_jugador =
            transform.position - jugador.transform.position;    //Definimos que la distancia entre la cámara y el jugador será la que está en la escena
    }

    void Update()
    {
        if (jugador)                                            //Verificamos si el "Player" existe para que la cámara persiga al jugador
        {
            transform.position =                                //Asignamos la posición de la cámara en cada frame para perseguir al jugador
                jugador.transform.position +                    //Calculamos la posición actual del jugador y...
                distancia_camara_jugador;                       //Sumamos la distancia que asignamos
        }
    }
    #endregion
}
