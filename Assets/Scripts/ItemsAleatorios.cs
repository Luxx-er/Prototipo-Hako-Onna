using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemData;

public class ItemsAleatorios : MonoBehaviour
{
    public float Radio = 1.3f;
    public bool Obtenido = false;   // Debe ser público para poder reiniciarse
    public static bool Investiga = false;
    public bool HakoOnnaAqui = false;
    public List<GameObject> Jugadores;
    public Animator animator;
    public bool CasillaBloqueadaPermanente = false; // Para ítems especiales

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
        if (!Player.PuedeInteractuar || !CartasRuido.YaEligio)
            return;

        // Encontrar jugador actual en turno
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

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !Obtenido && Investiga)
        {
            if (!HakoOnnaAqui)
                StartCoroutine(DarItem());
            else
                StartCoroutine(InvocarHakoOnna());

            CartasRuido.YaEligio = false; // consume la carta usada
            return;
        }

        // Interacción con ítem especial (solo si ya eligió carta)
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && Obtenido && Investiga && CasillaBloqueadaPermanente)
        {
            ItemData data = GetComponentInChildren<ItemData>();
            if (data != null)
                ActivarItemEspecial(data);

            CartasRuido.YaEligio = false;
            return;
        }
    }



    IEnumerator DarItem()
    {
        GameObject prefabItem = ControladorItems.Instance.ObtenerItemAleatorio();
        GameObject Tache = ControladorItems.Instance.Tache;
        Player.JugadorEnMovimiento = false;

        if (prefabItem == null) yield break;

        // Instancia temporal del ítem en el mapa
        GameObject item = Instantiate(prefabItem, transform.position, Quaternion.identity);
        item.transform.SetParent(transform);
        item.transform.localPosition = Vector3.zero;

        ItemData data = item.GetComponent<ItemData>();

        Investiga = false;
        Debug.Log($"Has encontrado: {prefabItem.name}");
        yield return new WaitForSeconds(2f);

        // Buscar el jugador actual
        Player jugadorActual = null;
        foreach (Player p in FindObjectsOfType<Player>())
        {
            if (p.playerInTurn)
            {
                jugadorActual = p;
                break;
            }
        }

        if (jugadorActual == null) yield break;
        Inventario inventarioJugador = jugadorActual.GetComponent<Inventario>();
        if (inventarioJugador == null) yield break;

        // Ítem normal se agrega al inventario
        if (data != null && data.tipoEspecial == TipoEspecial.Ninguno && !data.Tachesote)
        {
            inventarioJugador.AgregarItem(prefabItem);
            Debug.Log($"{jugadorActual.name} obtuvo {prefabItem.name}");

            Destroy(item);
            GameObject tache = Instantiate(Tache, transform.position, Quaternion.identity);
            tache.transform.SetParent(transform);
            tache.transform.localPosition = Vector3.zero;

            Obtenido = true;
            CasillaBloqueadaPermanente = false;
            Debug.Log("Ítem normal agregado al inventario y reemplazado por tache.");
        }
        // Tachesote no se agrega, pero se reemplaza y se reinicia
        else if (data != null && data.Tachesote)
        {
            Destroy(item);
            GameObject tache = Instantiate(Tache, transform.position, Quaternion.identity);
            tache.transform.SetParent(transform);
            tache.transform.localPosition = Vector3.zero;

            Obtenido = true;
            CasillaBloqueadaPermanente = false;
            Debug.Log("Tachesote detectado: no se agrega al inventario, pero la casilla puede reiniciarse.");
        }
        else
        {
            Debug.Log($"Ítem especial detectado: {prefabItem.name}. Se mantiene en el mapa.");
            Obtenido = true;
            CasillaBloqueadaPermanente = true; // no se reinicia
        }

        yield return new WaitForSeconds(3f);
        Turnos.Instance.NextTurn();
    }


    void ActivarItemEspecial(ItemData data)
    {
        switch (data.tipoEspecial)
        {
            case TipoEspecial.CajaFuerte:
                Debug.Log("Se activó la Caja Fuerte");
                break;

            case TipoEspecial.Sombrero:
                Debug.Log("El sombrero revela un secreto...");
                // Aquí puedes activar otro evento o animación
                break;

            case TipoEspecial.PuertaSecreta:
                Debug.Log("La Puerta Secreta se ha revelado...");
                // Podrías abrir una puerta en el mapa o cambiar de escena
                break;
        }
    }



    IEnumerator InvocarHakoOnna()
    {
        GameObject Hako = ControladorItems.Instance.Muerte;
        Player.JugadorEnMovimiento = false;
        CartasRuido.YaEligio = false;
        Hako.SetActive(true);
        Debug.Log("Hako Onna aparecio!");
        yield return new WaitForSeconds(4f);
        Turnos.Instance.NextTurn();
        Hako.SetActive(false);
    }
      
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}
