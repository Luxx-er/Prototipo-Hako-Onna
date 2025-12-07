using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventario : MonoBehaviour
{
    [SerializeField] private Transform contenedorIconos; //Tiene el objeto que va a almacenar los items el INVENTARIO 
    [SerializeField] private GameObject plantillaIcono;  //Casilla icono para clonarla 

    private List<GameObject> items = new List<GameObject>(); //Lista vacia donde se ponen los items que se obtengan

    public void AgregarItem(GameObject itemPrefab) 
    {
        items.Add(itemPrefab);
        ItemData data = itemPrefab.GetComponent<ItemData>(); //Referencia el ItemData, todos los objetos que tengan el codigo ItemsData los referencia a la lista
        if (data != null && data.icono != null) //Si si hay un item 
        {
            GameObject nuevoIcono = Instantiate(plantillaIcono, contenedorIconos); //Se instancia la casilla con la escala de los valores del padding en el Grid Layout group
            nuevoIcono.GetComponent<Image>().sprite = data.icono;
            nuevoIcono.SetActive(true); //La hace visible
        }
        if (CajaFuerte.CajaAbierta) //Referencias el codigo de la caja fuerte, y la booleana estatica del mismo codigo, si esa booleana es verdadera 
        {
            GameObject nuevoIcono = Instantiate(plantillaIcono, contenedorIconos); // Se instancia la casilla con la escala de los valores del padding en el Grid Layout group
            nuevoIcono.GetComponent<Image>().sprite = data.icono;
            nuevoIcono.SetActive(true);
        }

        Debug.Log(" Agregado al inventario: " + itemPrefab.name);
    }
}