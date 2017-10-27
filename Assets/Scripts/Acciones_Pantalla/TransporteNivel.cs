using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransporteNivel : MonoBehaviour {
	
	private GestionPuntuacion _score;
	private GameObject player;
	public string objetivo="Player";
	public float coorX;
	public float coorY;
	public float coorCamaraX;
	public float coorCamaraY;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		_score = GameObject.Find ("Puntaje").GetComponent<GestionPuntuacion>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void irseDeNivel()
	{
		PlayerPrefs.SetInt ("playerScore", _score.numeroPuntuacion);
		SceneManager.LoadScene ("winScreen");
	}


	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Colisiono con algo");
		if (other.gameObject.CompareTag(objetivo)) {
//			float x = 670.3538f;
//			float y = -77.34947f;
			player.transform.position = new Vector2 (coorX, coorY);
			Camera.main.GetComponent<CamaraJugador> ().enabled = false;
			Camera.main.transform.position=new Vector3 (coorCamaraX,coorCamaraY,-10f); 
			Debug.Log ("Colisiono con player");


		}
	}
}
