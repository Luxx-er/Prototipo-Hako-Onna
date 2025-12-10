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

    [Header("Prefabs y puntos de aparición")]
    [SerializeField] private List<GameObject> PrefabsCartas; 
    [SerializeField] private List<Transform> PuntosCartas;   

    private List<GameObject> CartasEnJuego = new List<GameObject>(); 
    private List<int> Cartas = new List<int> { 0, 1, 2, 3, 4, 5 };
    private List<int> NumerosCompletos = new List<int> { 0, 1, 2, 3, 4, 5 };

    private int RuidoTotal = 0;
    private int Indice = 0;
    private int CartaSacada;

    private void Update()
    {
        if (AccionesJugador.JugadorInvestiga || AccionesJugador.JugadorSeMueve) 
        {
            CanvasRuido.SetActive(true);
        }
        else
            CanvasRuido.SetActive(false);

        if (RuidoTotal >= 11)
        {
            StartCoroutine(ActivaHakoOnna());
            StartCoroutine(ReiniciarCartas());
        }
    }

    public void SeleccionaCarta()
    {
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
        TextoDecantidad.text = RuidoTotal.ToString();
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
        if (Indice >= PuntosCartas.Count)
        {
            Debug.LogWarning("Ya no hay más espacios para cartas en mano.");
            return;
        }

        GameObject prefab = PrefabsCartas[CartaSacada];
        Transform punto = PuntosCartas[Indice];

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
        ItemsAleatorios[] Casillas = FindObjectsOfType<ItemsAleatorios>();
        int Esconder = Random.Range(0, Casillas.Length);
        Casillas[Esconder].HakoOnnaAqui = true;
        Debug.Log("Hako Activada");
        CanvasRuido.SetActive(false);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ReiniciarCartas()
    {
        foreach (var carta in CartasEnJuego)
        {
            if (carta != null)
                Destroy(carta);
        }
        CartasEnJuego.Clear();

        Indice = 0;
        RuidoTotal = 0;
        Cartas.Clear();
        Cartas.AddRange(NumerosCompletos);
        TextoDecantidad.text = RuidoTotal.ToString();
        Player.JugadorEnMovimiento = false;

        yield return new WaitForSeconds(1f);
    }
}