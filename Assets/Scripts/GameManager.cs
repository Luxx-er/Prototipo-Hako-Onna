using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int cantJugadores;
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject Cambio;
    public GameObject CambioRuido;
    public Animator animatorCambio;
    public Animator animatorCambioRuido;
    void Start()
    {
        cantJugadores = CantidadJugadores.CantFinal;
        switch(cantJugadores)
        {
            case 2:
                Destroy(Player3);
                Destroy(Player4);
                break;
                case 3:
                Destroy(Player4);
                break;
        }
    }
    public void OcultarCanvas()
    {
        Cambio.SetActive(false);
    }
   public void EjecutarAnimacion()
    {
        Cambio.SetActive(true);
        animatorCambio.Play("Oscurece 1", -1, 0f);
    }
    public void EjecutarAnimacionRuido()
    {
        Cambio.SetActive(true);
        animatorCambioRuido.Play("Oscurece 2", -1, 0f);
    }
}
