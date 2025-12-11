using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{

    public void Jugar()
    {
        SceneManager.LoadScene("CantJugadores");
    }
    public void Siguiente()
    {
        SceneManager.LoadScene("InterfazPrincipal");
    }

}
