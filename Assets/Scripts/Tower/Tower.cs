using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BaseTower
{
    private bool isOnWall;
    public override void Die()
    {
        BattlefieldEventManager.instance.WallDestroyed -= WallDestroyed;
        BattlefieldEventManager.instance.OnTowerDestroyed(this.gameObject);
        base.Die();
    }

    private void WallDestroyed(GameObject wall)
    {
        Vector3Int wallLocation = wall.GetComponent<BaseTower>().location;
        if (wallLocation.x == location.x && wallLocation.y == location.y)
        {
            Die();
        }
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        isOnWall = false;
        if (BattlefieldController.instance.wallState[location.x, location.y])
        {
            isOnWall = true;
        }
        if (isOnWall)
        {
            SetHealthBarActive(false);
        }
        BattlefieldEventManager.instance.WallDestroyed += WallDestroyed;
    }
    
}