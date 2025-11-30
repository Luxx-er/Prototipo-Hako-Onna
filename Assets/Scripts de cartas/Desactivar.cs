using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivar : MonoBehaviour
{
    public GameObject Carta;
    // Start is called before the first frame update
    public void Desaparecer()
    {
        StartCoroutine(desactivar());
    }

    // Update is called once per frame
   public IEnumerator desactivar()
    {
        yield return new WaitForSeconds(2f);
        Carta.SetActive(false);
    }
}
