using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Menu : MonoBehaviour {
	public string nivel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CambioDeEscena(){
	
		Application.LoadLevel ("Level1");

	}

	public void Salir(){
		Application.Quit ();
	}

}
