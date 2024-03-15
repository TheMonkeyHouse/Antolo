using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattlefieldController : MonoBehaviour
{
    public static BattlefieldController instance {get; private set;}
    private BattlefieldGrid battlefieldGrid;
    public int width = 5;
    public int height = 5;
    private Cell[,] state;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
        battlefieldGrid = GetComponentInChildren<BattlefieldGrid>();
    }

    private void Start()
    {
        NewArena();
    }

    private void NewArena()
    {
        Camera.main.transform.position = new Vector3(width / 2f, height/ 2f, -10.0f);
        Camera.main.orthographicSize = width*0.8f;
        state = new Cell[width,height];
        for (int x=0; x< width; x++)
        {
            for (int y=0; y<height; y++)
            {
                Cell cell = new Cell();
                cell.position = new Vector3Int(x, y, 0);
                cell.type = CellType.Ground;
                if (x==width/2 && y==width/2)
                {
                    cell.type = CellType.Homebase;
                }
                this.state[x,y] = cell;
            }
        }
        DrawGrid();
    }

    public void DrawGrid()
    {
        battlefieldGrid.DrawGrid(state);
    }

    
    public bool IsInGrid(Vector3Int position)
    {
        return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
    }
}
