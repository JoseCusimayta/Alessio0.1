using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    #region Variables
    public float danio_bala;                //La cantidad de vida que reducira al enemigo
    public float velocidad_bala = 15f;        //La velocidad con la que se moverá la bala
    public string Target_Tag = "Enemigo";    // Se encarga de marcar a que tipo de elemento le causara daño la bala
    public GameObject Prefab_Explosion;     //Permite generar una explosion al colisionar
    private Rigidbody _rigidBody;           //Variable que permite todas las funciones fisicas para esta clase
    #endregion

    #region Start & Update
    void Start()
    {
        //dependiendo a quien se quiere dañar, la trayectoria cambiara
        if (Target_Tag == "Enemigo")
        {
            transform.Rotate(0, 0, 0);
        }
        else transform.Rotate(0, 180, 0);
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.velocity = transform.right * velocidad_bala; ;
        //Debug.Log("_rigidBody.velocity ="+ _rigidBody.velocity);
        Invoke("morir", 10);
    }

    void Update()
    {
        //_rigidBody.velocity = new Vector3(velocidad_bala * Time.deltaTime, 0, 0);
        //transform.Translate(velocidad_bala * Time.deltaTime, 0, 0);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Target_Tag))
        {
            morir(); //Destruir bala
        }

    }

    public void morir()
    {
        Destroy(gameObject);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject); //Destruye el objeto cuando sale de la camara
    }

}
