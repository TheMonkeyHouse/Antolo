using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseTower
{
    public override void Die()
    {
        BattlefieldEventManager.instance.OnWallDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        sr.sortingOrder = 1;
    }
}