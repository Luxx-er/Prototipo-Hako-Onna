using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Voltear : MonoBehaviour
{

   private bool voltear = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Flip();
    }

    private void Flip()
    {
        voltear = !voltear;
        transform.DORotate(new(0, voltear ? 0f : 180, 0), 0.25f);
    }

}
