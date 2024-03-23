using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingTower : AOESupportTower {
    public float healing {get; private set;}
    public override void Support(GameObject tower)
    {
        tower.GetComponent<BaseTower>().Heal(healing);
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.healing = towerBlueprint.baseStats["healing"];
    }
}