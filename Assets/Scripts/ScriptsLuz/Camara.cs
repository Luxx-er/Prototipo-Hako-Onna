using Cinemachine;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] private List<RoomsManager> Rooms = new List<RoomsManager>();
    [SerializeField] private CinemachineVirtualCamera follows;
    [SerializeField] private Turnos turnos;

    private void Update()
    {
        if (turnos == null || turnos.playerList == null || turnos.playerList.Count == 0)
            return;
        if (turnos.playerList[turnos.currentPlayer] == null)
        {
            Debug.LogWarning("El jugador actual fue destruido. Saltando al siguiente turno...");
            turnos.NextTurn();
            return;
        }
        Transform target = turnos.playerList[turnos.currentPlayer].transform;

        if (follows != null && follows.Follow != target)
            follows.Follow = target;
    }
}

