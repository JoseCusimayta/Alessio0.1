using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionInventario : MonoBehaviour {
    public GameObject[] itemRapido;         //arreglo que almacena las imagenes de presentacion de los items de acceso rapido, se almacenan en casillas
    public Item[] items;                    //arreglo que almacena los objetos de tipo item que contienen las imagenes de presentacion y accion
    // Use this for initialization
    void Start () {
        items = new Item[itemRapido.Length];

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void asignarItems(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        Debug.Log("Asignando imagenes de presentacion y accion a los items");
        //se hace una busqueda entre los items
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("items[i]=" + items[i]);
            if (items[i] == null)
            {
                items[i] = new Item();
                items[i].cargarImagenes(ItemPresentacion, ItemAccion);
                break;
            }
        }
    }

    public void asignarItemACasilla(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        Debug.Log("Gestion Inventario accedi");
        asignarItems(ItemPresentacion,ItemAccion);
        //se hace una busqueda entre los casilleros, para encontrar alguno vacio
        for (int i = 0; i < itemRapido.Length; i++)                  
        {
            //el casillero vacio es aquel que tenga el sprite por defecto "UIMask"
            Debug.Log("nombre=" + itemRapido[i].GetComponent<Image>().sprite.name);
            if (itemRapido[i].GetComponent<Image>().sprite.name=="UIMask")
            {
                //si el casillero esta vacio se le asigna el item recogido

                itemRapido[i].GetComponent<Image>().sprite = items[i].presentacion;
                Debug.Log("nombre luego de seleccionar=" + itemRapido[i].GetComponent<Image>().sprite.name);
                break;
            }
        }
    }
}
