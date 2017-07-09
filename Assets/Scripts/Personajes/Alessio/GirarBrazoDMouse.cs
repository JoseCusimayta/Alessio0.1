using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class GirarBrazoDMouse : MonoBehaviour
{
    #region Variables
    public bool mirando_derecha;                                    //Variable para determinar si el personaje esta viendo a la derecha
    public float sensibilidad = 500;                                //Variable para determinar la sensibilidad del mouse
    public Transform punto_disparo;                                 //Variable para guardar el Transform del punto de disparo
    public Transform jugador;                                       //Variable para guardar el Transform del jugador
    #endregion
    
    #region Funciones de Unity

    void Start()
    {
    }


    void Update()
    {
        if (punto_disparo.position.x > jugador.position.x)          //Verificamos si esta mirando a la derecha con una resta de posiciones entre el jugador y el punto de disparo
            mirando_derecha = true;
        else
            mirando_derecha = false;

        if (mirando_derecha)                                        //Verificamos si esta mirando a la derecha para decirle que hacer
        {
            RotacionBrazoArmaDerecha();                             //Función para rotar el brazo cuando mira a la derecha
            CorrecionAngulosDerecha();                              //Función para asegurarse de que el brazo no salga de los angulos limites cuando mira a la derecha
        }
        else
        {
            RotacionBrazoArmaIzquierda();                           //Función para rotar el brazo cuando mira a la izquierda
            CorrecionAngulosIzquierda();                            //Función para asegurarse de que el brazo no salga de los angulos limites cuando mira a la izquierda
        }

    }
    #endregion

    #region Funciones Update
    void RotacionBrazoArmaIzquierda() 
    {
        if (0 >= transform.rotation.x && transform.rotation.x >= -0.7)                               //Verificamos entre que ángulos se encuentro la rotacion X del brazo
        {
            if ((Input.GetAxis("Mouse Y") > 0))                                                     //Verificamos que el mouse este desplazandose hacia arriba
            {
                punto_disparo.Rotate(new Vector3(0, 0, 1), sensibilidad * Time.deltaTime);          //Rotamos el punto de disparo hacia arriba
                transform.Rotate(new Vector3(0, 0, 1), sensibilidad * Time.deltaTime);              //Rotamos el brazo hacia arriba
            }
        }
        if (1 >= transform.rotation.y && transform.rotation.y >= 0.75)                               //Verificamos entre que ángulos se encuentro la rotacion Y del brazo
        {
            if (Input.GetAxis("Mouse Y") < 0)                                                       //Verificamos que el mouse este desplazandose hacia abajo
            {
                punto_disparo.Rotate(new Vector3(0, 0, -1), sensibilidad * Time.deltaTime);         //Rotamos el punto de disparo hacia abajo
                transform.Rotate(new Vector3(0, 0, -1), sensibilidad * Time.deltaTime);             //Rotamos el brazo hacia arriba
            }
        }
    }
    void RotacionBrazoArmaDerecha()
    {
        if (0 >= transform.rotation.z && transform.rotation.z >= -0.7)                               //Verificamos entre que ángulos se encuentro la rotacion Z del brazo
        {
            if ((Input.GetAxis("Mouse Y") > 0))                                                     //Verificamos que el mouse este desplazandose hacia arriba
            {
                punto_disparo.Rotate(new Vector3(0, 0, 1), sensibilidad * Time.deltaTime);          //Rotamos el punto de disparo hacia arriba
                transform.Rotate(new Vector3(0, 0, 1), sensibilidad * Time.deltaTime);              //Rotamos el brazo hacia arriba
            }
        }
        if (1 >= transform.rotation.w && transform.rotation.w >= 0.75)                               //Verificamos entre que ángulos se encuentro la rotacion W del brazo
        {
            if (Input.GetAxis("Mouse Y") < 0)                                                       //Verificamos que el mouse este desplazandose hacia abajo
            {
                punto_disparo.Rotate(new Vector3(0, 0, -1), sensibilidad * Time.deltaTime);         //Rotamos el punto de disparo hacia abajo
                transform.Rotate(new Vector3(0, 0, -1), sensibilidad * Time.deltaTime);             //Rotamos el brazo hacia arriba
            }
        }
    }
    void CorrecionAngulosIzquierda()
    {
        if (transform.rotation.x > 0)                                                               //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = Vector3.zero;                                              //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = new Vector3(0, 0, 90);                                 //Corregimos y le damos el angulo máximo o mínimo permitido
        }
        if (transform.rotation.x < -0.7)                                                            //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);                                    //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = Vector3.zero;                                          //Corregimos y le damos el angulo máximo o mínimo permitido
        }
        if (transform.rotation.y > 1)                                                               //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = Vector3.zero;                                              //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = new Vector3(0, 0, 90);                                 //Corregimos y le damos el angulo máximo o mínimo permitido
        }
        if (transform.rotation.y < 0.75)                                                            //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = new Vector3(0, 0, -79);                                    //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = Vector3.zero;                                          //Corregimos y le damos el angulo máximo o mínimo permitido
        }
    }
    void CorrecionAngulosDerecha()
    {
        if (transform.rotation.z > 0)                                                               //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = Vector3.zero;                                              //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = new Vector3(0, 0, 90);                                 //Corregimos y le damos el angulo máximo o mínimo permitido
        }
        if (transform.rotation.z < -0.7)                                                            //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = new Vector3(0, 0, -90);                                    //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = Vector3.zero;                                          //Corregimos y le damos el angulo máximo o mínimo permitido
        }
        if (transform.rotation.w > 1)                                                               //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = Vector3.zero;                                              //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = new Vector3(0, 0, 90);                                 //Corregimos y le damos el angulo máximo o mínimo permitido
        }
        if (transform.rotation.w < 0.75)                                                            //Verificamos que el angulo se haya salido del rango permitido
        {
            transform.localEulerAngles = new Vector3(0, 0, -79);                                    //Corregimos y le damos el angulo máximo o mínimo permitido
            punto_disparo.localEulerAngles = Vector3.zero;                                          //Corregimos y le damos el angulo máximo o mínimo permitido
        }

    }
    #endregion
}