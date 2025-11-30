using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivarcanvas : MonoBehaviour
{
    public GameObject Cartastodo;
    // Start is called before the first frame update
    public void desactivarcanvas()
    {
        StartCoroutine(desaparecercanvas());
    }

    // Update is called once per frame
    public IEnumerator desaparecercanvas()  
    {
        yield return new WaitForSeconds(3f);
       Cartastodo.SetActive(false);
    }
}
