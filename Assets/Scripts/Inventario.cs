using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private Transform contenedorIconos; 
    [SerializeField] private GameObject plantillaIcono;  

    private List<GameObject> items = new List<GameObject>();

    public void AgregarItem(GameObject itemPrefab)
    {
        items.Add(itemPrefab);
        ItemData data = itemPrefab.GetComponent<ItemData>();
        if (data != null && data.icono != null)
        {
            GameObject nuevoIcono = Instantiate(plantillaIcono, contenedorIconos);
            nuevoIcono.GetComponent<Image>().sprite = data.icono;
            nuevoIcono.SetActive(true);
        }
        if (CajaFuerte.CajaAbierta)
        {
            GameObject nuevoIcono = Instantiate(plantillaIcono, contenedorIconos);
            nuevoIcono.GetComponent<Image>().sprite = data.icono;
            nuevoIcono.SetActive(true);
        }

        Debug.Log(" Agregado al inventario: " + itemPrefab.name);
    }
}