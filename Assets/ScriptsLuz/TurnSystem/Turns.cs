using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
public class Turns : MonoBehaviour
{

    [SerializeField]private List<Player> playerList = new List<Player>();
    [SerializeField] private Plyr[] players;
    private int currentPlayer = 0;

    private void Start()
    {
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

        public void ChooseActionInv()
        {
            players[currentPlayer].currentAction = Actions.INVESTIGAR;

            ExecuteAction(players[currentPlayer]);
        }
    public void ChooseActionAva()
    {
        players[currentPlayer].currentAction = Actions.AVANZAR;

        ExecuteAction(players[currentPlayer]);
    }
    public void ChooseActionPas()
    {
        players[currentPlayer].currentAction = Actions.PASAR;

        ExecuteAction(players[currentPlayer]);
    }

    private void ExecuteAction(Plyr player)
        {
            switch (player.currentAction)
            {
                case Actions.INVESTIGAR:
                    Debug.Log(player.name + "investigo");
                NextTurn();
                    break;
                case Actions.AVANZAR:
                    Debug.Log(player.name + "avanzo");
                NextTurn();
                    break;
                case Actions.PASAR:
                    Debug.Log(player.name + "paso turno");
                NextTurn();
                    break;
            }
        
            
        }
        private void NextTurn()
        {
            currentPlayer = (currentPlayer + 1) % players.Length;
            Debug.Log("Turno de" + players[currentPlayer].name);
        TurnCamera();
        }



}
