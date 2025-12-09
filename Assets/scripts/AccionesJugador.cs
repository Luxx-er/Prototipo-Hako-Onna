using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    public static bool TurnoTerminado = false;

    private void Update()
    {
        
    }
    public void MoveOrInvestigate()
    {

        Debug.Log("Panel de Cartas");
        JugadorInvestiga = true;
        ItemsAleatorios.Investiga = true; //booleana referenciando el codigo de ItemsAleatorios
        Investiga.interactable = false; //No puedes interactuar con los botones porque se vuelven falsos 
        PasaDeTurno.interactable = false; 
        SeMueve.interactable = false;
    }
    public void CambiaDeCuarto()
    {

        Debug.Log("Panel de Cartas");
        JugadorSeMueve = true;
        Puertas.SeMovera = true;  //booleana referenciando el codigo de Puertas
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
        
        TurnoTerminado = false;
        PanelTurno.SetActive(true);
        Turno.text = "Siguiente Turno";
    }
}
