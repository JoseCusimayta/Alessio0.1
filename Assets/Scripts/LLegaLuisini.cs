using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLegaLuisini : MonoBehaviour {
	public GameObject Mensaje;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Colisiono con algo");
		if (other.CompareTag("Player")) {
			
			Mensaje.SetActive (true);
		}
	}
	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Colisiono con algo");
		if (other.CompareTag("Player")) {

			Mensaje.SetActive (false);
		}
	}
}
