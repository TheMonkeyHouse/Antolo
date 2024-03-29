using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattlefieldPhase
{
    BuildPhase,
    WaveSpawningPhase,
    WaveClearingPhase,
    UpgradePhase,
    SelectItemPhase,
    ModifyBlueprintsPhase
}

public class BattlefieldController : MonoBehaviour
{
    public static BattlefieldController instance {get; private set;}
    private BattlefieldGrid battlefieldGrid;
    private GridController gridController;
    [SerializeField] private GameObject upgradeMenu;
    [SerializeField] private StartNewWaveButton startNewWaveButton;
    public int width = 5;
    public int height = 5;
    public Player player {get; private set;}
    public Cell[,] state {get; private set;}
    public bool[,] wallState {get; private set;}
    public bool[,] towerState {get; private set;}
    private int currentWave;
    public BattlefieldPhase currentPhase {get; private set;}
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
        battlefieldGrid = GetComponentInChildren<BattlefieldGrid>();
        gridController = GetComponentInChildren<GridController>();
        player = GetComponentInChildren<Player>();
        // init events
        BattlefieldEventManager.instance.WallPlaced += PlaceWall;
        BattlefieldEventManager.instance.TowerPlaced += PlaceTower;
        BattlefieldEventManager.instance.TowerDestroyed += TowerDestroyed;
        BattlefieldEventManager.instance.WallDestroyed += WallDestroyed;
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        BattlefieldEventManager.instance.WaveCleared += WaveCleared;
        BattlefieldEventManager.instance.UpgradeSelected += UpgradeSelected;
        //load towers
        Towers.LoadTowers();
        //load enemies
        Enemies.LoadEnemies();
        // load tower choices
        upgradeMenu.GetComponent<UpgradeMenu>().GenerateTowerChoices();
        // set activity
        upgradeMenu.SetActive(false);
        gridController.SetActive(true);
        startNewWaveButton.SetInteractable(true);
        
    }

    private void Start()
    {
        NewArena();
    }

    private void NewArena()
    {
        Camera.main.transform.position = new Vector3(width / 2f, height/ 2f, -10.0f);
        Camera.main.orthographicSize = width*0.6f;
        state = new Cell[width,height];
        wallState = new bool[width,height];
        towerState = new bool[width,height];
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
                    this.towerState[x,y] = true;
                }
                this.state[x,y] = cell;
            }
        }
        BattlefieldEventManager.instance.OnSetHomebase(new Vector3Int(width/2, height/2, 0));
        player.Initialize(1000, 300, 0);
        currentWave = 0;
        currentPhase = BattlefieldPhase.BuildPhase;
        DrawGrid();
    }

    public void DrawGrid()
    {
        battlefieldGrid.DrawGrid(state);
    }

    public void PlaceWall(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        this.wallState[position.x, position.y] = true;
        DrawGrid();
    }

    public void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        if (this.state[position.x, position.y].type == CellType.Wall)
        {
            return;
        }
        this.towerState[position.x, position.y] = true;
        DrawGrid();
    }

    private void TowerDestroyed(GameObject tower)
    {
        Vector3Int location = tower.GetComponent<Tower>().location;
        this.towerState[location.x, location.y] = false;
        DrawGrid();
    }
    private void WallDestroyed(GameObject wall)
    {
        Vector3Int location = wall.GetComponent<Wall>().location;
        this.wallState[location.x, location.y] = false;
        DrawGrid();
    }

    private void StartNewWave()
    {
        currentWave++;
        Wave newWave = MakeNewWave();
        currentPhase = BattlefieldPhase.WaveSpawningPhase;
        print("starting wave " + currentWave);
        StartCoroutine(newWave.WaveSpawner());
        startNewWaveButton.SetInteractable(false);
    }
    private void WaveFinishedSpawning()
    {
        currentPhase = BattlefieldPhase.WaveClearingPhase;
    }

    private void WaveCleared()
    {
        currentPhase = BattlefieldPhase.UpgradePhase;
        upgradeMenu.GetComponent<UpgradeMenu>().GenerateUpgrades();
        gridController.SetActive(false);
        upgradeMenu.SetActive(true);
    }

    private void UpgradeSelected(int from, TowerBlueprint toTowerBlueprint)
    {
        currentPhase = BattlefieldPhase.ModifyBlueprintsPhase;
        upgradeMenu.SetActive(false);
        gridController.SetActive(true);
        startNewWaveButton.SetInteractable(true);
    }

    private Wave MakeNewWave()
    {
        // float rating = Mathf.Pow(currentWave, 1.5f)*2.0f + 1.0f; // wave scaling function = 15x^(3) + 15 O(x^1.5)
        float rating = 1.0f;
        EnemyBlueprint[] possibleChoices = new EnemyBlueprint[] {Enemies.enemyBlueprints["BasicEnemy"]};
        float[] weights = new float[] {1.0f};
        return new Wave(rating, possibleChoices, weights);
    }

    public bool IsInGrid(Vector3Int position)
    {
        return position.x >= 0 && position.x < width && position.y >= 0 && position.y < height;
    }
}
