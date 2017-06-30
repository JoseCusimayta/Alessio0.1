using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPostNivel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //metodo que permite el cambio entre escenas
    public void cambiarEscena(string nivel)
    {
        SceneManager.LoadScene(nivel);
    }
}
