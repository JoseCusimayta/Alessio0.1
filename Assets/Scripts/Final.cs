using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour {
    public GameObject pantallaFinal;
    public GameObject jefe;
    private bool activar = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (jefe.GetComponentInChildren<Luissini>() == null && !activar)
        {
            Debug.Log("Se desactivo");
            Invoke("desactivarPantalla", 5);
            activar = true;
        }
        Debug.Log("activado="+activar);
    }

    public void desactivarPantalla()
    {
        pantallaFinal.SetActive(false);
    }
}
