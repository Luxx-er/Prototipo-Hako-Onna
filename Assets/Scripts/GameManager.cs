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
                Destroy(Player3);
                Destroy(Player4);
                break;
                case 3:
                Destroy(Player4);
                break;
        }
    }
   
}
