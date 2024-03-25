using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanAttackingTower : AttackingTower
{
    public float rotationSpeed {get; private set;}
    
    public override List<GameObject> GetTargets()
    {
        return this.towerRange.GetTargets();
    }

    public override void TryAttack(List<GameObject> targets)
    {
        foreach(GameObject target in targets)
        {
            if (target == null)
            {
                continue;
            }
            if (!CanAttackTarget(target))
            {
                continue;
            }
            // rotate towards target
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x -transform.position.x ) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            sr.transform.rotation = Quaternion.RotateTowards(sr.gameObject.transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);
            // if not looking at target skip
            if (!(targetRotation == sr.gameObject.transform.rotation))
            {
                return;
            }

            Attack(target);
            return;
        }
    }

    public override bool CanAttackTarget(GameObject target)
    {
        if (isOnWall)
        {
            return true;
        }
        // raycast to detect if target is behind wall
        Vector3 targetDirection = target.transform.position - gameObject.transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(gameObject.transform.position + new Vector3(0.5f, 0.5f, 0f), targetDirection, this.attackRange);
        foreach (RaycastHit2D hit in hits)
        {
            BaseTower towerScript = hit.transform.gameObject.GetComponent<BaseTower>();
            if (towerScript == null)
            {
                continue;
            }
            if (towerScript.towerType == "Wall")
            {
                return false;
            }
        }
        return true;
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        Instantiate(Resources.Load<GameObject>("Prefabs/TowerBase"), this.gameObject.transform);
        this.rotationSpeed = towerBlueprint.baseStats["rotationSpeed"];
    }

}