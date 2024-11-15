using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldEventManager : MonoBehaviour
{
    public static BattlefieldEventManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }

    public event Action<Vector3Int> SetHomebase;
    public event Action RedrawScreenTriggered;
    public event Action Deselect;
    public event Action<TowerBlueprint> TowerBlueprintSelected;
    public event Action DeleteTowerSelected;
    public event Action<TowerBlueprint, Vector3Int> TowerPlaced;
    public event Action<TowerBlueprint, Vector3Int> WallPlaced;
    public event Action<GameObject> EnemyDestroyed;
    public event Action<GameObject> TowerDestroyed;
    public event Action<GameObject> WallDestroyed;
    public event Action HomebaseDestroyed;
    public event Action StartNewWave;
    public event Action WaveFinishedSpawning;
    public event Action WaveCleared;
    public event Action<EnemyBlueprint> EnemySpawned;
    public event Action<int, TowerBlueprint> UpgradeSelected;


    // more events added here

    public void OnRedrawScreen()
    {
        RedrawScreenTriggered?.Invoke();
    }

    public void OnSetHomebase(Vector3Int location)
    {
        SetHomebase?.Invoke(location);
    }

    public void OnDeselect()
    {
        Deselect?.Invoke();
    }

    public void OnTowerBlueprintSelected(TowerBlueprint towerBlueprint)
    {
        TowerBlueprintSelected?.Invoke(towerBlueprint);
    }

    public void OnDeleteTowerSelected()
    {
        DeleteTowerSelected?.Invoke();
    }

    public void OnTowerPlaced(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        TowerPlaced?.Invoke(towerBlueprint, location);
    }
    public void OnWallPlaced(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        WallPlaced?.Invoke(towerBlueprint, location);
    }

    public void OnEnemyDestroyed(GameObject enemy)
    {
        EnemyDestroyed?.Invoke(enemy);
    }
    public void OnTowerDestroyed(GameObject tower)
    {
        TowerDestroyed?.Invoke(tower);
    }
    public void OnWallDestroyed(GameObject wall)
    {
        WallDestroyed?.Invoke(wall);
    }
    public void OnHomebaseDestroyed()
    {
        HomebaseDestroyed?.Invoke();
    }
    public void OnStartNewWave()
    {
        StartNewWave?.Invoke();
    }
    public void OnWaveFinishedSpawning()
    {
        WaveFinishedSpawning?.Invoke();
    }
    public void OnWaveCleared()
    {
        WaveCleared?.Invoke();
    }
    public void OnEnemySpawned(EnemyBlueprint enemyBlueprint)
    {
        EnemySpawned?.Invoke(enemyBlueprint);
    }

    public void OnUpgradeSelected(int from, TowerBlueprint toTowerBlueprint)
    {
        UpgradeSelected?.Invoke(from, toTowerBlueprint);
    }
}
