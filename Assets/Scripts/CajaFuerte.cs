using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CajaFuerte : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Digito1;
    [SerializeField] TextMeshProUGUI Digito2;
    [SerializeField] TextMeshProUGUI Digito3;

    int D1, D2, D3;
    int C1, C2, C3;
    bool correcta = false;

    List<int> Digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    List<int> DigitosCompletos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    void Start()
    {
        CodigoRandom();
    }

    public void MasD1() 
    { 
        if (D1 < 9) 
        { 
            D1++; Digito1.text = D1.ToString(); 
        } 
    }
    public void MenosD1() 
    { 
        if (D1 > 0) 
        {
            D1--; Digito1.text = D1.ToString(); 
        } 
    }

    public void MasD2() 
    { 
        if (D2 < 9) 
        { 
            D2++; Digito2.text = D2.ToString(); 
        } 
    }
    public void MenosD2() 
    { 
        if (D2 > 0) 
        { 
            D2--; Digito2.text = D2.ToString(); 
        }
    }

    public void MasD3() 
    { 
        if (D3 < 9) 
        { 
            D3++; Digito3.text = D3.ToString(); 
        } 
    }
    public void MenosD3()
    { 
        if (D3 > 0) 
        { 
            D3--; Digito3.text = D3.ToString(); 
        } 
    }

    public void Abrir()
    {
        int[] entrada = { D1, D2, D3 };
        int[] codigo = { C1, C2, C3 };

        if (entrada.OrderBy(x => x).SequenceEqual(codigo.OrderBy(x => x)))
        {
            correcta = true;
        }
        if (correcta == true)
            {
                Debug.Log("Contraseña Correcta");
            }
            else if(correcta == false)
            {
                Debug.Log("Contraseña Incorrecta");
            }
        
    }

    void CodigoRandom()
    {
        Digitos = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

        int Cod = Random.Range(0, Digitos.Count);
        C1 = Digitos[Cod]; Digitos.RemoveAt(Cod);

        Cod = Random.Range(0, Digitos.Count);
        C2 = Digitos[Cod]; Digitos.RemoveAt(Cod);

        Cod = Random.Range(0, Digitos.Count);
        C3 = Digitos[Cod]; Digitos.RemoveAt(Cod);

        Debug.Log("Código generado:" + C1 + C2 + C3);
        Debug.Log("Lista completa: " + string.Join(", ", Digitos));
    }
}
