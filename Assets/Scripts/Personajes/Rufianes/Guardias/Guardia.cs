using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guardia : MonoBehaviour {
    #region Patron de Movimiento    
    public Transform[] lista_coordenadas; 		    //Variable para guardar la Cantidad de Coordenadas que se guardará
    public Transform coordenada_objetivo; 		    //Variable para guardar las Coordenadas del punto al cual se dirigirá
    public int indice_coordenada;                   //Variable para guardar el indice de la lista_coordenadas para conseguir las Coordenadas a la cual dirigirse
    public float velocidad_normal = 5;              //Variable para determinar la velocidad con la que se moverá el objeto
    #endregion
    // Use this for initialization
    void Start () {
        coordenada_objetivo = lista_coordenadas[indice_coordenada];     //Definimos cual será el primer punto a dirigirse para patrullar
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(lista_coordenadas[1].localPosition);
        Debug.Log(lista_coordenadas[1].position);
        transform.position = Vector3.MoveTowards(                        //Funcion para mover al objeto
            transform.position,	                                        //Definimos la posición del objeto
            coordenada_objetivo.position,			                            //Definimos la posición a la cual se dirigirá (punto destino)
            Time.deltaTime * velocidad_normal			                        //Definimos la velocidad de movimiento (caminar) para patrullar
        );
        if (transform.position == coordenada_objetivo.position)          //Verificamos si el objeto llego al punto destino
        {
            indice_coordenada += 1;                                             //Indicamos que ahora debe ir al siguiente punto
            if (indice_coordenada == lista_coordenadas.Length)                  //Verificamos si llego al último punto
            {
                indice_coordenada = 0;                                          //Regresamos al punto inicial
            }
            coordenada_objetivo = lista_coordenadas[indice_coordenada];         //Definimos las coordenadas del punto al cual se moverá el objeto
        }
    }
}
