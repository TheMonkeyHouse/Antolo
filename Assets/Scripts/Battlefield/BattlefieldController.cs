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
    public Player player;
    public Cell[,] state {get; private set;}
    private int currentWave;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
        battlefieldGrid = GetComponentInChildren<BattlefieldGrid>();
        player = GetComponentInChildren<Player>();
        // init events
        BattlefieldEventManager.instance.TowerPlaced += PlaceTower;
        BattlefieldEventManager.instance.TowerDestroyed += TowerDestroyed;
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        //load towers
        Towers.LoadTowers();
        //load enemies
        Enemies.LoadEnemies();
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
                    cell.type = CellType.Tower;
                }
                this.state[x,y] = cell;
            }
        }
        BattlefieldEventManager.instance.OnSetHomebase(new Vector3Int(width/2, height/2, 0));
        player.Initialize(1000, 0);
        currentWave = 0;
        DrawGrid();
    }

    public void DrawGrid()
    {
        battlefieldGrid.DrawGrid(state);
    }

    public void PlaceWall(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        this.state[position.x, position.y].type = CellType.Wall;
        DrawGrid();
    }

    public void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        if (this.state[position.x, position.y].type == CellType.Wall)
        {
            return;
        }
        this.state[position.x, position.y].type = CellType.Tower;
        DrawGrid();
    }

    private void TowerDestroyed(GameObject tower)
    {
        Vector3Int location = tower.GetComponent<Tower>().location;
        this.state[location.x, location.y].type = CellType.Ground;
        DrawGrid();
    }

    public void StartNewWave()
    {
        currentWave++;
        Wave newWave = MakeNewWave();
        print("starting wave " + currentWave);
        StartCoroutine(newWave.WaveSpawner());
    }

    public Wave MakeNewWave()
    {
        float rating = Mathf.Pow(currentWave, 1.5f)*2.0f + 1.0f; // wave scaling function = 15x^(3) + 15 O(x^1.5)
        EnemyBlueprint[] possibleChoices = new EnemyBlueprint[] {Enemies.enemyBlueprints["BasicEnemy"]};
        float[] weights = new float[] {1.0f};
        return new Wave(rating, possibleChoices, weights);
    }

    public bool IsInGrid(Vector3Int position)
    {
        return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
    }
}
