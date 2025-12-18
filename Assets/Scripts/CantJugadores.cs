using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CantidadJugadores : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Cantidad;
    [SerializeField] GameObject Jugador3;
    [SerializeField] GameObject Jugador4;
    public AudioSource MusicaGeneral;
    public AudioSource Boton;
    public AudioSource Agudo;
    public AudioSource Grave;
    public bool Limite = false;
    public bool LimiteInferior = true;
    int Cant = 2;
    public static int CantFinal;
    public static CantidadJugadores Instance { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(MusicaGeneral);
        Instance = this;
    }
    public void MasJugadores()
    {
        if (Cant < 4)
        {
            Cant++; Cantidad.text = Cant.ToString();
        }
        if (Cant == 3)
        {
            StartCoroutine(TiemposMas());
            LimiteInferior = false;
        }
        if (Cant == 4)
        {
            StartCoroutine(TiemposMas());
            Limite = true;
        }
    }
    public void MenosJugadores()
    {
        if (Cant > 2)
        {
            Cant--; Cantidad.text = Cant.ToString();
        }
        if (Cant < 3)
        {
            StartCoroutine(TiemposMenos());
            LimiteInferior = true;
        }
        if (Cant < 4)
        {
            StartCoroutine(TiemposMenos());
            Limite = false;
        }
    }
    
    IEnumerator TiemposMas()
    {
        if (Cant == 3)
        {
            Agudo.Play();
            yield return new WaitForSeconds(0.5f);
            Jugador3.SetActive(true);
        }
        if (Cant == 4 && !Limite)
        {
            Agudo.Play();
            yield return new WaitForSeconds(0.5f);
            Jugador4.SetActive(true);

        }
    }
    IEnumerator TiemposMenos()
    {
        if (Cant == 2 && !LimiteInferior)
        {
            Grave.Play();
            yield return new WaitForSeconds(0.5f);
            Jugador3.SetActive(false);
        }
        if (Cant == 3)
        {
            Grave.Play();
            yield return new WaitForSeconds(0.5f);
            Jugador4.SetActive(false);
        }

    }
    public void Siguiente()
    {
        Boton.Play();
        CantFinal = Cant;
        StartCoroutine(CambioEscenaPrincipal());
        Debug.Log("cantidad es igual a " +  CantFinal);
    }
    IEnumerator CambioEscenaPrincipal()
    {

        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("InterfazPrincipal");
    }
}
