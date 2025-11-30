using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Investiga = false;
    public GameObject Bloqueo;

    void Start()
    {
    }
    void Update()
    {
    }
    public void Investigar()
    {
        Investiga = true;
        if(Bloqueo != null)
        {
            Bloqueo.layer = 3;
        }
    }
}
