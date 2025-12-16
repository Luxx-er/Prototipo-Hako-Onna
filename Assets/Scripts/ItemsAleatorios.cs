using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsAleatorios : MonoBehaviour
{
    public float Radio = 1.3f;
    public bool Obtenido = false;   // Debe ser público para poder reiniciarse
    public static bool Investiga = false;
    public bool HakoOnnaAqui = false;
    public List<GameObject> Jugadores;

    public Animator animator;
    public static ItemsAleatorios Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }

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

        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);
        bool jugadorEnRango = distancia < Radio;

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !Obtenido && Investiga && Player.PuedeInteractuar)
        {
            if (!HakoOnnaAqui)
                StartCoroutine(DarItem());
            else
                InvocarHakoOnna();
        }
    }

    IEnumerator DarItem()
    {
        GameObject prefabItem = ControladorItems.Instance.ObtenerItemAleatorio();
        GameObject Tache = ControladorItems.Instance.Tache;
        Player.JugadorEnMovimiento = false;

        if (prefabItem != null)
        {
            GameObject item = Instantiate(prefabItem, transform.position, Quaternion.identity);
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;

            Obtenido = true;
            Investiga = false;

            Debug.Log($"Has encontrado: {prefabItem.name}");
            yield return new WaitForSeconds(2f);

            Player jugadorActual = null;
            Player[] jugadores = FindObjectsOfType<Player>();

            foreach (Player p in jugadores)
            {
                if (p.playerInTurn)
                {
                    jugadorActual = p;
                    break;
                }
            }

            //Agregar el ítem al inventario de ese jugador
            if (jugadorActual != null)
            {
                Inventario inventarioJugador = jugadorActual.GetComponent<Inventario>();

                if (inventarioJugador != null)
                {
                    inventarioJugador.AgregarItem(prefabItem);
                    Debug.Log($" {jugadorActual.name} obtuvo {prefabItem.name}");
                }
            }



            Destroy(item);
            GameObject tache = Instantiate(Tache, transform.position, Quaternion.identity);
            tache.transform.SetParent(transform);
            tache.transform.localPosition = Vector3.zero;

            Debug.Log("Ítem reemplazado por tache.");
        }

        yield return new WaitForSeconds(3f);

        Turnos.Instance.NextTurn();
    }


    void InvocarHakoOnna()
    {
        GameObject Hako = ControladorItems.Instance.Muerte;
        Player.JugadorEnMovimiento = false;
        Hako.SetActive(true);
        Debug.Log("Hako Onna aparecio!");
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}
