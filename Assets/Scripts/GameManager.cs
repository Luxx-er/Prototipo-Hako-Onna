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
    void Start()
    {
        cantJugadores = CantidadJugadores.CantFinal;
        switch(cantJugadores)
        {
            case 2:
                Player3.SetActive(false);
                Player4.SetActive(false);
                break;
                case 3:
                Player4.SetActive(false);
                break;
        }
    }
    public void Inventario()
    {

    }
   
}
