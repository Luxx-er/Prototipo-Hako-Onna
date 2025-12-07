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

    [Header("Prefabs")]
    [SerializeField] private List<GameObject> PrefabsDisponibles; //Numeros de la caja fuerte
    public List<GameObject> PrefabsRestantes = new List<GameObject>(); //Lista vacia mete los numeros que sobran para encontrarlos en el mapa

    int D1, D2, D3; //D= Digito
    int C1, C2, C3; //C= Codigo
    bool correcta = false;

    List<int> Digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    List<int> DigitosCompletos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //Cuando recargas la escena, salga un codigo aleatorio

    public static bool CajaAbierta = false; //Booleana falsa para cuando desbloques la caja fuerte te de el item y se vuelva verdadera
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
        GameObject Llaves = ControladorItems.Instance.Llaves;  //Referenciando al codigo ControladorItems 
        int[] entrada = { D1, D2, D3 };
        int[] codigo = { C1, C2, C3 };

        if (entrada.OrderBy(x => x).SequenceEqual(codigo.OrderBy(x => x))) //Acomoda los numeros de menor a mayor, los compara y aunque 
        {
            correcta = true;
        }

        if (correcta)
        {
                Debug.Log("Contraseña correcta");
                Instantiate(Llaves, transform.position, Quaternion.identity); //Instancia las llaves y cambia las escalas para ponerlas en el inventario 
                Inventario inventario = FindObjectOfType<Inventario>(); //Referencias al codigo del inventario 
                if (inventario != null)
                {
                    inventario.AgregarItem(Llaves); //Referencias la funcion del codigo del inventario y agregas las llaves
                }

                Debug.Log("Has conseguido las " + Llaves.name);
        }
        else
        {
            Debug.Log("Contraseña incorrecta");
        }
    }

    void CodigoRandom()
    {
      
        List<int> copia = new List<int>(Digitos); //Generas una copia de la lista de digitos 

        int rand = Random.Range(0, copia.Count);
        C1 = copia[rand]; copia.RemoveAt(rand);

        rand = Random.Range(0, copia.Count);
        C2 = copia[rand]; copia.RemoveAt(rand);

        rand = Random.Range(0, copia.Count);
        C3 = copia[rand]; copia.RemoveAt(rand);

        Debug.Log($" Código generado: {C1}{C2}{C3}");


        PrefabsRestantes = new List<GameObject>(PrefabsDisponibles);

        EliminarPrefabsDeCodigo(new int[] { C1, C2, C3 });

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