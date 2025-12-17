using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ItemData;

public class ItemsAleatorios : MonoBehaviour
{
    [Header("Configuración general")]
    public float Radio = 1.3f;
    public bool Obtenido = false;
    public static bool Investiga = false;

    [Header("Estado de la casilla")]
    public bool HakoOnnaAqui = false;
    public bool HakobitoAqui = false;
    public bool CasillaBloqueadaPermanente = false;

    [Header("Jugadores y control")]
    public List<GameObject> Jugadores;
    public Animator animator;

    [Header("Canvas")]
    [SerializeField] private GameObject CanvasCajaFuerte;
    [SerializeField] private GameObject CanvasVictoria;
    [SerializeField] private GameObject CanvasDerrota;
    [SerializeField] private TextMeshProUGUI Textito;

    private static bool enEvento = false;

    private void Start()
    {
        Player[] personajes = FindObjectsOfType<Player>();
        foreach (Player p in personajes)
        {
            Jugadores.Add(p.gameObject);
        }
    }

    private void Update()
    {
        if (!Player.PuedeInteractuar || !CartasRuido.YaEligio)
            return;

        Jugadores.RemoveAll(obj => obj == null);

        Transform player = null;
        foreach (GameObject obj in Jugadores)
        {
            if (obj == null) continue;
            Player jugador = obj.GetComponent<Player>();
            if (jugador != null && jugador.playerInTurn)
            {
                player = obj.transform;
                break;
            }
        }

        if (player == null)
            return;

        float distancia = Vector2.Distance(transform.position, player.position);
        bool jugadorEnRango = distancia < Radio;

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && Investiga && !enEvento)
        {
            bool hizoAccion = false;

            if (!Obtenido)
            {
                if (HakoOnnaAqui)
                {
                    StartCoroutine(InvocarHakoOnna());
                    hizoAccion = true;
                }
                else if (HakobitoAqui)
                {
                    StartCoroutine(InvocarHakobito());
                    hizoAccion = true;
                }
                else
                {
                    StartCoroutine(DarItem());
                    hizoAccion = true;
                }
            }
            else if (CasillaBloqueadaPermanente)
            {
                ItemData data = GetComponentInChildren<ItemData>();
                if (data != null)
                {
                    ActivarItemEspecial(data);
                    hizoAccion = true;
                }
            }
            if (hizoAccion)
                CartasRuido.YaEligio = false;
        }

    }

    IEnumerator DarItem()
    {
        GameObject prefabItem = ControladorItems.Instance.ObtenerItemAleatorio();
        GameObject Tache = ControladorItems.Instance.Tache;
        Player.JugadorEnMovimiento = false;

        if (prefabItem == null)
        {
            enEvento = false;
            yield break;
        }

        GameObject item = Instantiate(prefabItem, transform.position, Quaternion.identity, transform);
        item.transform.localPosition = Vector3.zero;
        item.transform.localScale = Vector3.one;

        ItemData data = item.GetComponent<ItemData>();
        Investiga = false;

        Debug.Log($"Has encontrado: {prefabItem.name}");
        yield return new WaitForSeconds(2f);

        Player jugadorActual = GetJugadorActual();
        if (jugadorActual == null)
        {
            enEvento = false;
            yield break;
        }

        Inventario inventario = jugadorActual.GetComponent<Inventario>();
        if (inventario == null)
        {
            enEvento = false;
            yield break;
        }

        if (data != null && data.tipoEspecial == TipoEspecial.Ninguno && !data.Tachesote)
        {
            inventario.AgregarItem(prefabItem);
            Destroy(item);
            CrearTache(Tache);
            Obtenido = true;
            CasillaBloqueadaPermanente = false;
        }
        else if (data != null && data.Tachesote)
        {
            Destroy(item);
            CrearTache(Tache);
            Obtenido = true;
            CasillaBloqueadaPermanente = false;
            Debug.Log("Tachesote detectado. Casilla reiniciable.");
        }
        else
        {
            Debug.Log($"Ítem especial: {prefabItem.name}. Se mantiene en el mapa.");
            Obtenido = true;
            CasillaBloqueadaPermanente = true;
        }

        yield return new WaitForSeconds(3f);
        Turnos.Instance.NextTurn();
        enEvento = false;
    }

    void ActivarItemEspecial(ItemData data)
    {
        Player jugadorActual = GetJugadorActual();
        if (jugadorActual == null) return;

        Inventario inventario = jugadorActual.GetComponent<Inventario>();
        if (inventario == null) return;

        switch (data.tipoEspecial)
        {
            case TipoEspecial.CajaFuerte:
                CanvasCajaFuerte.SetActive(true);
                Player.JugadorEnMovimiento = false;
                Debug.Log("Se activó la Caja Fuerte");
                break;

            case TipoEspecial.Sombrero:
                StartCoroutine(VictoriaConSombrero(inventario));
                break;

            case TipoEspecial.PuertaSecreta:
                StartCoroutine(Escapar(inventario));
                break;
        }
    }

    IEnumerator InvocarHakoOnna()
    {
        GameObject Hako = ControladorItems.Instance.Muerte;
        Player.JugadorEnMovimiento = false;
        CartasRuido.YaEligio = false;

        Hako.SetActive(true);
        Debug.Log("Hako Onna apareció...");

        Player jugador = GetJugadorActual();
        if (jugador != null)
            yield return EncuentroConHako(jugador);

        yield return new WaitForSeconds(4f);
        Hako.SetActive(false);
        enEvento = false;
    }

    IEnumerator InvocarHakobito()
    {
        GameObject Hakobito = ControladorItems.Instance.MuerteHakobito;
        Player.JugadorEnMovimiento = false;
        CartasRuido.YaEligio = false;

        Hakobito.SetActive(true);
        Debug.Log("Hakobito apareció...");

        Player jugador = GetJugadorActual();
        if (jugador != null)
            yield return AsesinatoDeHakobito(jugador);

        yield return new WaitForSeconds(4f);
        Hakobito.SetActive(false);
        enEvento = false;
    }

    IEnumerator VictoriaConSombrero(Inventario inventario)
    {
        bool tieneSemillas = inventario.ObtenerItems().Exists(i => i && i.name.Contains("Semillas mostaza"));

        if (tieneSemillas)
        {
            CanvasVictoria.SetActive(true);
            Player.JugadorEnMovimiento = false;
            Debug.Log("¡Has ganado! Tienes las semillas y el sombrero.");
        }
        else
        {
            Textito.text = "No tienes las semillas mostaza... pierdes el turno.";
            Player.JugadorEnMovimiento = false;
            yield return new WaitForSeconds(2f);
            Turnos.Instance.NextTurn();
        }
        enEvento = false;
    }

    IEnumerator Escapar(Inventario inventario)
    {
        bool tieneLlaves = inventario.ObtenerItems().Exists(i => i && i.name.Contains("Llaves"));

        if (tieneLlaves)
        {
            CanvasVictoria.SetActive(true);
            Player.JugadorEnMovimiento = false;
            Debug.Log("¡Has ganado! Lograste escapar.");
        }
        else
        {
            Textito.text = "No tienes las llaves... pierdes el turno.";
            Player.JugadorEnMovimiento = false;
            yield return new WaitForSeconds(2f);
            Turnos.Instance.NextTurn();
        }
        enEvento = false;
    }

    IEnumerator EncuentroConHako(Player jugador)
    {
        Inventario inventario = jugador.GetComponent<Inventario>();
        GameObject debilidadReal = ControladorItems.Instance.DebilidadDeHako;

        bool tieneDebilidad = inventario.ObtenerItems().Exists(i => i && i.name == debilidadReal.name);

        if (tieneDebilidad)
            yield return VictoriaJugador(jugador);
        else
            yield return AsesinatoDeHako(jugador);
    }

    IEnumerator VictoriaJugador(Player jugador)
    {
        Debug.Log($"{jugador.name} ha ganado derrotando a Hako Onna.");
        CanvasVictoria.SetActive(true);
        yield return new WaitForSeconds(2f);
        enEvento = false;
    }

    IEnumerator AsesinatoDeHako(Player jugador)
    {
        GameObject Hako = ControladorItems.Instance.Muerte;
        Hako.SetActive(true);
        yield return new WaitForSeconds(2f);

        Inventario inv = jugador.GetComponent<Inventario>();
        if (inv != null)
        {
            inv.LimpiarInventarioVisual();
            inv.ObtenerItems().Clear();
        }

        Destroy(jugador.gameObject);
        Debug.Log($"{jugador.name} fue asesinado por Hako Onna.");

        yield return new WaitForSeconds(3f);
        ControladorItems.Instance.EsconderHakobito();
        Hako.SetActive(false);
        Turnos.Instance.NextTurn();
        enEvento = false;
    }

    IEnumerator AsesinatoDeHakobito(Player jugador)
    {
        GameObject Hakobito = ControladorItems.Instance.MuerteHakobito;
        Hakobito.SetActive(true);
        yield return new WaitForSeconds(2f);

        Inventario inv = jugador.GetComponent<Inventario>();
        if (inv != null)
        {
            inv.LimpiarInventarioVisual();
            inv.ObtenerItems().Clear();
        }

        Destroy(jugador.gameObject);
        Debug.Log($"{jugador.name} fue asesinado por un Hakobito.");

        yield return new WaitForSeconds(3f);
        ControladorItems.Instance.EsconderHakobito();
        Hakobito.SetActive(false);
        Turnos.Instance.NextTurn();
        enEvento = false;
    }

    Player GetJugadorActual()
    {
        foreach (Player p in FindObjectsOfType<Player>())
            if (p.playerInTurn)
                return p;
        return null;
    }

    void CrearTache(GameObject prefab)
    {
        GameObject tache = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        tache.transform.localPosition = Vector3.zero;
        tache.transform.localScale = Vector3.one;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}


