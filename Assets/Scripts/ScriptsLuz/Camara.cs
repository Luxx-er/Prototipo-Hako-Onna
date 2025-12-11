using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] private List<RoomsManager> Rooms = new List<RoomsManager>();
    [SerializeField] private CinemachineVirtualCamera follows;
    [SerializeField] private Turnos turnos;

    // Update is called once per frame
    void Update()
    {
        follows.Follow = turnos.playerList[turnos.currentPlayer].transform;
    }
}
