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

    public event Action RedrawScreenTriggered;
    public event Action TowerDeselected;
    public event Action<TowerBlueprint> TowerBlueprintSelected;
    public event Action<TowerBlueprint, Vector3Int> TowerPlaced;

    // more events added here

    public void OnRedrawScreen()
    {
        RedrawScreenTriggered?.Invoke();
    }

    public void OnTowerDeselected()
    {
        TowerDeselected?.Invoke();
    }

    public void OnTowerBlueprintSelected(TowerBlueprint towerBlueprint)
    {
        TowerBlueprintSelected?.Invoke(towerBlueprint);
    }

    public void OnTowerPlaced(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        TowerPlaced?.Invoke(towerBlueprint, location);
    }
}
