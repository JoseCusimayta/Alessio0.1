using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    #region Variables
    public float _vidaActual = 100;             //Variable para calcular la vida actual del objeto
    public float _vidaMaxima = 100;             //Varable para establecer una cantidad máxima a la vida
    public GameObject _ultimoAtacante;          //Variable para guardar el último objeto que lo golpeó
    #endregion

    #region Funciones de Unity
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Funcione de Alessio
    public void ModificarVida(float golpe, GameObject atacante)
    {
        _vidaActual -= golpe;
        if (_vidaActual > _vidaMaxima)
        {
            _vidaActual = _vidaMaxima;
        }
        if (_vidaActual < 0)
        {
            _vidaActual = 0;
        }
        _ultimoAtacante = atacante;
    }
    #endregion
}
