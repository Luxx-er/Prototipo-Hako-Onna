using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects", order = 1)]
public class InventarioPlayer1 : ScriptableObject
{
    public List<CompartirInventario> Items;
    public int Name;
    public int MaxCantidad = 10;

}
