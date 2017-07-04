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
        for(int i = 0; i < itemRapido.Length; i++)
        {
            Debug.Log("nombre=" + itemRapido[i].GetComponent<Image>().sprite.name);
            if (itemRapido[i].GetComponent<Image>().sprite.name=="UIMask")
            {
                itemRapido[i].GetComponent<Image>().sprite = ga.GetComponentInChildren<SpriteRenderer>().sprite;
                break;
            }
        }
    }
}
