using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour {
    public static int Daño_Pistola = 3;
    public static string tipoArma = "Pistola";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string getPistola()
    {
        return tipoArma;
    }
}
