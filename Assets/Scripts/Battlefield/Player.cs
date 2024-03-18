using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player : MonoBehaviour
{
    public int money {get; private set;}
    public int level {get; private set;}
    [SerializeField] private TMP_Text moneyDisplay;
    [SerializeField] private TMP_Text levelDisplay;
    // tower choices

    public TowerBlueprint selectedTower;

    void Awake()
    {
        BattlefieldEventManager.instance.TowerBlueprintSelected += SelectTower;
        BattlefieldEventManager.instance.TowerDeselected += DeselectTower;
        BattlefieldEventManager.instance.TowerPlaced += TowerPlaced;
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        BattlefieldEventManager.instance.EnemyDestroyed += EnemyDestroyed;
        BattlefieldEventManager.instance.WaveCleared += WaveCleared;
        selectedTower = null;
    }

    void SelectTower(TowerBlueprint towerBlueprint)
    {
        selectedTower = towerBlueprint;
    }

    void DeselectTower()
    {
        selectedTower = null;
    }

    void UpdateMoneyDisplay()
    {
        moneyDisplay.text = money.ToString("N0");
    }

    void UpdateLevelDisplay()
    {
        levelDisplay.text = level.ToString();
    }

    void TowerPlaced(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        money -= (int) towerBlueprint.baseStats["cost"];
        UpdateMoneyDisplay();
    }
    void EnemyDestroyed(GameObject enemy)
    {
        money = money + 50;
        UpdateMoneyDisplay();
    }
    void WaveCleared()
    {
        money = money + 250;
        UpdateMoneyDisplay();
    }
    void StartNewWave()
    {
        level = level+1;
        UpdateLevelDisplay();
    }
    public void Initialize(int startingMoney, int level)
    {
        this.money = startingMoney;
        this.level = level;
        UpdateMoneyDisplay();
        UpdateLevelDisplay();
    }
}
