using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionInventario : MonoBehaviour {
    public GameObject[] itemRapido;         //arreglo que almacena los items de acceso rapido, se almacenan en casillas

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void asignarItemACasilla(GameObject ga)
    {
        Debug.Log("Gestion Inventario accedi");
        //se hace una busqueda entre los casilleros
        for(int i = 0; i < itemRapido.Length; i++)                  
        {
            //el casillero vacio es aquel que tenga el sprite por defecto "UIMask"
            Debug.Log("nombre=" + itemRapido[i].GetComponent<Image>().sprite.name);
            if (itemRapido[i].GetComponent<Image>().sprite.name=="UIMask")
            {
                //si el casillero esta vacio se le asigna el item recogido
                itemRapido[i].GetComponent<Image>().sprite = ga.GetComponentInChildren<SpriteRenderer>().sprite;
                Debug.Log("nombre luego de seleccionar=" + itemRapido[i].GetComponent<Image>().sprite.name);
                break;
            }
        }
    }
}
