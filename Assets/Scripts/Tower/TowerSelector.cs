using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    private TowerBlueprint towerBlueprint;
    [SerializeField]
    private string towerSeleciton;
    private bool isSelected;

    void Awake()
    {
        isSelected = false;
        BattlefieldEventManager.instance.TowerDeselected += Deselect;
        towerBlueprint = Towers.towerBlueprints[towerSeleciton];
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
