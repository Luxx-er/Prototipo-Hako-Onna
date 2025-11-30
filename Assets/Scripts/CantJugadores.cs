using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CantidadJugadores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Cantidad;
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
    public void Siguiente()
    {
        SceneManager.LoadScene("SeleccionPersonajes");
    }
}
