using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class MapaAleatorio : MonoBehaviour
{
    public List<GameObject> Mapas;
    public GameObject TableroActual = null;

    // Start is called before the first frame update
    void Start()
    {
        int indiceAleatorio = Random.Range(0, Mapas.Count);
        GameObject itemSeleccionado = Mapas[indiceAleatorio];
        TableroActual = Instantiate(itemSeleccionado, transform.position, transform.rotation);

        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            Transform BloqueoListo = TableroActual.transform.GetComponentsInChildren<Transform>(true)
                .FirstOrDefault(t => t.CompareTag("Bloqueos"));
            {
                if (BloqueoListo != null)
                {
                    gameManager.Bloqueo = BloqueoListo.gameObject;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
