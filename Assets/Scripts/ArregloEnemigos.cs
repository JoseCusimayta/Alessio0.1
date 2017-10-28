using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArregloEnemigos : MonoBehaviour {
	public GameObject[] enemigos;
	
	bool desactivarMuros;
	public GameObject pantallaFinal;
    private int contadorMuertes;
	// Use this for initialization
	void Start () {
        contadorMuertes = 0;

    }
	
	// Update is called once per frame
	void Update () {
        muereEnemigo();
        desactivar_Muros();

    }

	public void muereEnemigo() {
        contadorMuertes = 0;

        for (int i = 0; i < enemigos.Length; i++) {
			if (enemigos[i].GetComponentInChildren<Guardia>()==null) {
                contadorMuertes++;
               
                Debug.Log("muertes totales= "+ contadorMuertes);
				
			}
		}
        if (contadorMuertes== enemigos.Length)
        {
            Debug.Log("Se activa portal");
            desactivarMuros = true;
        }
	}

	public void desactivar_Muros() {
		if (desactivarMuros) {
			pantallaFinal.SetActive (true);

		}
	}

}
