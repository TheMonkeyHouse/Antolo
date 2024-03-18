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

    // temp
    private float timeSinceEnemySpawn;

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
        //load towers
        Towers.LoadTowers();
        //load enemies
        Enemies.LoadEnemies();

        //temp
        timeSinceEnemySpawn = 5.0f;
    }

    private void Start()
    {
        NewArena();
    }

    private void Update()
    {
        //spawn enemny every 5 seconds
        timeSinceEnemySpawn += Time.deltaTime;
        if (timeSinceEnemySpawn >= 5.0)
        {
            GetComponentInChildren<EnemyController>().SpawnEnemy();
            timeSinceEnemySpawn = 0.0f;
        }
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
        DrawGrid();
    }

    public void DrawGrid()
    {
        battlefieldGrid.DrawGrid(state);
    }

    public void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        this.state[position.x, position.y].type = CellType.Tower;
        DrawGrid();
    }

    private void TowerDestroyed(GameObject tower)
    {
        Vector3Int location = tower.GetComponent<Tower>().location;
        this.state[location.x, location.y].type = CellType.Ground;
        DrawGrid();
    }

    public bool IsInGrid(Vector3Int position)
    {
        return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
    }
}
