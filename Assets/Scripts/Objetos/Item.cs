using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    #region Variables
    public Sprite presentacion;                                 //Es el sprite que aparece en la caja de inventario 
    public Sprite accion;                                       //Es el sprite que aparece cuando el jugador manipula el script
    public GameObject objeto;
    #endregion

    #region Funciones para guardar las imágenes
    public void cargarImagenes(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        objeto = new GameObject();
        //si es sanacion se recrea el objeto 
        switch (ItemPresentacion.name)
        {
            case "Curacion":
                objeto.AddComponent<Sanacion>();
                objeto.name = ItemPresentacion.name;
            break;
            case "Ametralladora": 
                 objeto.AddComponent<Pistola>();
                 objeto.name = ItemPresentacion.name;
           break;
        }
        
        Debug.Log("objecto=" + objeto);
        presentacion =ItemPresentacion.GetComponentInChildren<SpriteRenderer>().sprite;
        accion=ItemAccion;
        
    }
    #endregion
}
