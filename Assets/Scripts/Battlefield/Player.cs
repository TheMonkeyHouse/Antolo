using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Player : MonoBehaviour
{
    public int energyPerWave {get; private set;}
    public int energy {get; private set;}
    public int level {get; private set;}
    public TowerBlueprint[] towerBlueprints {get; private set;}
    [SerializeField] private TMP_Text energyDisplay;
    [SerializeField] private TMP_Text levelDisplay;
    
    // Associated Buttons
    [SerializeField] private BlueprintSelector attackTower1Button;
    [SerializeField] private BlueprintSelector attackTower2Button;
    [SerializeField] private BlueprintSelector tankyTower1Button;
    [SerializeField] private BlueprintSelector tankyTower2Button;
    [SerializeField] private BlueprintSelector supportTower1Button;
    [SerializeField] private BlueprintSelector supportTower2Button;

    void Awake()
    {
        towerBlueprints = new TowerBlueprint[6];
        BattlefieldEventManager.instance.TowerPlaced += TowerPlaced;
        BattlefieldEventManager.instance.WallPlaced += WallPlaced;
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        BattlefieldEventManager.instance.WaveCleared += WaveCleared;
        BattlefieldEventManager.instance.UpgradeSelected += UpgradeSelected;
    }
    public void RecieveEnergy(int extraEnergy)
    {
        this.energy += extraEnergy;
        UpdateEnergyDisplay();
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
        energy += energyPerWave;
        UpdateEnergyDisplay();
    }
    void StartNewWave()
    {
        level = level+1;
        UpdateLevelDisplay();
    }

    private void UpgradeSelected(int from, TowerBlueprint toTowerBlueprint)
    {
        towerBlueprints[from] = toTowerBlueprint;
        UpdateButtons();
    }

    public Vector3 IntToScreenPoint(int choiceFor)
    {
        if (choiceFor == 0) {
            return attackTower1Button.gameObject.transform.position;
        } else if (choiceFor == 1) {
            return attackTower2Button.gameObject.transform.position;
        } else if (choiceFor == 2) {
            return tankyTower1Button.gameObject.transform.position;
        } else if (choiceFor == 3) {
            return tankyTower2Button.gameObject.transform.position;
        } else if (choiceFor == 4) {
            return supportTower1Button.gameObject.transform.position;
        } else if (choiceFor == 5) {
            return supportTower2Button.gameObject.transform.position;
        }
        return new Vector3(0f,0f,0f);
    }
    public void Initialize(int startingEnergy, int energyPerWave, int level)
    {
        this.energy = startingEnergy;
        this.energyPerWave = energyPerWave;
        this.level = level;
        this.towerBlueprints[0] = Towers.towerBlueprints["MachineGunTower"];
        this.towerBlueprints[1] = Towers.towerBlueprints["SniperTower"];
        this.towerBlueprints[2] = Towers.towerBlueprints["BasicWall"];
        this.towerBlueprints[3] = Towers.towerBlueprints["SpikeTrap"];
        this.towerBlueprints[4] = Towers.towerBlueprints["HealingTower"];
        this.towerBlueprints[5] = Towers.towerBlueprints["EnergyGeneratorTower"];
        UpdateButtons();
        UpdateEnergyDisplay();
        UpdateLevelDisplay();
    }
}
