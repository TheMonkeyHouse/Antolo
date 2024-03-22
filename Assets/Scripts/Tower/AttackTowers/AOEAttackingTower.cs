using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttackingTower : AttackingTower {
    
    public override List<GameObject> GetTargets()
    {
        return this.towerRange.GetTargets();
    }
}