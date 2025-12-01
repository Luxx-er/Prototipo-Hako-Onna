using UnityEngine;

public class ItemsAleatorios : MonoBehaviour
{
    public Transform player;
    public float Radio = 1.3f;
    private bool jugadorEnRango = false;
    private bool Obtenido = false;
    public bool HakoOnnaAqui = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distancia = Vector2.Distance(transform.position, player.position);
        jugadorEnRango = distancia < Radio;

        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !Obtenido)
        {
            if (HakoOnnaAqui == false)
            {
                DarItem();
            }else
            {
                InvocarHakoOnna();
            }
            
        }
    }

    void DarItem()
    {
        GameObject prefabItem = ControladorItems.Instance.ObtenerItemAleatorio();

        if (prefabItem != null)
        {
            Instantiate(prefabItem, transform.position, Quaternion.identity);
            Debug.Log("Has encontrado: " + prefabItem.name);
            Obtenido= true; 
        }
        else
        {
            Debug.Log("No hay ítems disponibles o ya revisaste todo.");
        }
    }

    void InvocarHakoOnna()
    {
        GameObject Hako = ControladorItems.Instance.Caballo;
        Instantiate(Hako, transform.position, Quaternion.identity);
        Debug.Log("CAGASTE");

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radio);
    }
}