using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartasRuido : MonoBehaviour
{
    [Header("UI y Canvas")]
    [SerializeField] private TextMeshProUGUI TextoDecantidad;
    [SerializeField] private GameObject CanvasRuido;
    [SerializeField] private GameObject CanvasHako;
    [SerializeField] private CanvasGroup Mazo;
    [SerializeField] private GameObject Carta0;
    [SerializeField] private GameObject Carta1;
    [SerializeField] private GameObject Carta2;
    [SerializeField] private GameObject Carta3;
    [SerializeField] private GameObject Carta4;
    [SerializeField] private GameObject Carta5;
    public AudioSource Estruendo;

    [Header("Prefabs y puntos de aparición")]
    [SerializeField] private List<GameObject> PrefabsCartas;
    [SerializeField] private List<Transform> PuntosCartas;

    private List<GameObject> CartasEnJuego = new List<GameObject>();
    private List<int> Cartas = new List<int> { 0, 1, 2, 3, 4, 5 };
    private List<int> NumerosCompletos = new List<int> { 0, 1, 2, 3, 4, 5 };

    private int RuidoTotal = 0;
    private int Indice = 0;
    private int CartaSacada;
    public static bool YaEligio = false;

    public static bool HakoEnTurno = false;
    private bool hakoActivada = false;

    private void Update()
    {
        if ((AccionesJugador.JugadorInvestiga || AccionesJugador.JugadorSeMueve) && !HakoEnTurno)
        {
            CanvasRuido.SetActive(true);
        }
        else
        {
            CanvasRuido.SetActive(false);
        }

        // Activar Hako Onna si el ruido supera 11
        if (RuidoTotal >= 11 && !hakoActivada)
        {
            hakoActivada = true;
            StartCoroutine(ActivaHakoOnna());
        }
    }

    public void SeleccionaCarta()
    {
        if (HakoEnTurno) return; // no permitir sacar cartas mientras Hako está activa

        StartCoroutine(SacarCarta());
        Mazo.blocksRaycasts = false;
    }

    IEnumerator SacarCarta()
    {
        CanvasRuido.SetActive(true);
        int CartaRandom = Random.Range(0, Cartas.Count);
        CartaSacada = Cartas[CartaRandom];
        Cartas.RemoveAt(CartaRandom);
        RuidoTotal += CartaSacada;

        Debug.Log($"Sacaste la carta: {CartaSacada}");

        yield return new WaitForSeconds(2f);

        AcumularCartas();

        yield return new WaitForSeconds(3f);
        if (RuidoTotal < 11)
        {
            AccionesJugador.JugadorInvestiga = false;
            AccionesJugador.JugadorSeMueve = false;
            Player.JugadorEnMovimiento = true;
            Mazo.blocksRaycasts = true;
        }
        YaEligio = true;
    }

    void AcumularCartas()
    {
        if (Indice >= PuntosCartas.Count)
        {
            Debug.LogWarning("Ya no hay más espacios para cartas en mano.");
            return;
        }

        GameObject prefab = PrefabsCartas[CartaSacada];
        Transform punto = PuntosCartas[Indice];

        TextoDecantidad.text = RuidoTotal.ToString();
        GameObject nuevaCarta = Instantiate(prefab, punto.position, Quaternion.identity);
        nuevaCarta.transform.SetParent(punto, false);
        nuevaCarta.transform.localPosition = Vector3.zero;
        nuevaCarta.transform.rotation = Quaternion.identity;
        nuevaCarta.transform.localScale = Vector3.one;
        nuevaCarta.SetActive(true);
        nuevaCarta.transform.SetAsFirstSibling();
        CartasEnJuego.Add(nuevaCarta);
        Indice++;
    }

    IEnumerator ActivaHakoOnna()
    {
        HakoEnTurno = true;
        Estruendo.Play();
        CanvasHako.SetActive(true);
        CanvasRuido.SetActive(false);
        Player.JugadorEnMovimiento = false;
        AccionesJugador.JugadorInvestiga = false;
        AccionesJugador.JugadorSeMueve = false;
        ItemsAleatorios.Investiga = false;
        Puertas.SeMovera = false;

        Debug.Log("Hako Onna activada");

        yield return new WaitForSeconds(0.5f);
        Turnos.Instance.NextTurn();

        yield return new WaitForSeconds(0.3f);

        if (ControladorItems.Instance != null)
        {
            ControladorItems.Instance.ReiniciarCasillas();
        }
        RuidoTotal = 0;
        yield return new WaitForSeconds(1f);
        StartCoroutine(ReiniciarCartas());

        yield return new WaitForSeconds(4f);

        HakoEnTurno = false;
        hakoActivada = false; 
        Player.JugadorEnMovimiento = false;
        YaEligio = false;
        AccionesJugador.Instance.ActivarBotones();
        CanvasHako.SetActive(false);
        Debug.Log("Hako Onna terminó su efecto.");
    }

    IEnumerator ReiniciarCartas()
    {
        yield return new WaitForSeconds(1f);

        foreach (var carta in CartasEnJuego)
        {
            if (carta != null)
                Destroy(carta);
        }

        CartasEnJuego.Clear();
        Indice = 0;
        Cartas.Clear();
        Cartas.AddRange(NumerosCompletos);
        TextoDecantidad.text = RuidoTotal.ToString();

        Carta0.SetActive(true);
        Carta1.SetActive(true);
        Carta2.SetActive(true);
        Carta3.SetActive(true);
        Carta4.SetActive(true);
        Carta5.SetActive(true);

        YaEligio = false;
    }

}
