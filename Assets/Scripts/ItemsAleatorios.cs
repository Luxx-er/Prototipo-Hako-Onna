using System.Collections;
using UnityEngine;

public class ItemsAleatorios : MonoBehaviour
{
    public static Transform player;
    public float Radio = 1.3f;
    private bool jugadorEnRango = false;
    private bool Obtenido = false;
    public static bool Investiga = false;
    public bool HakoOnnaAqui = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        
        float distancia = Vector2.Distance(transform.position, player.position);
        jugadorEnRango = distancia < Radio;

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !Obtenido && Investiga && Player.PuedeInteractuar)
        {
            if (HakoOnnaAqui == false)
            {
                StartCoroutine(DarItem());
            }else
            {
                InvocarHakoOnna();
            }
            Player.JugadorEnMovimiento = false;
        }
    }

    IEnumerator DarItem()
    {
        GameObject prefabItem = ControladorItems.Instance.ObtenerItemAleatorio();
        GameObject Tache = ControladorItems.Instance.Tache;

        if (prefabItem != null)
        {
            Instantiate(prefabItem, transform.position, Quaternion.identity);
            Debug.Log("Has encontrado: " + prefabItem.name);
            Obtenido = true;
            Investiga = false;
        }
        yield return new WaitForSeconds(2f);
        Inventario inventario = FindObjectOfType<Inventario>();
        if (inventario != null)
        {
            inventario.AgregarItem(prefabItem);
        }
        Instantiate(Tache, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(4f);
        Turnos.Instance.NextTurn();

    }

    void InvocarHakoOnna()
    {
        GameObject Hako = ControladorItems.Instance.Muerte;
        Hako.SetActive(true);
        Debug.Log("CAGASTE");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}