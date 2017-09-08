using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Menu : MonoBehaviour {
	

	public void CambioDeEscena(string nombre){
	
		EditorSceneManager.LoadScene (nombre);

	}

	public void Salir(){
		Application.Quit ();
	}

}
