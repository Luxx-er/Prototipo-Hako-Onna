using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    public static Transform player;
    public float Radio = 1.3f;
    private bool jugadorEnRango = false;
    private bool SeMovio = false;
    public static bool SeMovera = false;
    public enum Direccion { Norte, Sur, Este, Oeste} //Enum = Lista
    public Direccion direccion;
    public float DistanciaSalto = 7f;

    private void Start()
    {
        player = GameObject.FindAnyObjectByType<Player>().transform;
    }

    private void Update()
    {
        
        float distancia = Vector2.Distance(transform.position, player.position);
        jugadorEnRango = distancia < Radio;

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !SeMovio && SeMovera && Player.PuedeInteractuar)
        {
            Player.JugadorEnMovimiento = false;
            Debug.Log("En rango de una puerta");
            SeMovio = true;
            SeMovera = false;
            Vector3 nuevaPos = player.position;

            switch (direccion)
            {
                case Direccion.Norte:
                    nuevaPos.y += DistanciaSalto;
                    Debug.Log("Puerta Norte usada");
                    break;

                case Direccion.Sur:
                    nuevaPos.y -= DistanciaSalto;
                    Debug.Log("Puerta Sur usada");
                    break;

                case Direccion.Este:
                    nuevaPos.x += DistanciaSalto;
                    Debug.Log("Puerta Este usada");
                    break;

                case Direccion.Oeste:
                    nuevaPos.x -= DistanciaSalto;
                    Debug.Log("Puerta Oeste usada");
                    break;
            }
            player.position = nuevaPos;
            Cambio();
        }
    } 
    IEnumerator Cambio()
    {
        yield return new WaitForSeconds(2f);
        Turnos.Instance.NextTurn();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}