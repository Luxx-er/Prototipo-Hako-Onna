using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CantidadJugadores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Cantidad;
    [SerializeField] GameObject Jugador3;
    [SerializeField] GameObject Jugador4;
    int Cant = 2;
    public void MasJugadores()
    {
        if (Cant < 4)
        {
            Cant++; Cantidad.text = Cant.ToString();
        }
    }
    public void MenosJugadores()
    {
        if (Cant > 2)
        {
            Cant--; Cantidad.text = Cant.ToString();
        }
    }

    public void Mostrarjugador()
    {
        if (Cant == 3)
        {
            Jugador3.SetActive(true);
        }
        if (Cant == 4)
        {
            Jugador4.SetActive(true);
        }



    }

    public void Ocultarjugador()
    {
        if (Cant < 3)
        {
            Jugador3.SetActive(false);
        }
        if (Cant < 4)
        {
            Jugador4.SetActive(false);
        }
    }
    public void Siguiente()
    {
        SceneManager.LoadScene("InterfazPrincipal");
    }
}
