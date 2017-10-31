using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaJefe : MonoBehaviour {
    private bool puertaCerrada;
    public GameObject[] jefes;
    private int contadorMuertes;
    public GameObject pantallaFinal;
    public GameObject puerta_cerrada;
    public GameObject Cubo;
    public GameObject prefab_explosion;
    private GameObject g = null;
    // Use this for initialization
    void Start () {
        puertaCerrada = true;
        contadorMuertes = 0;
    }
	
	// Update is called once per frame
	void Update () {
        muereJefe();
        desactivar_Muros();
    }

    public void muereJefe()
    {
        contadorMuertes = 0;

        for (int i = 0; i < jefes.Length; i++)
        {
            if (jefes[i] == null)
            {
                contadorMuertes++;
            }
         
        }

        if (contadorMuertes == jefes.Length)
        {
            Debug.Log("Se murieron todos lso jefes que habia");
            puertaCerrada = false;
        }
    }

    public void desactivar_Muros()
    {
        if (!puertaCerrada)
        {
            pantallaFinal.SetActive(true);
            Cubo.SetActive(false);
            //se instancia su explosion una sola vez desactivarse la puerta
            if (g == null)
            {
              g = Instantiate(prefab_explosion, pantallaFinal.transform.position, pantallaFinal.transform.rotation);
            }
           
            puerta_cerrada.SetActive(false);
            
        }
    }

    
}
