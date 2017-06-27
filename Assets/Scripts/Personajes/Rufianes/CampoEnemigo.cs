using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoEnemigo : MonoBehaviour {
    private Enemigo padre;
    public float speedCampo = 0.1f;
    // Use this for initialization
    void Start()
    {
        padre = GetComponentInParent<Enemigo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Entro al campo de fuerza");
        if (other.CompareTag("Player"))
        {
            padre.velocidad = speedCampo;
            padre.reposo = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("Salio de campo de fuerza");
        if (other.CompareTag("Player"))
        {
            padre.velocidad = 0;
            padre.reposo = true;
        }
    }
}
