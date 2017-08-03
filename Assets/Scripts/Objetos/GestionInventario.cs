using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionInventario : MonoBehaviour {

    #region Variables
    public GameObject[] itemRapido;                              //arreglo que almacena las imagenes de presentacion de los items de acceso rapido, se almacenan en casillas
    public Item[] items;                                         //arreglo que almacena los objetos de tipo item que contienen las imagenes de presentacion y accion
    private Sprite porDefecto;                                  //sprite que contiene UIMask, la imagen por defecto
    private bool soloUnaVez;                                    //boleano para cargar solo una vez la imagen por defecto
    #endregion

    #region Funciones de Unity
    void Start () {
        items = new Item[itemRapido.Length];
    }
	

	void Update () {
		
	}
    #endregion

    #region Funciones para guardar items
    private void asignarItems(GameObject ItemPresentacion, Sprite ItemAccion)       //Función para guardar los items
    {
        Debug.Log("Asignando imagenes de presentacion y accion a los items");
        //se hace una busqueda entre los items
        for (int i = 0; i < items.Length; i++)
        {
            Debug.Log("items["+i+"]=" + items[i]);
            if (items[i] == null)
            {
                Debug.Log("ItemPresentacion=" + ItemPresentacion);
                items[i] = new Item();
                items[i].cargarImagenes(ItemPresentacion, ItemAccion);
                Debug.Log("items[" + i + "]=" + items[i].presentacion);
                break;
            }
        }
    }
    //metodo que permite quitar del inventario al objeto
    public void desasignarItemACasilla(int i)
    {
        itemRapido[i].GetComponent<Image>().sprite = porDefecto;
    }

    //carga al sprite por defecto, para cuando necesitemos mostrar una casilla vacia
    private void cargarSpritePorDefecto(Sprite s)
    {
        if (!soloUnaVez)
        {
            porDefecto = s;
            soloUnaVez = true;
        }
        
    }

    public void recargarMunicion(GameObject municion,Sprite aux)
    {
        bool encontrado = false;
        Debug.Log("La municion se llama: " + municion.name);
        switch (municion.name)
        {
            case "MunicionPistola":
               
                //se hace una busqueda entre los casilleros, para encontrar alguno vacio
                for (int i = 0; i < itemRapido.Length; i++)
                {
                    ////el casillero vacio es aquel que tenga el sprite por defecto "UIMask"
                    //Debug.Log("nombre=" + itemRapido[i+1].GetComponent<Image>().sprite.name);            
                    if (itemRapido[i].GetComponent<Image>().sprite.name != "UIMask")
                    {
                        
                        switch (items[i].objeto.name)
                        {
                           
                            case "Pistola":
                                //agregamos un texto que indique el numero de balas a usar
                                items[i].objeto.GetComponent<Pistola>().cantidadBalas += 6;
                                itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas+"";
                                encontrado = true;
                                break;
                           
                        }

                    }
                    if (encontrado)
                    {
                        break;
                    }
                    
                }
                if (!encontrado)
                {
                    asignarItemACasilla(municion,aux);
                }
                break;
            case "MunicionAmetralladora":
                //se hace una busqueda entre los casilleros, para encontrar alguno vacio
                for (int i = 0; i < itemRapido.Length; i++)
                {
                    ////el casillero vacio es aquel que tenga el sprite por defecto "UIMask"
                            
                    if (itemRapido[i].GetComponent<Image>().sprite.name != "UIMask")
                    {
                        Debug.Log("nombre del arma encontrado=" + items[i].objeto.name);
                        switch (items[i].objeto.name)
                        {

                            case "Ametralladora":
                                //agregamos un texto que indique el numero de balas a usar
                                items[i].objeto.GetComponent<Pistola>().cantidadBalas += 30;
                                Debug.Log("Balas agregadas=" + items[i].objeto.GetComponent<Pistola>().cantidadBalas);
                                itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas+ "";
                                encontrado = true;
                                break;

                        }

                    }
                    if (encontrado)
                    {
                        break;
                    }

                }
                if (!encontrado)
                {
                    asignarItemACasilla(municion, aux);
                }
                break;
        }
    }

    //metodo que nos ayuda a verificar si ya tenemos un item y no sea necesario ponerlo en el invetario, sino solo aumentar su numero
    private int buscarItem(string nombre)
    {
       
        int lugarItem = -1;
       
        //se hace una busqueda entre los casilleros, para encontrar al item
        for (int i = 0; i < itemRapido.Length; i++)
        {
            if (itemRapido[i].GetComponent<Image>().sprite.name != "UIMask")
            {
                switch (items[i].objeto.name)
                {
                    case "Curacion":
                        //verificamos si ya tenemos este item, si ya existe se guarda el lugar del item actualmente
                        if(nombre== "Curacion")
                        {
                           Debug.Log("Se encontro otra curacion");
                           int nroCuras = int.Parse(itemRapido[i].GetComponentInChildren<Text>().text) ;
                           nroCuras++;
                           itemRapido[i].GetComponentInChildren<Text>().text = nroCuras+"";
                           lugarItem = i;
                            
                        }
                        break;
                    case "Pistola":
                        //verificamos si ya tenemos este item, si ya existe se guarda el lugar del item actualmente
                        if (nombre == "Pistola")
                        {
                            Debug.Log("Se encontro otra Pistola");
                            items[i].objeto.GetComponent<Pistola>().cantidadBalas += 6;
                            itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas + " / "+ items[i].objeto.GetComponent<Pistola>().TotalBalas;
                            lugarItem = i;
                           
                        }
                        break;
                    case "Ametralladora":
                        //verificamos si ya tenemos este item, si ya existe se guarda el lugar del item actualmente
                        if (nombre == "Ametralladora")
                        {
                            Debug.Log("Se encontro otra Ametralladora");
                            items[i].objeto.GetComponent<Pistola>().cantidadBalas += 30;
                            itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas + " / " + items[i].objeto.GetComponent<Pistola>().TotalBalas;
                            lugarItem = i;

                        }
                        break;
                }
            }
        }
        
        
        return lugarItem;
    }

    public int asignarItemACasilla(GameObject ItemPresentacion, Sprite ItemAccion)
    {
        Debug.Log("Gestion Inventario accedi ");
        int lugarAsignado = -1;
        Debug.Log("ItemPresentacion="+ ItemPresentacion.name);

        //buscaremos si el item que sera asignado ya existe en nuestro inventario
        //buscarItem devuelve el lugar del item si es que ya existe, de lo contrario, envia -1
        lugarAsignado = buscarItem(ItemPresentacion.name);
        
        Debug.Log("itemEncontrado en lugar nro=" + lugarAsignado);
        //si no se encontro el item, entonces se procede a agregar al inventario
        if (lugarAsignado==-1)
        {
            asignarItems(ItemPresentacion, ItemAccion);
            //se hace una busqueda entre los casilleros, para encontrar alguno vacio
            for (int i = 0; i < itemRapido.Length; i++)
            {
                ////el casillero vacio es aquel que tenga el sprite por defecto "UIMask"
                //Debug.Log("nombre=" + itemRapido[i+1].GetComponent<Image>().sprite.name);            
                if (itemRapido[i].GetComponent<Image>().sprite.name == "UIMask")
                {
                    //cargamos el sprite por defecto (solo una vez)
                    cargarSpritePorDefecto(itemRapido[i].GetComponent<Image>().sprite);
                    //si el casillero esta vacio se le asigna el item recogido
                    itemRapido[i].GetComponent<Image>().sprite = items[i].presentacion;
                    switch (items[i].objeto.name)
                    {
                        case "Curacion":

                            //agregamos un texto que indique el numero de botiquines que se posee
                            itemRapido[i].GetComponentInChildren<Text>().text = items[i].getStock() + "";
                            lugarAsignado = i;
                            break;
                        case "Pistola":
                            //agregamos un texto que indique el numero de balas a usar
                            itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas + " / " + items[i].objeto.GetComponent<Pistola>().TotalBalas;
                            lugarAsignado = i;
                            break;
                        case "Ametralladora":
                            //agregamos un texto que indique el numero de balas a usar
                            itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas + " / " + items[i].objeto.GetComponent<Pistola>().TotalBalas;
                            lugarAsignado = i;
                            break;
                        case "MunicionPistola":
                            //agregamos un texto que indique el numero de balas a usar
                            itemRapido[i].GetComponentInChildren<Text>().text = "6";
                            lugarAsignado = i;
                            break;
                        case "MunicionAmetralladora":
                            //agregamos un texto que indique el numero de balas a usar
                            itemRapido[i].GetComponentInChildren<Text>().text = "30";
                            lugarAsignado = i;
                            break;

                    }

                    Debug.Log("El nombre del nuevo elemento es=" + itemRapido[i].GetComponent<Image>().sprite.name);
                    break;
                }
                //else
                //{
                //    switch (items[i].objeto.name)
                //    {
                //        case "Curacion":
                //            //agregamos un texto que indique el numero de botiquines que se posee
                //            itemRapido[i].GetComponentInChildren<Text>().text = "0";
                //            break;
                //        case "Pistola":
                //            //agregamos un texto que indique el numero de balas a usar
                //            Debug.Log("Repeti pistola");
                //            //desasignarItemACasilla(items.Length-1);
                //            //items[items.Length - 1] = null;
                //            //agregamos un texto que indique el numero de balas a usar
                //            items[i].objeto.GetComponent<Pistola>().cantidadBalas += 6;
                //            itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas + "";
                //            //itemRapido[i].GetComponentInChildren<Text>().text = items[i].objeto.GetComponent<Pistola>().cantidadBalas + "";
                //            break;
                //    }
                //break;
                // }

            }//fin de for

        }
        
        return lugarAsignado;
    }

    public void restarMunicion(int posicionItem, int municionActual)
    {
        switch (items[posicionItem].objeto.name)
        {
            
            case "Pistola":
                if (municionActual <= 0)
                {
                    itemRapido[posicionItem].GetComponentInChildren<Text>().text = "0 / "+ items[posicionItem].objeto.GetComponent<Pistola>().TotalBalas;
                    items[posicionItem].objeto.GetComponent<Pistola>().cantidadBalas = 0;
                }
                //agregamos un texto que indique el numero de balas que quedan
                else
                {
                    itemRapido[posicionItem].GetComponentInChildren<Text>().text = municionActual + " / "+ items[posicionItem].objeto.GetComponent<Pistola>().TotalBalas;
                    items[posicionItem].objeto.GetComponent<Pistola>().cantidadBalas = municionActual;
                }
                break;
            case "Ametralladora":
                //en caso que se acaben las balas se retira objeto
                if (municionActual <= 0)
                {
                    itemRapido[posicionItem].GetComponentInChildren<Text>().text ="0";
                    items[posicionItem].objeto.GetComponent<Pistola>().cantidadBalas = 0;
                }
                //agregamos un texto que indique el numero de balas que quedan
                else
                {
                    itemRapido[posicionItem].GetComponentInChildren<Text>().text = municionActual + " / " + items[posicionItem].objeto.GetComponent<Pistola>().TotalBalas;
                    items[posicionItem].objeto.GetComponent<Pistola>().cantidadBalas = municionActual;
                }
                break;
        }
    }
    #endregion
}
