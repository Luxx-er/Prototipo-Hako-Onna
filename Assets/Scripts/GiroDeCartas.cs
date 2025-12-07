using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroDeCartas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Destruye()
    {
        StartCoroutine(DesaparecerCarta());
    }
    IEnumerator DesaparecerCarta()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
    }
}
