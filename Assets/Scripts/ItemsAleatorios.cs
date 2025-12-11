using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemsAleatorios : MonoBehaviour
{
    
    public float Radio = 1.3f;
    private bool jugadorEnRango = false;
    private bool Obtenido = false;
    public static bool Investiga = false;
    public bool HakoOnnaAqui = false;
    public List<GameObject> Jugadores;
    
    private void Start()
    {
        Player[] Personajes = FindObjectsOfType<Player>();
        foreach(Player personajes in Personajes)
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

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !Obtenido && Investiga && Player.PuedeInteractuar)
        {
            if (HakoOnnaAqui == false)
            {
                StartCoroutine(DarItem());
            }else
            {
                InvocarHakoOnna();
            }
        }
    }

    IEnumerator DarItem()
    {
        GameObject prefabItem = ControladorItems.Instance.ObtenerItemAleatorio();
        GameObject Tache = ControladorItems.Instance.Tache;
        Player.JugadorEnMovimiento = false;
        if (prefabItem != null)
        {
            Instantiate(prefabItem, transform.position, Quaternion.identity);
            Debug.Log("Has encontrado: " + prefabItem.name);
            Obtenido = true;
            Investiga = false;
        }
        yield return new WaitForSeconds(2f);
        Inventario inventario = FindObjectOfType<Inventario>();
        if (inventario != null)
        {
            inventario.AgregarItem(prefabItem);
        }
        Instantiate(Tache, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        Turnos.Instance.NextTurn();

    }

    void InvocarHakoOnna()
    {
        GameObject Hako = ControladorItems.Instance.Muerte;
        Player.JugadorEnMovimiento = false;
        Obtenido = true;
        Hako.SetActive(true);
        Debug.Log("CAGASTE");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}