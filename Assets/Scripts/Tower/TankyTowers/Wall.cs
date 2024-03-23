using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : BaseTower
{
    public override void Die()
    {
        BattlefieldEventManager.instance.OnWallDestroyed(this.gameObject);
        base.Die();
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.gameObject.tag = "Wall";
        sr.sortingOrder = 1;
    }
}