using System.Collections;
using System.Collections.Generic;
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
    public List<Player> playerList = new List<Player>();
    [SerializeField] private Plyr[] players;
    public int currentPlayer = 0;

    public static Turnos Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Player[] Personajes = FindObjectsOfType<Player>();
        foreach(Player personajes in Personajes)
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
        Debug.Log("Turno de" + players[currentPlayer].name);
        TurnCamera();

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
        if (currentPlayer == 4) {
            currentPlayer = 0;
        }
        currentPlayer = (currentPlayer + 1) % players.Length;
        Debug.Log("Turno de" + players[currentPlayer].name);
        TurnCamera();
        AccionesJugador.Instance.ActivarBotones();
    }
        


}
