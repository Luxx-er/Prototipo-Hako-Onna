using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaltarTurno : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI SaltasteTurno;
    [SerializeField] private GameObject PanelTurno;
    [SerializeField] private TextMeshProUGUI Turno;
    public void SiguienteTurno()
    {
        Debug.Log("SaltasteTurno SIGUIENTE TURNO");
        PanelTurno.SetActive(true);
        Turno.text = "Siguiente Turno";
    }
}
