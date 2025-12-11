using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiroDeCartas : MonoBehaviour
{
    public bool BotonActivo = false;
    public bool voltear = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Flip()
    {
        if (voltear == true)
            return;
        StartCoroutine(voltea());
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
        if(BotonActivo == true)
        {
            
        }
    }

    public IEnumerator voltea()
    {
        voltear = !voltear;
        transform.DORotate(new(0, voltear ? 180f : 0, 0), 0.25f);

        GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(4f);
    }

}
