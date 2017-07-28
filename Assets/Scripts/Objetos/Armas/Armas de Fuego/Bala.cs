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
    private Rigidbody _rigidBody;           //Variable que permite todas las funciones fisicas para esta clase
    #endregion

    #region Funciones de Unity
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.velocity = transform.right * velocidad_bala; ;
        Invoke("morir", 10);
    }

    void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
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
