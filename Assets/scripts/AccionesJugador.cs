using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AccionesJugador : MonoBehaviour
{
    public static bool JugadorInvestiga = false;
    public static bool JugadorSeMueve = false;
    [SerializeField] private TextMeshProUGUI SaltasteTurno;
    [SerializeField] private GameObject PanelTurno;
    [SerializeField] private TextMeshProUGUI Turno;
    [SerializeField] public Button SeMueve;
    [SerializeField] public Button PasaDeTurno;
    [SerializeField] public Button Investiga;

    //[SerializeField] private Button MoveInvestigateButton;
    //[SerializeField] private GameObject PanelNoiseCards;

    //public void MoveAndInvestigate()
    //{

    //}

    public void MoveOrInvestigate()
    {

        Debug.Log("Panel de Cartas");
        JugadorInvestiga = true;
        ItemsAleatorios.Investiga = true;
        Investiga.interactable = false;
        PasaDeTurno.interactable = false;
        SeMueve.interactable = false;
    }
    public void CambiaDeCuarto()
    {

        Debug.Log("Panel de Cartas");
        JugadorSeMueve = true;
        Investiga.interactable = false;
        PasaDeTurno.interactable = false;
        SeMueve.interactable = false;
    }
    public void SiguienteTurno()
    {
        Debug.Log("SaltasteTurno SIGUIENTE TURNO");
        Investiga.interactable = false;
        PasaDeTurno.interactable = false;
        SeMueve.interactable = false;
        PanelTurno.SetActive(true);
        Turno.text = "Siguiente Turno";
    }
}
