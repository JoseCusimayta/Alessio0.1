using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
	public float danio_bala;				//La cantidad de vida que reducira al enemigo
	public float velocidad_bala=15f;        //La velocidad con la que se moverá la bala
    public string Target_Tag="Enemigo";
    public GameObject Prefab_Explosion;
    private Rigidbody _rigidBody;

    #region Start & Update
    void Start()
    {
        transform.Rotate(0, 180, 0);
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
