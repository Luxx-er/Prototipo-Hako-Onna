using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class CartasRuido : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI TextoDecantidad;
    [SerializeField] GameObject CanvasRuido;
    List<int> Cartas = new List<int> { 0, 1, 2, 3, 4, 5 };
    List<int> NumerosCompletos = new List<int> { 0, 1, 2, 3, 4, 5 };
    int RuidoTotal = 0;



    private void Update()
    {
        if (RuidoTotal >= 11)
        {
            StartCoroutine(ActivaHakoOnna());
        }
    }




    public void SacarCarta()
    {
        if (RuidoTotal <= 10)
        {
            int CartaRandom = Random.Range(0, Cartas.Count);
            int CartaSacada = Cartas[CartaRandom];
            Cartas.RemoveAt(CartaRandom);
            RuidoTotal += CartaSacada;
            TextoDecantidad.text = RuidoTotal.ToString();
            Debug.Log("Sacaste" + CartaSacada);
        }

    }
    IEnumerator ActivaHakoOnna()
    {
        if (RuidoTotal >= 11)
        {
            Debug.Log("Hako Activada");
            yield return new WaitForSeconds(2f);
            CanvasRuido.SetActive(false);

        }
    }
}
// Start is called before the first frame update

