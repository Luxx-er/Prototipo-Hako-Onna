using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompartirInventario : MonoBehaviour
{
    [SerializeField] private GameObject InventarioAjeno;
    [SerializeField] private GameObject PestañaInventario;
    public void InventarioCompartido()
    {
        Debug.Log("Mostrar Inventario");
        InventarioAjeno.SetActive(true);
        PestañaInventario.SetActive(true);
        //if otro jugador esta en la misma habitacion que el jugador activar boton de compartir inventario
    }

    public void PestañaInventarios()
    {
        //Cambiar entre inventarios, presionar boton de tag y cambia entre paneles
        Debug.Log("Sobrepone Inventario");
    }
}
