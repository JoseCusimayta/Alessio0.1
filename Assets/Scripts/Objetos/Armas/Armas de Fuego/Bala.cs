using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    #region Variables
    public float danio_bala=20;                //La cantidad de vida que reducira al enemigo
    public float velocidad_bala = 15f;        //La velocidad con la que se moverá la bala
    public string Target_Tag = "Enemigo";    // Se encarga de marcar a que tipo de elemento le causara daño la bala
    public GameObject Prefab_Explosion;     //Permite generar una explosion al colisionar
    private Rigidbody2D _rigidBody;           //Variable que permite todas las funciones fisicas para esta clase
	public string tipoArma;					//variable para determinar que tipo de arna se usa.
    #endregion

    #region Funciones de Unity
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
		switch( tipoArma){
		case "Pistola":
			velocidad_bala = 15f;
			break;
		case "Ametralladora":
			velocidad_bala = 40f;
			break;
		}
        _rigidBody.velocity = transform.right * velocidad_bala; ;
        Invoke("morir", 10);
    }

    void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Target_Tag))
        {
            morir();
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject); 
    }
    #endregion

    #region Funciones
    public void morir()
    {
        Destroy(gameObject);
    }
    #endregion

}
