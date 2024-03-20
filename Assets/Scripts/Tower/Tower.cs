using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : BaseTower
{
    public override void Die()
    {
        BattlefieldEventManager.instance.WallDestroyed -= WallDestroyed;
        BattlefieldEventManager.instance.OnTowerDestroyed(this.gameObject);
        Destroy(this.gameObject);
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
        BattlefieldEventManager.instance.WallDestroyed += WallDestroyed;
    }
    
}