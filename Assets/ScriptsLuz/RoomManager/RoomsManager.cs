using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    public bool PlayerInRoom;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if( collision.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Player>().playerInTurn  )
        {
            PlayerInRoom = true;
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if(collision.gameObject.GetComponent<Player>().playerInTurn)
    //    {
    //        PlayerInRoom = true;
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Player>().playerInTurn)
        {
            PlayerInRoom = false;

        }
    }
}
