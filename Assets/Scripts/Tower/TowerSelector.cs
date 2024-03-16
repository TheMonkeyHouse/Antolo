using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    public TowerBlueprint towerBlueprint;
    private bool isSelected;

    void Awake()
    {
        isSelected = false;
        BattlefieldEventManager.instance.TowerDeselected += Deselect;
        towerBlueprint = Towers.towerBlueprints["MachineGunTower"];
    }

    public void Deselect()
    {
        isSelected = false;
    }

    public void OnClick()
    {
        if (!isSelected)
        {
            BattlefieldEventManager.instance.OnTowerBlueprintSelected(towerBlueprint);
        }
        else
        {
            BattlefieldEventManager.instance.OnTowerDeselected();
        }
        isSelected = !isSelected;
    }
}
