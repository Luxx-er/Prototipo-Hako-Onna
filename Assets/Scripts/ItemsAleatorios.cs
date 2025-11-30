using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAleatorios : MonoBehaviour
{
    public List<GameObject> Items;
    private GameObject itemActivo = null;
    public GameObject Tache;

    public void ItemsRandom()
    {
        if (Items.Count == 0)
        {
            Debug.Log("No hay más ítems disponibles.");
            return;
        }

        if (itemActivo != null)
        {
            Destroy(itemActivo);
            itemActivo = null;
        }

        int indiceAleatorio = Random.Range(0, Items.Count);
        GameObject itemSeleccionado = Items[indiceAleatorio];
        itemActivo = Instantiate(itemSeleccionado, transform.position, transform.rotation);
        Items.RemoveAt(indiceAleatorio);
        Items.Add(Tache);
    }
}

