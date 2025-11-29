using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MapaAleatorio : MonoBehaviour
{
    public List<GameObject> Mapas;
    private GameObject TableroActual = null;
    // Start is called before the first frame update
    void Start()
    {
        int indiceAleatorio = Random.Range(0, Mapas.Count);
        GameObject itemSeleccionado = Mapas[indiceAleatorio];
        TableroActual = Instantiate(itemSeleccionado, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
