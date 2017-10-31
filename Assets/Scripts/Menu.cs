using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	

	public void CambioDeEscena(string nombre){

        SceneManager.LoadScene (nombre);

	}

	public void Salir(){
		Application.Quit ();
	}

}
