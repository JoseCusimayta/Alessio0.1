using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {
    private bool inPause;
    public RectTransform panelPausa;
    // Use this for initialization
    void Start () {
        //ajuste inicial que permite que el panel de pausa solo aparesca cuando se le llame por evento
		panelPausa.localPosition = Vector3.one * 999;
		//panelPausa.localPosition = Vector3.zero;
    }


	// Update is called once per frame
	void Update () {
        //al pulsar la tecla escape se activa el menu pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = !inPause;

            //si esta en pausa, el juego se congela
            if (inPause)
            {
                Time.timeScale = 0;
                //local posicion es la posicion del objeto relativo a su padre
                //el panel de pausa es devuelto a su posicion original
                panelPausa.localPosition = Vector3.zero;
            }
            //si no esta en pausa el panel de pausa es movido
            else
            {
                Time.timeScale = 1;
                panelPausa.localPosition = Vector3.one * 999;
            }
        }

    }

    public void continuaJuego()
    {
        inPause = false;
        Time.timeScale = 1;
        panelPausa.localPosition = Vector3.one * 999;
    }

    public void salirJuego()
    {
        //Application.Quit();
		SceneManager.LoadScene ("MenuScene");
    }

    public void reiniciarJuego()
    {
        //carga escena actualmente cargada
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
