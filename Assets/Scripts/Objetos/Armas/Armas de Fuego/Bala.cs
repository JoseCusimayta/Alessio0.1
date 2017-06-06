using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour {
	public float daño_bala;				//La cantidad de vida que reducira al enemigo
	public float velocidad_bala=15f;        //La velocidad con la que se moverá la bala
    public string Target_Tag="Enemigo";
    public GameObject Prefab_Explosion;
    

    #region Start & Update
    void Start()
    {
        Invoke("morir", 10);
    }

    void Update()
    {
        transform.Translate(velocidad_bala * Time.deltaTime, 0, 0);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Target_Tag))
        {
            //if (Target_Tag == "Rufianes")
            //{
                
            //    RufianesAI rufianesAI = other.gameObject.GetComponent<RufianesAI>();        //Instanciamos la clase RufianesAI            
            //    rufianesAI.Vida_Rufianes = rufianesAI.Vida_Rufianes - Pistola.Daño_Pistola;  //Se resta la vida del rufian - el daño de la pistola
            //    if (rufianesAI.Vida_Rufianes <= 0)
            //    {
            //        rufianesAI.morir();
            //        Record.Score++; //Aumentamos en 1 el record
            //    }

               
            //}
            //if (Target_Tag == "Player")
            //{
                
            //    Alessio player = other.GetComponent<Alessio>();
            //    Record.Lives = Record.Lives - Pistola.Daño_Pistola; //Restamos la vida del jugador - el daño de la pistola
            //    if (Record.Lives <= 0)
            //    {
            //        Record.Lives = 0;
            //        player.morir(); //llamamos al metodo morir que instancia la destruccion del objeto y una explosion                    
            //    }
                
            //}
            Destroy(gameObject); //Destruir bala
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
