using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private List<RoomsManager> rooms = new List<RoomsManager>();
    [SerializeField] private CinemachineVirtualCamera follows;

    [SerializeField] private Turns turns;

    private void Update()
    {
        
                follows.Follow = turns.playerList[turns.currentPlayer].transform;
        
    }
}
