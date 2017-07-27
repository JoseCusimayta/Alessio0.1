using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour {
    public static int Daño_Pistola = 3;
    private string tipoArma = "Pistola";
    public int cantidadBalas = 6;
    public int TotalBalas = 6;

    // Use this for tipoArma
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setPistola(string tipo)
    {
        tipoArma = tipo;
        switch (tipoArma)
        {
            case "Ametralladora":
                cantidadBalas = 30;
                TotalBalas = 30;
                break;
            case "Pistola":
                cantidadBalas = 6;
                TotalBalas = 6;
                break;
        }


    }
    public string getPistola()
    {
        return tipoArma;
    }

    
}
