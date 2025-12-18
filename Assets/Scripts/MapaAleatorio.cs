using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MapaAleatorio : MonoBehaviour
{
    public List<GameObject> Mapas; //Lista para meter todos los tilemaps prefabs
    public GameObject TableroActual = null;

    // Start is called before the first frame update
    void Start()
    {
        int indiceAleatorio = Random.Range(0, Mapas.Count); //Indice porque todas las listas son un indice
        GameObject itemSeleccionado = Mapas[indiceAleatorio];
        TableroActual = Instantiate(itemSeleccionado, transform.position, transform.rotation); //Guarda el item/mapa seleccionado en el GameObject
    }

    // Update is called once per frame
    void Update()
    {

    }
}
