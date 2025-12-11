using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartasRuido : MonoBehaviour
{
    [Header("UI y Canvas")]
    [SerializeField] private TextMeshProUGUI TextoDecantidad;
    [SerializeField] private GameObject CanvasRuido;
    [SerializeField] private CanvasGroup Mazo;
    [SerializeField] private GameObject Carta0;
    [SerializeField] private GameObject Carta1;
    [SerializeField] private GameObject Carta2;
    [SerializeField] private GameObject Carta3;
    [SerializeField] private GameObject Carta4;
    [SerializeField] private GameObject Carta5;

    [Header("Prefabs y puntos de aparición")]
    [SerializeField] private List<GameObject> PrefabsCartas; 
    [SerializeField] private List<Transform> PuntosCartas;   

    private List<GameObject> CartasEnJuego = new List<GameObject>(); 
    private List<int> Cartas = new List<int> { 0, 1, 2, 3, 4, 5 };
    private List<int> NumerosCompletos = new List<int> { 0, 1, 2, 3, 4, 5 };

    private int RuidoTotal = 0;
    private int Indice = 0;
    private int CartaSacada;
    public static bool HakoEnTurno = false;
    private void Update()
    {
        if (AccionesJugador.JugadorInvestiga || AccionesJugador.JugadorSeMueve && !HakoEnTurno) 
        {
            CanvasRuido.SetActive(true);
        }
        else
            CanvasRuido.SetActive(false);

        if (RuidoTotal >= 11)
        {
            StartCoroutine(ActivaHakoOnna());
            CanvasRuido.SetActive(false);
            
        }
    }

    public void SeleccionaCarta()
    {
        if (HakoEnTurno) return;
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
        Debug.Log("Sacaste la carta con valor: " + CartaSacada);
        yield return new WaitForSeconds(2f);
        AcumularCartas();
        yield return new WaitForSeconds(3f);
        AccionesJugador.JugadorInvestiga = false;
        AccionesJugador.JugadorSeMueve = false;
        Player.JugadorEnMovimiento = true;
        Mazo.blocksRaycasts = true;
    }

    void AcumularCartas()
    {

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
        CanvasRuido.SetActive(false);       // Oculta el panel de cartas al instante
        Player.JugadorEnMovimiento = false; // Bloquea cualquier movimiento
        AccionesJugador.JugadorInvestiga = false;
        AccionesJugador.JugadorSeMueve = false;
        ItemsAleatorios.Investiga = false;
        Puertas.SeMovera = false;

        //Esperar un breve instante antes de pasar turno (solo para permitir animaciones o efectos)
        yield return new WaitForSeconds(1f);
        Turnos.Instance.NextTurn();
        RuidoTotal = 0;

        //Hako se esconde en una casilla
        ItemsAleatorios[] Casillas = FindObjectsOfType<ItemsAleatorios>();
        int Esconder = Random.Range(0, Casillas.Length);
        Casillas[Esconder].HakoOnnaAqui = true;
        Debug.Log("Hako Onna se ha escondido");

        yield return new WaitForSeconds(1f);
        StartCoroutine(ReiniciarCartas());

        yield return new WaitForSeconds(4f);
        HakoEnTurno = false;
        Player.JugadorEnMovimiento = false; // Mantener bloqueado hasta acción del nuevo jugador
        AccionesJugador.Instance.ActivarBotones(); // El nuevo jugador ya puede elegir acción

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
        

    }
}