using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public Sprite presentacion; //es el sprite que aparece en la caja de inventario 
    public Sprite accion;       // es el sprite que aparece cuando el jugador manipula el script

	
    public void cargarImagenes(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        presentacion=ItemPresentacion.GetComponentInChildren<SpriteRenderer>().sprite;
        accion=ItemAccion;
    }
}
