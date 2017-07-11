using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionInventario : MonoBehaviour {

    #region Variables
    public GameObject[] itemRapido;                              //arreglo que almacena las imagenes de presentacion de los items de acceso rapido, se almacenan en casillas
    public Item[] items;                                         //arreglo que almacena los objetos de tipo item que contienen las imagenes de presentacion y accion
    private Sprite porDefecto;                                  //sprite que contiene UIMask, la imagen por defecto
    private bool soloUnaVez;                                    //boleano para cargar solo una vez la imagen por defecto
    #endregion

    #region Funciones de Unity
    void Start () {
        items = new Item[itemRapido.Length];
    }
	

	void Update () {
		
	}
    #endregion

    #region Funciones para guardar items
    private void asignarItems(GameObject ItemPresentacion, Sprite ItemAccion)       //Función para guardar los items
    {
        Debug.Log("Asignando imagenes de presentacion y accion a los items");
        //se hace una busqueda entre los items
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("items["+i+"]=" + items[i]);
            if (items[i] == null)
            {
                Debug.Log("ItemPresentacion=" + ItemPresentacion);
                items[i] = new Item();
                items[i].cargarImagenes(ItemPresentacion, ItemAccion);
                Debug.Log("items[" + i + "]=" + items[i].presentacion);
                break;
            }
        }
    }
    //metodo que permite quitar del inventario al objeto
    public void desasignarItemACasilla(int i)
    {
        itemRapido[i].GetComponent<Image>().sprite = porDefecto;
    }

    //carga al sprite por defecto, para cuando necesitemos mostrar una casilla vacia
    private void cargarSpritePorDefecto(Sprite s)
    {
        if (!soloUnaVez)
        {
            porDefecto = s;
            soloUnaVez = true;
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
            //Debug.Log("nombre=" + itemRapido[i].GetComponent<Image>().sprite.name);
            if (itemRapido[i].GetComponent<Image>().sprite.name=="UIMask")
            {
                //cargamos el sprite por defecto (solo una vez)
                cargarSpritePorDefecto(itemRapido[i].GetComponent<Image>().sprite);
                //si el casillero esta vacio se le asigna el item recogido
                itemRapido[i].GetComponent<Image>().sprite = items[i].presentacion;
                //Debug.Log("nombre luego de seleccionar=" + itemRapido[i].GetComponent<Image>().sprite.name);
                break;
            }
        }
    }
    #endregion
}
