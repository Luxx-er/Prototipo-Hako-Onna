using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CartasRuido : MonoBehaviour
{ 

    [SerializeField] private TextMeshProUGUI textoDecantidad;
    [SerializeField] private int cantidad;
    [SerializeField] private int cantidadMaxima;
    public GameObject Cartastodo;
    public void Sumar(int cantidadDeEntrada)
    {
        if (cantidad >= cantidadMaxima)
        {
            desactivarcanvas();

            return;
        }


        cantidad += cantidadDeEntrada;
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textoDecantidad.text = cantidad.ToString();
    }
    public void desactivarcanvas()
    {
        StartCoroutine(desaparecercanvas());
    }

    public IEnumerator desaparecercanvas()
    {
        yield return new WaitForSeconds(3f);
        Cartastodo.SetActive(false);
    }

    public GameObject Carta;
    // Start is called before the first frame update
    public void Desaparecer()
    {
        StartCoroutine(desactivar());
    }

    // Update is called once per frame
    public IEnumerator desactivar()
    {
        yield return new WaitForSeconds(2f);
        Carta.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
