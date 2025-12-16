using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public bool PlayerInRoom;
    public static bool ItemsInRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Player>().playerInTurn)
        {
            PlayerInRoom = true;
            if(PlayerInRoom)
            {
                ItemsInRoom = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Player>().playerInTurn)
        {
            PlayerInRoom = false;
        }
        if(!PlayerInRoom)
        {
            ItemsInRoom = false;
        }
    }

}
