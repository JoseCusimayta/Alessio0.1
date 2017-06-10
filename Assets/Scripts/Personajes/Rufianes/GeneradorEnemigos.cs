using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour {
    public GameObject Prefab_Rufian;
    public GameObject Alessio;
    public float Tiempo_Respawn;
    float x, y, z;
    // Use this for initialization
    void Start()
    {
        Tiempo_Respawn = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        Tiempo_Respawn -= Time.deltaTime;
        if (Tiempo_Respawn <= 0)
        {
            Respawn();
        }

    }
    void Respawn()
    {
        x = Random.Range(10f, 20f); //Posición del eje Y al azar, entre 10 y 20
        y = Random.Range(-4f, 5f); //Posición del eje X al azar, entre -4 y 5
        z = 0.0f; //Posición del eje Z en 0
        Vector3 vector3 = new Vector3(x, y, z); //Se crea un vector para guardar la posición en los ejes        
        Enemigo rufianesAI = Prefab_Rufian.GetComponent<Enemigo>();
        rufianesAI.player = Alessio;
        Instantiate(Prefab_Rufian, vector3, transform.rotation); //Crear un nuevo rufian con los anteriores valores 

        Tiempo_Respawn = Random.Range(1, 5);

    }
}
