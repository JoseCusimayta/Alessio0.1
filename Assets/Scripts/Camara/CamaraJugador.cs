using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraJugador : MonoBehaviour
{

    #region Variables
    public Transform jugador;                                  //Variable para guardar el objeto  "Player"    
    public float distanciaX;                                    //Variable para definir la distancia en el eje X, con respecto al "Player"
    public float distanciaY;                                    //Variable para definir la distancia en el eje Y, con respecto al "Player"
    Record record;
    #endregion

    #region Start & Update
    void Start()
    {
        jugador = GameObject.FindWithTag("Player").GetComponent<Transform>();        
        transform.position =
            jugador.position +
            new Vector3(distanciaX, distanciaY, -10);             // Le damos la posicion de la camara, con respecto al jugador, agregandole la distancia que hemos decidido        
    }

    void Update()
    {
        if (jugador)
            transform.position = jugador.position + new Vector3(distanciaX, distanciaY, -10);
    }
    #endregion
}