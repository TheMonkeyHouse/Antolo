using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    [SerializeField] private ChooseUpgrade upgradeChoice1;
    [SerializeField] private ChooseUpgrade upgradeChoice2;

    // dict that contains upgrades tower type -> tower tier -> t
    private Dictionary<string, Dictionary<int, List<TowerBlueprint>>> towerChoices;

    public void GenerateTowerChoices()
    {
        towerChoices = new Dictionary<string, Dictionary<int, List<TowerBlueprint>>>();
        towerChoices["Damage"] = new Dictionary<int, List<TowerBlueprint>>();
        towerChoices["Tanky"] = new Dictionary<int, List<TowerBlueprint>>();
        towerChoices["Support"] = new Dictionary<int, List<TowerBlueprint>>();
        
        for (int i = 1; i <= 3; i++)
        {
            towerChoices["Damage"][i] = new List<TowerBlueprint>();
            towerChoices["Tanky"][i] = new List<TowerBlueprint>();
            towerChoices["Support"][i] = new List<TowerBlueprint>();
        }
        foreach (TowerBlueprint tower in Towers.towerBlueprints.Values)
        {
            if (tower.towerClass == "Homebase")
            {
                continue;
            }
            towerChoices[tower.towerClass][tower.towerTier].Add(tower);
        }

        for (int i = 1; i <= 3; i++)
        {
            towerChoices["Damage"][i].Shuffle();
            towerChoices["Tanky"][i].Shuffle();
            towerChoices["Support"][i].Shuffle();
        }
    }

    public void GenerateUpgrades()
    {
        // find lowest tier in player towers
        TowerBlueprint[] currentBlueprints = BattlefieldController.instance.player.towerBlueprints;
        int lowestTier = 5;

        foreach (TowerBlueprint towerBlueprint in currentBlueprints)
        {
            if (towerBlueprint.towerTier < lowestTier)
            {
                lowestTier = towerBlueprint.towerTier;
            }
        }

        // list of possible upgrade indexes
        List<int> possibleUpgradeIndices = new List<int>();
        for (int i = 0; i < currentBlueprints.Length; i++)
        {
            if (currentBlueprints[i].towerTier == lowestTier)
            {
                possibleUpgradeIndices.Add(i);
            }
        }

        // add two random choices
        int upgradeFrom1 = -1;
        int upgradeFrom2 = -1;
        // if only 1 choice
        if (possibleUpgradeIndices.Count <= 1)
        {
            upgradeFrom1 = possibleUpgradeIndices[0];
            upgradeFrom2 = possibleUpgradeIndices[0];
        } 
        else 
        {
            possibleUpgradeIndices.Shuffle();
            upgradeFrom1 = possibleUpgradeIndices[0];
            upgradeFrom2 = possibleUpgradeIndices[1];
        }

        // order
        if (upgradeFrom2 < upgradeFrom1)
        {
            int temp = upgradeFrom1;
            upgradeFrom1 = upgradeFrom2;
            upgradeFrom2 = temp;
        }
        
        // get choice from choices list
        TowerBlueprint towerFrom1 = BattlefieldController.instance.player.towerBlueprints[upgradeFrom1];
        TowerBlueprint towerFrom2 = BattlefieldController.instance.player.towerBlueprints[upgradeFrom2];

        TowerBlueprint towerChoice1 = null;
        TowerBlueprint towerChoice2 = null;
        
        if (towerChoices[towerFrom1.towerClass][towerFrom1.towerTier + 1].Count > 0)
        {
            towerChoice1 = towerChoices[towerFrom1.towerClass][towerFrom1.towerTier + 1][0];
            towerChoices[towerFrom1.towerClass][towerFrom1.towerTier + 1].RemoveAt(0);
            
        }
        else // default
        {
            towerChoice1 = Towers.towerBlueprints["MachineGunTower"];
        }
        if (towerChoices[towerFrom2.towerClass][towerFrom2.towerTier + 1].Count > 0)
        {
            towerChoice2 = towerChoices[towerFrom2.towerClass][towerFrom2.towerTier + 1][0];
            towerChoices[towerFrom2.towerClass][towerFrom2.towerTier + 1].RemoveAt(0);
        }
        else
        {
            towerChoice2 = Towers.towerBlueprints["MachineGunTower"];
        }
       
        // set upgrades
        upgradeChoice1.SetUpgrade(upgradeFrom1, towerChoice1);
        upgradeChoice2.SetUpgrade(upgradeFrom2, towerChoice2);
    }
}