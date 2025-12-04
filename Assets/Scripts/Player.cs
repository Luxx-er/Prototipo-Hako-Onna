using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float tileSize = 1f; //Tamaño del tile 
    private Rigidbody2D rb;
    private bool HasInput; //Tiene input

    [SerializeField] private LayerMask SolidObjectLayer; //Capa para todos los solidos y objetos
    [SerializeField] private float checkRadius = 0.2f;
    private Vector2 targetPos;

    private Vector2 inputDir;
    public static bool JugadorEnMovimiento = false;
    public static bool Muevete = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        Vector2 p = transform.position;
        rb.position = new Vector2(Mathf.Round(p.x / tileSize) * tileSize, Mathf.Round(p.y / tileSize) * tileSize);
        targetPos = rb.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasInput && JugadorEnMovimiento)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(v) > 0.01f)
            {
                inputDir = new Vector2(0, Mathf.Sign(v)); //si es positivo regresa a 1 si es negativo regresa a -1
            }
            else if (Mathf.Abs(h) > 0.01f)
            {
                inputDir = new Vector2(Mathf.Sign(h), 0);
            }
            else
            {
                inputDir = Vector2.zero;  //los dos valores son 0 
            }
            if (inputDir != Vector2.zero)
            {
                Vector2 target = (Vector2)transform.position + inputDir * tileSize;
                bool blocked = Physics2D.OverlapCircle(target, checkRadius, SolidObjectLayer) != null;

                if (!blocked) //SI no estoy bloqueado
                {
                    targetPos = target;
                    HasInput = true;

                    //Animator goes here

                }
            }

        }


    }
    private void FixedUpdate()
    {
        if (HasInput)
        {
            float step = Speed * Time.fixedDeltaTime;
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, step);
            rb.MovePosition(newPos);

            if (Vector2.SqrMagnitude(newPos - targetPos) < 0.0001f) //Magnitud cuadrada 
            {
                rb.MovePosition(targetPos);
                HasInput = false; //Te dejas de mover 
            }

        }
    }
}
