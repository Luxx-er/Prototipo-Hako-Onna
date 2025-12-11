using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompartirInventario : MonoBehaviour
{
    [SerializeField] private GameObject InventarioAjeno;
    [SerializeField] private GameObject PestañaInventario;
    [SerializeField] public Button Compartir;
    public static bool PuedeCompartrir = false;


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
    //En el momento que se istancie un objeto se agrege al scriptable object del inventario, CAPACITY (max Elements)

}
