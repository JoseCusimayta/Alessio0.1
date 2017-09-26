using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuchillo : MonoBehaviour {
    public float danio_cuchillo = 20;                //La cantidad de vida que reducira al enemigo
    public string Target_Tag = "Player";    // Se encarga de marcar a que tipo de elemento le causara daño el cuchillo
                                             // Use this for initialization
    void Start () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Target_Tag))
        {
            morir();
        }

    }

    public void morir()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        //Destroy(gameObject);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
