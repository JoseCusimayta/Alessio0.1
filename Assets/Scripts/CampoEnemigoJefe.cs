using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampoEnemigoJefe : MonoBehaviour
{

	#region Variables
	private Enemigo Script_Enemigo;                              //Variable para manejar el script del Enemigo
	private Guardia Script_Guardia;
	private Chef Script_Chef;
	private Luissini Script_luissini;
	public bool personaje_detectado;                            //Variable para saber si se ha detectado al jugador
	public float velocidad_asignada;                            //Variable para asignar la velocidad con la que se moverá el objeto (retroceder, o avanzar)
	#endregion

	#region Funciones de Unity
	void Start()
	{
		Script_Enemigo = GetComponentInParent<Enemigo>();       //Definimos el Script que usaremos del enemigo
		Script_Guardia = GetComponentInParent<Guardia>();       //Definimos el Script que usaremos del enemigo
		Script_Chef = GetComponentInParent<Chef>(); 
		Script_luissini = GetComponentInParent<Luissini>();  
	}

	// Update is called once per frame
	void Update()
	{
		LlamarCuchillosElbrayan ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))                                 //Verificamos si el personaje se acerca
		{
			personaje_detectado = true;                                 //Activamos la variable para decir que se ha detectado al personaje
			if (Script_Enemigo)
			{
				Script_Enemigo.velocidad_normal = velocidad_asignada;       //Asignamos la velocidad cambiada al objeto Enemigo
			}
			if (Script_Guardia)
			{
				Script_Guardia.velocidad_normal = velocidad_asignada;       //Asignamos la velocidad cambiada al objeto Enemigo
			}
		}

		if (other.CompareTag("Player") && !Script_luissini.activarCuchillo) {
			Debug.Log("Alession entro al area");
			Script_luissini.activarCuchillo = true;
			//_animacion.SetTrigger ("Cuchillo");
			Invoke ("LanzaCuchillosElbrayan",3);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))                                 //Verificamos si el personaje se aleja
		{
			personaje_detectado = false;                                //Desactivamos la variable para decir que se ha detectado al personaje
			if (Script_Enemigo)
			{
				Script_Enemigo.velocidad_normal = velocidad_asignada;       //Asignamos la velocidad cambiada al objeto Enemigo
			}
			if (Script_Guardia)
			{
				Script_Guardia.velocidad_normal = velocidad_asignada;       //Asignamos la velocidad cambiada al objeto Enemigo
			}
		}
	}
	#endregion

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player") && !Script_luissini.activarCuchillo) {
			Debug.Log("Alession entro al area");
			Script_luissini.activarCuchillo = true;
			//_animacion.SetTrigger ("Cuchillo");
			Invoke ("LlamarCuchillosElbrayan",3);

		}

	}

	void LlamarCuchillosElbrayan(){

		Script_luissini.LanzaCuchillosElbrayan ();

	}
}
