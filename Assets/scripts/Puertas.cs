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
    public List<GameObject> Jugadores;

    private void Start()
    {
        Player[] Personajes = FindObjectsOfType<Player>();
        foreach (Player personajes in Personajes)
        {
            Jugadores.Add(personajes.gameObject);
        }
    }

    private void Update()
    {
        Transform player = null;
        foreach (GameObject obj in Jugadores)
        {
            Player Jugador = obj.GetComponent<Player>();
            if (Jugador.playerInTurn)
            {
                player = obj.transform;
                break;
            }
        }
        float distancia = Vector2.Distance(transform.position, player.position);
        jugadorEnRango = distancia < Radio;

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !SeMovio && SeMovera && Player.PuedeInteractuar && Player.Quieto == true && CartasRuido.YaEligio)
        {
            Debug.Log("En rango de una puerta");
            SeMovera = false;
            Vector3 nuevaPos = player.position;
            Player.JugadorEnMovimiento = false;
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
            StartCoroutine (Cambio());
            CartasRuido.YaEligio = false;
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