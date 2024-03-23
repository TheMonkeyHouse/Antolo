using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBlueprint
{
    public string towerID;
    public string towerName;
    public string description;
    public string towerType;
    public Dictionary<string, float> baseStats;

    public bool isPlaceable(Vector3Int pos)
    {
        {
        if (!BattlefieldController.instance.IsInGrid(pos))
        {
            return false;
        }
        if (BattlefieldController.instance.state[pos.x,pos.y].type == CellType.Tower)
        {
            return false;
        }
        if (BattlefieldController.instance.state[pos.x,pos.y].type == CellType.Wall && towerType == "Wall")
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