using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour {
    public static int Daño_Pistola = 3;
    private string tipoArma = "Pistola";
    public int cantidadBalas = 6;
    public int balasExtra = 0;
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

    //permite reducir la cantidad de balas disponibles cuando disparamos
    public void dispararBalaArma()
    {
        if (cantidadBalas > 0)
        {
            cantidadBalas -= 1;
        }
        
    }

    //añadimos mas municion al arma
    public void abastecerArma()
    {
        balasExtra += TotalBalas;

    }

    //recargaremos el arma con las municiones de las balas extra
    public void recargarArma()
    {
        if (balasExtra > 0)
        {
            cantidadBalas += TotalBalas;
            balasExtra -= TotalBalas;
        }
        

    }


    public void setBala(int bala)
	{
		cantidadBalas -= bala;
	}
    
}
