using System.Collections.Generic;
using UnityEngine;

public class ControladorItems : MonoBehaviour
{
    public static ControladorItems Instance;

    [Header("Prefabs de ítems y otros")]
    public List<GameObject> Items;
    public GameObject Tache;
    public GameObject Muerte;
    public GameObject Llaves;

    [Header("Debilidades de Hako Onna")]
    public List<GameObject> Debilidades = new List<GameObject>(); // Las 3 posibles
    public GameObject DebilidadDeHako; // La verdadera debilidad de esta partida

    private void Start()
    {
        GenerarDebilidadHako();
    }
    private void Awake()
    {
        Instance = this;
    }
    public GameObject ObtenerItemAleatorio()
    {

        int index = Random.Range(0, Items.Count);
        GameObject itemSeleccionado = Items[index];

        //Eliminarlo de la lista para que no se repita
        Items.RemoveAt(index);
        Items.Add(Tache);
        return itemSeleccionado;
    }
    public void GenerarDebilidadHako()
    {

        //Crear una copia temporal de la lista
        List<GameObject> copia = new List<GameObject>(Debilidades);

        //Elegir una debilidad real al azar
        int index = Random.Range(0, copia.Count);
        DebilidadDeHako = copia[index];
        copia.RemoveAt(index);

        //Las 2 restantes se agregan a los ítems disponibles
        foreach (GameObject debilidadFalsa in copia)
        {
            if (!Items.Contains(debilidadFalsa))
            {
                Items.Add(debilidadFalsa);
            }
        }

        Debug.Log($"Debilidad de Hako Onna: {DebilidadDeHako.name}");
    }


    //Reinicia todas las casillas abiertas y cambia sprites
    public void ReiniciarCasillas()
    {
        ItemsAleatorios[] todasLasCasillas = FindObjectsOfType<ItemsAleatorios>();
        List<ItemsAleatorios> disponibles = new List<ItemsAleatorios>();
        // Limpia marcas de Hako Onna
        foreach (ItemsAleatorios casilla in todasLasCasillas)
            casilla.HakoOnnaAqui = false;

        foreach (ItemsAleatorios casilla in todasLasCasillas)
        {
            // Si está bloqueada permanentemente (ítem especial), no tocarla
            if (casilla.CasillaBloqueadaPermanente)
                continue;

            // Reiniciar casillas normales o Tachesote
            if (casilla.Obtenido)
            {
                casilla.Obtenido = false;
                ItemsAleatorios.Investiga = false;

                foreach (Transform hijo in casilla.transform)
                {
                    if (hijo.name.Contains("X")) // eliminar taches
                        Destroy(hijo.gameObject);
                }
            }

            if (!casilla.Obtenido)
                disponibles.Add(casilla);
        }

        //  Hako Onna se esconde en una casilla disponible
        if (disponibles.Count > 0)
        {
            ItemsAleatorios nuevaCasilla = disponibles[Random.Range(0, disponibles.Count)];
            nuevaCasilla.HakoOnnaAqui = true;
            Debug.Log("Hako Onna se escondió");
        }
        else
        {
            Debug.LogWarning("No hay casillas disponibles para esconder a Hako Onna.");
        }

        Debug.Log("Casillas normales reiniciadas correctamente.");
    }

    public void CargarItemsExternos(List<GameObject> items)
    {
        Items.AddRange(items);
    }

}
