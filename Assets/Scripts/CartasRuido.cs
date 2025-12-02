using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class CartasRuido : MonoBehaviour
{ 

    [SerializeField] private TextMeshProUGUI TextoDecantidad;
    [SerializeField] GameObject CanvasRuido;
    List<int> Cartas = new List<int> { 0, 1, 2, 3, 4, 5};
    List<int> NumerosCompletos = new List<int> { 0, 1, 2, 3, 4, 5 };
    int RuidoTotal = 0;
    [SerializeField] GameObject Carta0;
    [SerializeField] GameObject Carta1;
    [SerializeField] GameObject Carta2;
    [SerializeField] GameObject Carta3;
    [SerializeField] GameObject Carta4;
    [SerializeField] GameObject Carta5;


    private void Update()
    {
        if (AccionesJugador.JugadorInvestiga == true || AccionesJugador.JugadorSeMueve == true )
        {
            CanvasRuido.SetActive(true);
        }
        else
        {
            CanvasRuido.SetActive(false);
            
        }
        if (RuidoTotal >= 11)
        {
            StartCoroutine(ActivaHakoOnna());
            StartCoroutine(ReiniciarCartas());
            
        }
    }
    public void SeleccionaCarta()
    {
        StartCoroutine (SacarCarta());
    }
    IEnumerator SacarCarta()
    {
        CanvasRuido.SetActive(true);
        int CartaRandom = Random.Range(0, Cartas.Count);
        int CartaSacada = Cartas[CartaRandom];
        Cartas.RemoveAt(CartaRandom);
        RuidoTotal += CartaSacada;
        TextoDecantidad.text = RuidoTotal.ToString();
        Debug.Log("Sacaste" + CartaSacada);
        yield return new WaitForSeconds(3f);
        AccionesJugador.JugadorInvestiga = false;
        AccionesJugador.JugadorSeMueve = false;
        Player.JugadorEnMovimiento = true;

    }
    IEnumerator ActivaHakoOnna()
    {
            
        Debug.Log("Hako Activada");   
        CanvasRuido.SetActive(false);
        yield return new WaitForSeconds(1f);
    }
    IEnumerator ReiniciarCartas()
    {
        RuidoTotal = 0;
        Cartas.Clear();
        Cartas.AddRange(NumerosCompletos);
        yield return new WaitForSeconds(1f);
        Carta0.SetActive(true);
        Carta1.SetActive(true);
        Carta2.SetActive(true);
        Carta3.SetActive(true);
        Carta4.SetActive(true);
        Carta5.SetActive(true);
        TextoDecantidad.text = RuidoTotal.ToString();
        Player.JugadorEnMovimiento = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
}
