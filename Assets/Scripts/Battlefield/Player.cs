using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int health;

    // tower choices

    // energy

    // money

    public TowerBlueprint selectedTower;

    void Awake()
    {
        BattlefieldEventManager.instance.TowerBlueprintSelected += SelectTower;
        BattlefieldEventManager.instance.TowerDeselected += DeselectTower;
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
}
