
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volteardos : MonoBehaviour
{
    //Vector3 Mouseposition;
    //RaycastHit2D raycastHit2;
    //Transform clickObject;
    private bool voltear = false;

    //void Update()
    //{
    //    Mouseposition = Input.mousePosition;

    //   Ray mouseRay = Camera.main.ScreenPointToRay(Mouseposition);

    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        raycastHit2 = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
    //        clickObject = raycastHit2 ? raycastHit2.collider.transform : null;

    //        if (clickObject)
    //        {
    //            Flip();
    //        }
    //    }

        
    //}

    public void Flip()
    {
        if (voltear == true)
            return;
        StartCoroutine(voltea());
    }

    public IEnumerator voltea()
    {
        voltear = !voltear;
        //transform.DORotate(new(0, voltear ? 180f : 0, 0), 0.25f);

        GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(2f);
    }
}
