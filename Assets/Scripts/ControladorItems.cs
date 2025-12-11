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

        foreach (ItemsAleatorios casilla in todasLasCasillas)
        {
            // Si la casilla ya fue obtenida, la "reseteamos"
            if (casilla.Obtenido)
            {
                casilla.Obtenido = false;
                casilla.HakoOnnaAqui = false;
                ItemsAleatorios.Investiga = false;

                //Eliminar taches (instancias de prefab Tache)
                foreach (Transform hijo in casilla.transform)
                {
                    if (hijo.name.Contains("X"))
                        Destroy(hijo.gameObject);
                }
            }
            else
            {
                disponibles.Add(casilla);
            }
        }

        //Elegir una casilla para esconder a Hako Onna
        ItemsAleatorios casillaHako = null;

        if (disponibles.Count > 0)
        {
            // Si hay casillas sin abrir, usa una de ellas
            casillaHako = disponibles[Random.Range(0, disponibles.Count)];
        }
        else
        {
            // Si todas estaban abiertas, elige una aleatoria de todas las casillas
            casillaHako = todasLasCasillas[Random.Range(0, todasLasCasillas.Length)];
        }

        casillaHako.HakoOnnaAqui = true;
       
        Debug.Log("Casillas reiniciadas y taches eliminados.");
        Debug.Log("Hako Onna se escondió");
    }
    public void CargarItemsExternos(List<GameObject> items)
    {
        Items.AddRange(items);
    }

}
