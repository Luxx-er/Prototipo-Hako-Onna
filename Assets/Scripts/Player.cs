using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float Speed = 5f;
    [SerializeField] private float tileSize = 1f; // Tamaño del tile
    private Rigidbody2D rb;
    private bool HasInput;
    [SerializeField] public float DistanciaDeSalto;
    [SerializeField] private LayerMask SolidObjectLayer;
    [SerializeField] private float checkRadius = 0.2f;
    private Vector2 targetPos;
    private Vector2 inputDir;

    public static bool JugadorEnMovimiento = false;
    public static bool Muevete = false;
    public static bool PuedeInteractuar = false;
    public static bool Quieto = true;
    public bool playerInTurn;
    public static bool JugadorVivo = true;

    public AudioSource Pasos;
    public static Player Instance { get; private set; }

    [SerializeField] private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;

        Vector2 p = transform.position;
        rb.position = new Vector2(Mathf.Round(p.x / tileSize) * tileSize, Mathf.Round(p.y / tileSize) * tileSize);
        targetPos = rb.position;

        Instance = this;
    }

    void Update()
    {
        if (!HasInput && JugadorEnMovimiento && playerInTurn && JugadorVivo)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(v) > 0.01f)
            {
                inputDir = new Vector2(0, Mathf.Sign(v));
                Quieto = false;
            }
            else if (Mathf.Abs(h) > 0.01f)
            {
                inputDir = new Vector2(Mathf.Sign(h), 0);
                Quieto = false;
            }
            else
            {
                inputDir = Vector2.zero;
                Quieto = true;
                Pasos.Stop();
            }

            if (inputDir != Vector2.zero)
            {
                Vector2 target = (Vector2)transform.position + inputDir * tileSize;

                bool blocked = false;
                Collider2D[] hits = Physics2D.OverlapCircleAll(target, checkRadius, SolidObjectLayer);
                foreach (Collider2D s in hits)
                {
                    if (s.gameObject != gameObject)
                    {
                        blocked = true;
                        break;
                    }
                }

                if (!blocked)
                {
                    targetPos = target;
                    HasInput = true;
                    if (!Pasos.isPlaying)
                        Pasos.Play();

                    if (animator)
                    {
                        animator.SetFloat("MoveX", inputDir.x);
                        animator.SetFloat("MoveY", inputDir.y);
                        animator.SetBool("IsMoving", true);
                    }
                }
                else
                {
                    if (animator)
                    {
                        animator.SetFloat("MoveX", inputDir.x);
                        animator.SetFloat("MoveY", inputDir.y);
                        animator.SetBool("IsMoving", false);
                    }
                }
            }
        }

        PuedeInteractuar = true;
    }

    private void FixedUpdate()
    {
        if (HasInput)
        {
            float step = Speed * Time.fixedDeltaTime;
            Vector2 newPos = Vector2.MoveTowards(rb.position, targetPos, step);
            rb.MovePosition(newPos);

            if (Vector2.SqrMagnitude(newPos - targetPos) < 0.0001f)
            {
                rb.MovePosition(targetPos);
                HasInput = false;

                if (animator)
                    animator.SetBool("IsMoving", false);
            }
        }
    }
}
