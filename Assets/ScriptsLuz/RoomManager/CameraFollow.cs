using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private List<RoomsManager> rooms = new List<RoomsManager>();
    [SerializeField] private CinemachineVirtualCamera follows;

    private void Update()
    {
        foreach (var item in rooms)
        {
            if (item.PlayerInRoom)
            {
                follows.Follow = item.gameObject.transform;
            }
        }
    }
}
