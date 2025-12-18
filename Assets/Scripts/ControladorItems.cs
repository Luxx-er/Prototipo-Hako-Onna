using System.Collections.Generic;
using UnityEngine;

public class ControladorItems : MonoBehaviour
{
    public static ControladorItems Instance;

    [Header("Prefabs de ítems y otros")]
    public List<GameObject> Items;
    public GameObject Tache;
    public GameObject Muerte;
    public GameObject MuerteHakobito;
    public GameObject Derrota;
    public GameObject Llaves;
    public AudioSource ScreamerCegua;
    public AudioSource ScreamerCeguita;
    public AudioSource Victoria;
    public AudioSource Moriste;

    [Header("Debilidades de Hako Onna")]
    public List<GameObject> Debilidades = new List<GameObject>(); // Las 3 posibles debilidades
    public GameObject DebilidadDeHako; // La verdadera debilidad de esta partida
    private List<ItemsAleatorios> hakobitosActivos = new List<ItemsAleatorios>();


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

        foreach (ItemsAleatorios casilla in todasLasCasillas)
            casilla.HakoOnnaAqui = false;

        foreach (ItemsAleatorios casilla in todasLasCasillas)
        {
            if (casilla.CasillaBloqueadaPermanente)
                continue;

            if (casilla.Obtenido)
            {
                casilla.Obtenido = false;
                ItemsAleatorios.Investiga = false;

                if (casilla.animator != null)
                {
                    casilla.animator.SetBool("Obtenido", false);
                }

                foreach (Transform hijo in casilla.transform)
                {
                    if (hijo.name.Contains("X") || hijo.CompareTag("Item"))
                        Destroy(hijo.gameObject);
                }
            }

            if (!casilla.Obtenido && !casilla.HakobitoAqui)
                disponibles.Add(casilla);
        }

        if (disponibles.Count > 0)
        {
            ItemsAleatorios nuevaCasilla = disponibles[Random.Range(0, disponibles.Count)];
            nuevaCasilla.HakoOnnaAqui = true;
            Debug.Log("Hako Onna se escondió en una nueva casilla.");
        }
        MoverHakobitos(todasLasCasillas);

        Debug.Log("Casillas normales reiniciadas correctamente.");
    }



    public void EsconderHakobito()
    {
        ItemsAleatorios[] casillas = FindObjectsOfType<ItemsAleatorios>();
        List<ItemsAleatorios> disponibles = new List<ItemsAleatorios>();

        foreach (ItemsAleatorios c in casillas)
        {
            if (!c.Obtenido && !c.CasillaBloqueadaPermanente && !c.HakoOnnaAqui && !c.HakobitoAqui)
            {
                disponibles.Add(c);
            }
        }

        if (disponibles.Count > 0)
        {
            ItemsAleatorios casillaSeleccionada = disponibles[Random.Range(0, disponibles.Count)];
            casillaSeleccionada.HakobitoAqui = true;
            hakobitosActivos.Add(casillaSeleccionada);
            Debug.Log("Un Hakobito se ha escondido");
        }
        else
        {
            Debug.LogWarning("No hay casillas disponibles para esconder un Hakobito.");
        }
    }


    public void MoverHakobitos(ItemsAleatorios[] todasLasCasillas)
    {
        hakobitosActivos.Clear();
        List<ItemsAleatorios> origenes = new List<ItemsAleatorios>();
        foreach (ItemsAleatorios c in todasLasCasillas)
        {
            if (c.HakobitoAqui)
                origenes.Add(c);
        }

        if (origenes.Count == 0)
        {
            Debug.Log("No hay Hakobitos activos en el mapa.");
            return;
        }
        foreach (ItemsAleatorios c in origenes)
            c.HakobitoAqui = false;
        List<ItemsAleatorios> libres = new List<ItemsAleatorios>();
        foreach (ItemsAleatorios c in todasLasCasillas)
        {
            if (!c.Obtenido && !c.CasillaBloqueadaPermanente && !c.HakoOnnaAqui && !c.HakobitoAqui)
                libres.Add(c);
        }

        if (libres.Count == 0)
        {
            Debug.LogWarning("No hay casillas disponibles para mover a los Hakobitos.");
            return;
        }
        foreach (ItemsAleatorios c in origenes)
        {
            if (libres.Count == 0) break;
            ItemsAleatorios destino = libres[Random.Range(0, libres.Count)];
            destino.HakobitoAqui = true;
            hakobitosActivos.Add(destino);
            libres.Remove(destino);

            Debug.Log($"Un Hakobito se escondió");
        }
    }

    public void CargarItemsExternos(List<GameObject> items)
    {
        Items.AddRange(items);
    }

}
