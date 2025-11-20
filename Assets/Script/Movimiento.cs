using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float speed = 50f;
    [SerializeField] private float tileSize = 1f;

    private Rigidbody2D rb;
    public Vector2 targetPos;


    public GameObject player;

    //para que el personaje sepa moverse en el grid/timemap
    private void Awake()
    {
        //rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = true;
        //Vector2 p = transform.position;
        //rb.position = new Vector2(Mathf.Round(p.x / tileSize) * tileSize, Mathf.Round(p.y / tileSize) * tileSize);
        //targetPos = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        //esto sirve para poder generar la posicion del jugador y que el click del mouse lo pueda mover

        Vector2 myPosition = transform.position;

        float distance = Vector2.Distance(myPosition, targetPos);
        Debug.Log(distance);
        //para que el juego sepa que los clicks del mouse los pueda moverS
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosScreen = Input.mousePosition;
            targetPos = Camera.main.ScreenToWorldPoint(mousePosScreen);

        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

    }
}
