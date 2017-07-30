using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    #region Variables
    public Sprite presentacion;                                 //Es el sprite que aparece en la caja de inventario 
    public Sprite accion;                                       //Es el sprite que aparece cuando el jugador manipula el script
    private int stock;                                          //variable que permite contar la cantidad actual del item, no aplicable en pistolas
    public GameObject objeto;
    #endregion

    #region Funciones para guardar las imágenes
    public void cargarImagenes(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        objeto = new GameObject();
        //si es sanacion se recrea el objeto 
        Debug.Log("ItemPresentacion.name=" + ItemPresentacion.name);
        Debug.Log("ItemPresentacion.accion=" + ItemAccion.name);
        switch (ItemPresentacion.name)
        {
            case "Curacion":
                ItemPresentacion.GetComponentInChildren<SpriteRenderer>().flipX = true;
                objeto.AddComponent<Sanacion>();
                objeto.name = ItemPresentacion.name;
                stock = 1;
            break;
            case "Ametralladora": 
                objeto.AddComponent<Pistola>();
                objeto.GetComponent<Pistola>().setPistola("Ametralladora");
                objeto.name = ItemPresentacion.name;
                stock = 30;
           break;
            case "Pistola":
                objeto.AddComponent<Pistola>();
                objeto.GetComponent<Pistola>().setPistola("Pistola");
                objeto.name = ItemPresentacion.name;
                stock = 6;
                break;
        }
        
        Debug.Log("objecto=" + objeto);
        presentacion =ItemPresentacion.GetComponentInChildren<SpriteRenderer>().sprite;
        accion=ItemAccion;
        
    }

    public int getStock()
    {
        return stock;
    }
    #endregion
}
