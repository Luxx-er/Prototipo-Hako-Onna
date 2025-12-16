using UnityEngine;

public class ItemData : MonoBehaviour
{
    public enum TipoEspecial
    {
        Ninguno,
        CajaFuerte,
        Sombrero,
        PuertaSecreta,
        Llaves
    }
    public Sprite icono; //Codigo para todos los items
    public TipoEspecial tipoEspecial = TipoEspecial.Ninguno;
    public bool Tachesote;     
}