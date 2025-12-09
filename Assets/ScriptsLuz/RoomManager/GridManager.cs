using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    int[,] Grid;
    private int Columns, Rows;
    [SerializeField] private CinemachineVirtualCamera[] Camera;

    // Start is called before the first frame update
    void Start()
    {
        Columns = 10;
        Rows = 10;
        Grid = new int[Columns, Rows];
        for (int i = 0; i < Columns; i++)
        {
            for (int j = 0; j < Rows; j++)
            {
                Grid[i, j] = Random.Range(0, 11);
                SpawnTile(i, j, Grid[i,j]);
            }
        }
    }

  private void SpawnTile(int x,  int y, int value)
    {
        GameObject tile = new GameObject("x:" + x + "y:");
        tile.transform.position = new Vector3(x, y, value);
    }
}
