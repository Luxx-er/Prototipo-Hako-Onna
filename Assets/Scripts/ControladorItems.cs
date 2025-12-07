using System.Collections.Generic;
using UnityEngine;

public class ControladorItems : MonoBehaviour
{
    public static ControladorItems Instance;
    public List<GameObject> Items;
    public GameObject Tache;
    public GameObject Llaves;
    [SerializeField] public GameObject Muerte;
    List<GameObject> DigitosRestantes = new List<GameObject>();
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
        CajaFuerte caja = FindAnyObjectByType<CajaFuerte>();
        List<GameObject> NuevosItems = caja.AgregarItems();
        Items.AddRange(NuevosItems);

        
    }
}