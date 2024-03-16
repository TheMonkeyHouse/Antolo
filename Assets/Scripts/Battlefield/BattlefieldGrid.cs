using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class BattlefieldGrid: MonoBehaviour
{
    public Tilemap tileGrid {get; private set;}
    public Tile tileUnknown;
    public Tile tileGround;
    public Tile tileHomebase;
    public Tile tileWall;
    public Tile tileTowerBase;

    void Awake()
    {
        tileGrid = GetComponent<Tilemap>();
    }

    public void DrawGrid(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x,y];
                tileGrid.SetTile(cell.position, GetTile(cell));
            }
        }
    }

    private Tile GetTile(Cell cell)
    {
        switch (cell.type)
        {
            case CellType.Ground:
                return tileGround;
            case CellType.Wall:
                return tileWall;
            case CellType.Homebase:
                return tileHomebase;
            case CellType.Tower:
                return tileTowerBase;
            default:
                return tileUnknown;
        }
    }
}

