using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ItemData;

public class Inventario : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Transform contenedorIconos; // Contenedor GridLayoutGroup donde van los íconos
    [SerializeField] private GameObject plantillaIcono;  // Prefab base del ícono
    private List<GameObject> items = new List<GameObject>(); // Lista de ítems del jugador

    // Agrega un ítem al inventario de este jugador.
    public void AgregarItem(GameObject itemPrefab)
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning($"[{name}] Se intentó agregar un item nulo al inventario.");
            return;
        }

        ItemData data = itemPrefab.GetComponent<ItemData>();
        if (data == null)
        {
            Debug.LogWarning($"[{name}] El prefab {itemPrefab.name} no tiene ItemData.");
            return;
        }

        // Si es especial o Tachesote, no se agrega al inventario
        if (data.tipoEspecial != TipoEspecial.Ninguno || data.Tachesote)
        {
            Debug.Log($"[{name}] El item {itemPrefab.name} es un objeto único, no se agrega al inventario.");
            return;
        }
   
        items.Add(itemPrefab);

        // Crear el ícono visual en la UI
        if (data.icono != null)
        {
            GameObject nuevoIcono = Instantiate(plantillaIcono, contenedorIconos);
            nuevoIcono.GetComponent<Image>().sprite = data.icono;
            nuevoIcono.SetActive(true);
        }

        // Extra: Si la caja fuerte está abierta, agregar el ícono de su contenido
        if (CajaFuerte.CajaAbierta && data.icono != null)
        {
            GameObject nuevoIcono = Instantiate(plantillaIcono, contenedorIconos);
            nuevoIcono.GetComponent<Image>().sprite = data.icono;
            nuevoIcono.SetActive(true);
        }

        Debug.Log($"[{gameObject.name}] agregó al inventario: {itemPrefab.name}");
    }


    // Activa o desactiva la UI del inventario.
    public void ActivarUI(bool activo)
    {
        if (contenedorIconos != null)
            contenedorIconos.gameObject.SetActive(activo);
    }
    // Limpia el inventario visual (por si lo necesitas entre turnos).
    public void LimpiarInventarioVisual()
    {
        foreach (Transform hijo in contenedorIconos)
        {
            Destroy(hijo.gameObject);
        }
    }
    // Devuelve todos los ítems que este jugador tiene (por si los necesitas).
    public List<GameObject> ObtenerItems()
    {
        return items;
    }
}