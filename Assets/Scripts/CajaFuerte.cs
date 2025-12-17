using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CajaFuerte : MonoBehaviour
{
    [Header("Texto UI")]
    [SerializeField] TextMeshProUGUI Digito1; //Textos que se actualizan 
    [SerializeField] TextMeshProUGUI Digito2;
    [SerializeField] TextMeshProUGUI Digito3;
    [SerializeField] TextMeshProUGUI Textito;

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> PrefabsDisponibles; //Numeros de la caja fuerte
    public List<GameObject> PrefabsRestantes = new List<GameObject>(); //Lista vacia mete los numeros que sobran para encontrarlos en el mapa
    private bool LlavesConseguidas = false;

    int D1, D2, D3; //D= Digito
    int C1, C2, C3; //C= Codigo

    List<int> Digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    List<int> DigitosCompletos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //Cuando recargas la escena, salga un codigo aleatorio

    public static bool CajaAbierta = false; //Booleana falsa para cuando desbloques la caja fuerte te de el item y se vuelva verdadera
    [SerializeField] private GameObject CanvasCajaFuerte;
    void Start()
    {
        CodigoRandom(); //Cuando inica sale el codigo random 
    }
    public void MasD1() { if (D1 < 9) { D1++; Digito1.text = D1.ToString(); } }
    public void MenosD1() { if (D1 > 0) { D1--; Digito1.text = D1.ToString(); } }
    public void MasD2() { if (D2 < 9) { D2++; Digito2.text = D2.ToString(); } }
    public void MenosD2() { if (D2 > 0) { D2--; Digito2.text = D2.ToString(); } }
    public void MasD3() { if (D3 < 9) { D3++; Digito3.text = D3.ToString(); } }
    public void MenosD3() { if (D3 > 0) { D3--; Digito3.text = D3.ToString(); } }

    public void Abrir()
    {
       StartCoroutine(AbrirCaja());
    }
    IEnumerator AbrirCaja()
    {
        GameObject Llaves = ControladorItems.Instance.Llaves;
        int[] entrada = { D1, D2, D3 };
        int[] codigo = { C1, C2, C3 };

        bool correcta = entrada.OrderBy(x => x).SequenceEqual(codigo.OrderBy(x => x));

        if (correcta && !LlavesConseguidas)
        {
            Textito.text = "Clave correcta. Llaves conseguidas.";
            LlavesConseguidas = true;

            // Buscar al jugador actual
            Player jugadorActual = null;
            foreach (Player p in FindObjectsOfType<Player>())
            {
                if (p.playerInTurn)
                {
                    jugadorActual = p;
                    break;
                }
            }

            // Agregar llaves al inventario
            if (jugadorActual != null)
            {
                Inventario inventario = jugadorActual.GetComponent<Inventario>();
                if (inventario != null)
                {
                    inventario.AgregarItem(Llaves);
                    Debug.Log($"{jugadorActual.name} obtuvo las Llaves.");
                }
            }

            CanvasCajaFuerte.SetActive(false);
            yield return new WaitForSeconds(2f);
        }
        else if (correcta && LlavesConseguidas)
        {
            Textito.text = "La caja fuerte ya está vacía.";
            CanvasCajaFuerte.SetActive(false);
            yield return new WaitForSeconds(2f);
        }
        else
        {
            Textito.text = "Clave incorrecta... pierdes turno.";
            CanvasCajaFuerte.SetActive(false);
            yield return new WaitForSeconds(2f);
        }
        Player.JugadorEnMovimiento = false;
        Player.PuedeInteractuar = false;
        ItemsAleatorios.Investiga = false;
        CartasRuido.YaEligio = false;

        Turnos.Instance.NextTurn();
    }


    void CodigoRandom()
    {
      
        List<int> copia = new List<int>(Digitos); //Generas una copia de la lista de digitos 

        int rand = Random.Range(0, copia.Count);
        C1 = copia[rand]; copia.RemoveAt(rand);  //Se guarda en numero en el Codigo1 y se elimina de la lista para que no se vuelva a generar en el siguiente 

        rand = Random.Range(0, copia.Count);
        C2 = copia[rand]; copia.RemoveAt(rand);

        rand = Random.Range(0, copia.Count);
        C3 = copia[rand]; copia.RemoveAt(rand);

        Debug.Log($" Código generado: {C1}{C2}{C3}");


        PrefabsRestantes = new List<GameObject>(PrefabsDisponibles); //Lista que guarda los numeros que quedan de la copia lista 

        EliminarPrefabsDeCodigo(new int[] { C1, C2, C3 });

        ControladorItems.Instance.CargarItemsExternos(PrefabsRestantes);


    }

    void EliminarPrefabsDeCodigo(int[] codigo)
    {
        foreach (int num in codigo)
        {
            if (num >= 0 && num < PrefabsRestantes.Count)
            {
                var prefabAEliminar = PrefabsDisponibles[num];
                PrefabsRestantes.Remove(prefabAEliminar);
            }
        }
    }
    public List<GameObject> AgregarItems()
    {
        return PrefabsRestantes;
    }
}