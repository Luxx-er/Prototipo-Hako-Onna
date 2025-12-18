using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum Actions { INVESTIGAR, AVANZAR, PASAR }

public class Plyr
{
    public string name;
    public Actions currentAction;
    public Plyr(string name)
    {
        this.name = name;
        currentAction = Actions.PASAR;
    }
}

public class Turnos : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Textito;
    public List<Player> playerList = new List<Player>();
    [SerializeField] private Plyr[] players;
    [SerializeField] GameObject Derrota;
    public GameManager gameManager;
    public int currentPlayer = 0;

    public static Turnos Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Player[] Personajes = FindObjectsOfType<Player>();
        foreach (Player personajes in Personajes)
        {
            playerList.Add(personajes);
        }

        players = new Plyr[]
        {
            new Plyr("Player 1"),
            new Plyr("Player 2"),
            new Plyr("Player 3"),
            new Plyr("Player 4")
        };

        Debug.Log("Turno de " + players[currentPlayer].name);
        TurnCamera();
        ActualizarInventariosUI();
    }

    private void TurnCamera()
    {
        foreach (Player p in playerList)
        {
            p.playerInTurn = false;
        }
        playerList[currentPlayer].playerInTurn = true;
    }

    public void NextTurn()
    {
        Textito.text = "";
        if (playerList == null || playerList.Count == 0)
        {
            Debug.LogWarning("No hay jugadores en la lista. No se puede avanzar el turno.");
            return;
        }
        playerList.RemoveAll(p => p == null);

        if (playerList.Count == 0)
        {
            Debug.Log("Todos los jugadores han muerto. Fin del juego.");
            return;
        }
        currentPlayer++;
        if (currentPlayer >= playerList.Count)
            currentPlayer = 0;
        TurnCamera();
        ActualizarInventariosUI();
        AccionesJugador.Instance.ActivarBotones();
        CartasRuido.YaEligio = false;
        Debug.Log($"Turno de {playerList[currentPlayer].name}");
    }

    public void ActualizarInventariosUI()
    {
        foreach (Player p in playerList)
        {
            Inventario inv = p.GetComponent<Inventario>();
            if (inv != null)
            {
                // Mostrar solo el inventario del jugador en turno
                bool activo = p.playerInTurn;
                inv.ActivarUI(activo);
            }
        }
    }
}
