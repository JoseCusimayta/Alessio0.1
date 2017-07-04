using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class GirarBrazoDMouse : MonoBehaviour
{
    #region Variables
    SpriteRenderer _sprite;
    public bool mirando_derecha;
    public Transform punto_disparo;
    public Transform jugador;
    #endregion

    #region Funciones de Unity
    
	void Start () {
        _sprite = GetComponent<SpriteRenderer>();
	}


    void Update()
    {

        if (punto_disparo.position.x > jugador.position.x)
            mirando_derecha = true;
        else
            mirando_derecha = false;

        if (mirando_derecha)
        {
            if (transform.rotation.z < 0 && transform.rotation.z > -1)
            {
                if ((Input.GetAxis("Mouse Y") > 0))
                {
                    punto_disparo.Rotate(new Vector3(0, 0, 1), 200 * Time.deltaTime);
                    transform.Rotate(new Vector3(0, 0, 1), 200 * Time.deltaTime);
                }
            }
            if (transform.rotation.w > 0.8 && transform.rotation.w < 1)
            {
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    punto_disparo.Rotate(new Vector3(0, 0, -1), 200 * Time.deltaTime);
                    transform.Rotate(new Vector3(0, 0, -1), 200 * Time.deltaTime);
                }
            }
        }
        else
        {
            if (transform.rotation.x < 0 && transform.rotation.x > -1)
            {
                Debug.Log(transform.rotation.x);
                Debug.Log(transform.rotation.y);
                if ((Input.GetAxis("Mouse Y") > 0))
                {
                    punto_disparo.Rotate(new Vector3(0, 0, 1), 200 * Time.deltaTime);
                    transform.Rotate(new Vector3(0, 0, 1), 200 * Time.deltaTime);
                }
            }
            if (transform.rotation.y > 0.5 && transform.rotation.y < 1)
            {
                if (Input.GetAxis("Mouse Y") < 0)
                {
                    punto_disparo.Rotate(new Vector3(0, 0, -1), 200 * Time.deltaTime);
                    transform.Rotate(new Vector3(0, 0, -1), 200 * Time.deltaTime);
                }
            }
        }
        /*
        Vector3 direccion_movimiento = punto_disparo.position - Input.mousePosition;
        direccion_movimiento.Normalize();
        transform.rotation = new Quaternion(-direccion_movimiento.x, -direccion_movimiento.y, 0, 0);
        Debug.Log(direccion_movimiento);*/
    }
    #endregion
}
