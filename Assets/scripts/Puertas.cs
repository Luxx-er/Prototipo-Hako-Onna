using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puertas : MonoBehaviour
{
    public Transform player;
    public float Radio = 1.3f;
    private bool jugadorEnRango = false;
    private bool SeMovio = false;
    public static bool SeMovera = false;
    [SerializeField] GameObject Puerta;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distancia = Vector2.Distance(transform.position, player.position);
        jugadorEnRango = distancia < Radio;
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !SeMovio && SeMovera)
        {
            Player.JugadorEnMovimiento = false;
            Debug.Log("En rango de una puerta");
            SeMovio = true;
            SeMovera = false;

            if(Puerta.transform.position.y < player.transform.position.y)
            {
                Debug.Log("Puerta Norte Usada");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}