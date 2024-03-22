using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanAttackingTower : AttackingTower 
{
    public float rotationSpeed {get; private set;}
    
    public override List<GameObject> GetTargets()
    {
        List<GameObject> targets = this.towerRange.GetTargets();
        if (targets.Count == 0)
        {
            return targets;
        }
        return new List<GameObject>{targets[0]};
    }
 
    public override void TryAttack(List<GameObject> targets)
    {
        // idle if no targets
        if (targets.Count == 0)
        {
            return;
        }
        
        GameObject target = targets[0];
        
        // rotate towards target
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x -transform.position.x ) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        sr.transform.rotation = Quaternion.RotateTowards(sr.gameObject.transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);

        // if not looking at target skip
        if (!(targetRotation == sr.gameObject.transform.rotation))
        {
            return;
        }

        base.TryAttack(targets);
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.rotationSpeed = towerBlueprint.baseStats["rotationSpeed"];
    }

}