using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueprint
{
    public string towerID;
    public string towerName;
    public string description;
    public string towerClass;
    public string towerType;
    public int towerTier;
    public Dictionary<string, float> baseStats;

    public bool isPlaceable(Vector3Int pos)
    {
        {
        if (!BattlefieldController.instance.IsInGrid(pos))
        {
            return false;
        }
        if (BattlefieldController.instance.towerState[pos.x,pos.y])
        {
            return false;
        }
        if (BattlefieldController.instance.wallState[pos.x,pos.y] && towerType == "Wall")
        {
            return false;
        }
        if (BattlefieldController.instance.player.energy < baseStats["energyCost"])
        {
            return false;
        }
        return true;
    }
    }
}