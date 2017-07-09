using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public Sprite presentacion; //es el sprite que aparece en la caja de inventario 
    public Sprite accion;       // es el sprite que aparece cuando el jugador manipula el script
    public GameObject objeto;
	
    public void cargarImagenes(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        objeto = ItemPresentacion;
        presentacion =ItemPresentacion.GetComponentInChildren<SpriteRenderer>().sprite;
        accion=ItemAccion;
        Debug.Log("objecto=" + objeto);
    }
}
