using System.Collections.Generic;
using UnityEngine;

public class ControladorItems : MonoBehaviour
{
    public static ControladorItems Instance;
    public List<GameObject> Items;
    public GameObject Tache;
    public GameObject Caballo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

   
    public GameObject ObtenerItemAleatorio()
    {
        
        int indice = Random.Range(0, Items.Count);
        GameObject itemSeleccionado = Items[indice];
        Items.RemoveAt(indice);
        Items.Add(Tache);
        return itemSeleccionado;
    }
    public void Start()
    {
        ItemsAleatorios[] Casillas = FindObjectsOfType<ItemsAleatorios>();
        int Esconder = Random.Range(0, Casillas.Length);
        Casillas[Esconder].HakoOnnaAqui = true;
    }
}