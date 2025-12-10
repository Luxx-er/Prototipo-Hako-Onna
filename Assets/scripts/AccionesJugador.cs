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

    [SerializeField] private List<GameObject> Jugadores = new List<GameObject>();
    
    public static AccionesJugador Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    { 
    }
    private void Start()
    {
        Player[] jugadores  = FindObjectsOfType<Player>();

        foreach (Player player in jugadores)
        {
            Jugadores.Add(player.gameObject);
           if (player.playerInTurn)
            {
                Puertas.player = player.transform;
                ItemsAleatorios.player = transform;
            }
        }
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
        Turnos.Instance.NextTurn();
        PanelTurno.SetActive(true);
        Turno.text = "Siguiente Turno";
    }
    public void ActivarBotones()
    {
        Investiga.interactable = true;
        PasaDeTurno.interactable = true;
        SeMueve.interactable = true;           
    }
}
