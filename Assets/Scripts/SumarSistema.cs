using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SumarSistema : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoDecantidad;
    [SerializeField] private int cantidad;
    [SerializeField] private int cantidadMaxima;
   [SerializeField] Desactivarcanvas desactivarcanvas;
    public void Sumar(int cantidadDeEntrada)
    {
        if(cantidad >= cantidadMaxima)
        {
            desactivarcanvas.desactivarcanvas();

            return;
        }
        

        cantidad += cantidadDeEntrada;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoDecantidad.text = cantidad.ToString();
        
    }
}

