using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicarSprites : MonoBehaviour {
    public Transform Objeto;
    public int RepeticionesX;
    public int RepeticionesY;
    public float DistanciaX;
    public float DistanciaY;
    // Use this for initialization
    void Start () {
		for ( int x=0; x < RepeticionesX; x++)
        {
            for(int y = 0; y < RepeticionesY; y++)
            {
                Transform instancia;
                instancia = Instantiate(Objeto, new Vector3(x * DistanciaX, y * DistanciaY, 0), Quaternion.identity) as Transform;
                instancia.parent = this.transform;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
