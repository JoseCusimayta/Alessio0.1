using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManMovement : MonoBehaviour
{
    #region Variables
    public float speedPlayer = 10;                      //Velocidad del Jugador
    public float speedPlayerArmed = 5;                  //Velocidad del Jugador cuando tiene un arma
    public float axisH;                                 //Sirve para medir el valor horizontal del Axis
    public float axisV;                                 //Sirve para medir el valor vertical del Axis
    public SpriteRenderer _spriteRendererPlayer;        //Variable para guardar el Sprite del Jugador
    public Animator _animatorPlayer;                    //Variable para guardar el Animator del Jugador
    public Transform armaPlayer;                        //Variable para guardar el Transform del objeto que sirve para posicionar el arma al agarrar
    public SpriteRenderer _spriteRendererObject;        //Variable para guardar el Sprite del Arma para hacerle Flip, si es necesario
    public Transform _transformObject;                  //Variable para guardar el Transform del Arma
    public bool GetWeapon;                              //Variable para saber si el personaje tiene o no un arma
    public CatchingWeapon _catchingWeapon;              //Variable para guardar el Script del Arma "CatchingWeapon"
    public bool _SeeingRight;                           //Variable para saber si el personaje está viendo a la derecha o no
    public bool _isReleasing;                           //Variable para saber si el personaje está soltando el arma
    #endregion

    #region Funciones de Unity
    void Start()
    {
        //Inicializamos las variables, llenandolos con los componentes de jugador
        _spriteRendererPlayer = GetComponentInChildren<SpriteRenderer>();
        _animatorPlayer = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        //Llamamos a las funciones necesarias
        ManageFlipping();
        ManageAnimations();
        ReciveInputs();
    }

    void FixedUpdate()
    {
        //Llamamos a las funciones necesarias
        _Movimiento();
    }

    void OnTriggerEnter(Collider other)
    {
        //Primero verificamos con que hemos colisionado
        if (other.tag == "Pistola")
        {
            //Primero verificamos que no este soltando un arma
            //El cual será cambiado a falso en la animación "FinishReleasing"
            if (!_isReleasing)
            {
                //Luegeo guardamos el script correspondiente
                _catchingWeapon = other.GetComponent<CatchingWeapon>();
                //Después llamamos a la función CatchWeapon del Script
                _catchingWeapon.CatchWeapon(armaPlayer);
                //Luego le decimos al script que el jugador tiene un arma
                GetWeapon = true;
            }
        }
    }
    #endregion

    #region Funciones de Yiwo
    public void ReciveInputs()
    {
        axisH = Input.GetAxis("Horizontal");                    //llamamos al Axis horizontal       
        axisV = Input.GetAxis("Vertical");                      //llamamos al Axis vertical       
        //Ahora si es que lleva un arma, debe poder soltarlo
        //Por ello, primero verificamos si tiene el arma
        if (GetWeapon)
        {
            //Luego verificamos que presione la tecla de lanzar el arma
            bool KeyE = Input.GetKeyDown(KeyCode.E);            
            if (KeyE)
            {
                //Le decimos al Script que está soltando un arma, por lo tanto no puede agarrar otra arma hasta que la suelte por completo
                _isReleasing = true;    
                //Si es que lo ha presionado, llamamos a la animación de lanzar el arma
                _animatorPlayer.SetTrigger("DropWeapon");
                //Y en la animación se ejecutará la acción de solar el arma
            }
            //También podemos verificar si es que quiere disparar
            bool Disparar = Input.GetMouseButtonDown(0);
            if (Disparar)
            {
                //Si quiere disparar, le decimos al script del arma que dispare
                _catchingWeapon.Disprar();
            }
        }
    }
    public void _Movimiento()
    {
        Vector2 moveVector = new Vector2(0, 0);                 //Creamos un nuevo Vector2 para manipular la velocidad                
        if (GetWeapon)
        {
            //Si esta armado usaremos la velocidad de cuando el personaje está armado
            moveVector.x = Mathf.Abs(axisH) * speedPlayerArmed;         //modificamos el valor X del Vector2 anteriormente creado, según el Axis
            moveVector.y = axisV * speedPlayerArmed;
        }
        else
        {
            //En caso de no estar armado, usaremos la velocidad de cuando el personaje no está armado
            moveVector.x = Mathf.Abs(axisH) * speedPlayer;         //modificamos el valor X del Vector2 anteriormente creado, según el Axis
            moveVector.y = axisV * speedPlayer;
        }
        transform.Translate(moveVector * Time.deltaTime);       //Movemos al objeto en función del nuevo Vector
    }
    void ManageFlipping()
    {
        //Manejamos el giro de Jugador y para saber si esta yendo a la derecha o izquierda usamos el valor obtenido por el Axis Horizontal
        if (axisH < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);      //Si H es menor a 0, hacemos que el objeto mire a la derecha            
            _SeeingRight = false;
        }
        if (axisH > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);	    //Si H es mayor a 0, hacemos que el objeto mire a la izquierda
            _SeeingRight = true;
        }
        if (GetWeapon)
            _catchingWeapon.ManageFlipping(_SeeingRight);
    }
    
    void ManageAnimations()
    {
        //Manipulamos las variables del animator para sus respectivas animaciones
        _animatorPlayer.SetFloat("Movement", Mathf.Abs(axisH)+Mathf.Abs(axisV));                             //Le pasamos el valor de Axis para el movimiento
        _animatorPlayer.SetBool("GetWeapon", GetWeapon);                                    //Le decimos que es cierto que tiene un arma
        
    }
    public void DropWeapon()
    {
        GetWeapon = false;
        _catchingWeapon.DropWeapon();
    }
   
    #endregion
}