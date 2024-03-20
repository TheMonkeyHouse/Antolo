using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public int startingEnergy {get; private set;}
    public int energy {get; private set;}
    public int level {get; private set;}
    private TowerBlueprint[] towerBlueprints;
    [SerializeField] private TMP_Text energyDisplay;
    [SerializeField] private TMP_Text levelDisplay;
    
    // Associated Buttons
    [SerializeField] private TowerSelector attackTower1Button;
    [SerializeField] private TowerSelector attackTower2Button;
    [SerializeField] private TowerSelector tankyTower1Button;
    [SerializeField] private TowerSelector tankyTower2Button;
    [SerializeField] private TowerSelector supportTower1Button;
    [SerializeField] private TowerSelector supportTower2Button;

    public TowerBlueprint selectedTower {get; private set;}
    public bool deleteTowerSelected {get; private set;}

    void Awake()
    {
        towerBlueprints = new TowerBlueprint[6];
        BattlefieldEventManager.instance.TowerBlueprintSelected += SelectTower;
        BattlefieldEventManager.instance.DeleteTowerSelected += DeleteTowerSelected;
        BattlefieldEventManager.instance.Deselect += Deselect;
        BattlefieldEventManager.instance.TowerPlaced += TowerPlaced;
        BattlefieldEventManager.instance.WallPlaced += WallPlaced;
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        BattlefieldEventManager.instance.WaveCleared += WaveCleared;
        selectedTower = null;
        deleteTowerSelected = false;
    }

    void SelectTower(TowerBlueprint towerBlueprint)
    {
        selectedTower = towerBlueprint;
    }

    void DeleteTowerSelected()
    {
        deleteTowerSelected = true;
    }

    void Deselect()
    {
        selectedTower = null;
        deleteTowerSelected = false;
    }

    private void UpdateEnergyDisplay()
    {
        energyDisplay.text = energy.ToString();
    }

    private void UpdateLevelDisplay()
    {
        levelDisplay.text = level.ToString();
    }

    private void UpdateButtons()
    {
        attackTower1Button.UpdateButton(towerBlueprints[0]);
        attackTower2Button.UpdateButton(towerBlueprints[1]);
        tankyTower1Button.UpdateButton(towerBlueprints[2]);
        tankyTower2Button.UpdateButton(towerBlueprints[3]);
        supportTower1Button.UpdateButton(towerBlueprints[4]);
        supportTower2Button.UpdateButton(towerBlueprints[5]);
    }

    void TowerPlaced(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        energy -= (int) towerBlueprint.baseStats["energyCost"];
        UpdateEnergyDisplay();
    }
    void WallPlaced(TowerBlueprint towerBlueprint, Vector3Int position)
    {
        energy -= (int) towerBlueprint.baseStats["energyCost"];
        UpdateEnergyDisplay();
    }
    void WaveCleared()
    {
        energy = startingEnergy;
        UpdateEnergyDisplay();
    }
    void StartNewWave()
    {
        level = level+1;
        UpdateLevelDisplay();
    }
    public void Initialize(int startingEnergy, int level)
    {
        this.startingEnergy = startingEnergy;
        this.energy = startingEnergy;
        this.level = level;
        this.towerBlueprints[0] = Towers.towerBlueprints["MachineGunTower"];
        this.towerBlueprints[1] = Towers.towerBlueprints["SniperTower"];
        this.towerBlueprints[2] = Towers.towerBlueprints["BasicWall"];
        this.towerBlueprints[3] = Towers.towerBlueprints["SpikeTrap"];
        this.towerBlueprints[4] = Towers.towerBlueprints["HealingTower"];
        this.towerBlueprints[5] = Towers.towerBlueprints["RoundIncomeTower"];
        UpdateButtons();
        UpdateEnergyDisplay();
        UpdateLevelDisplay();
    }
}
