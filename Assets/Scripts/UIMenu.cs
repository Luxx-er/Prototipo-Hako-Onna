using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public AudioSource Boton;
    public void Jugar()
    {
        Boton.Play();
        StartCoroutine(CambioEscena());
    }
    public void Siguiente()
    {
        SceneManager.LoadScene("InterfazPrincipal");
    }
    IEnumerator CambioEscena()
    {
        
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("CantJugadores");
    }
    public void Salir()
    {
        Boton.Play();
        Application.Quit();
        Debug.Log("Saliste del juego");
    }
    public void VolverAJugar()
    {
        SceneManager.LoadScene("CantJugadores");
    }
    public void SuicidioColectivo()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
    public void Reglamento()
    {
        Boton.Play();
    }
}
