using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class AttackingTower : Tower
{
    public float damage {get; private set;}
    public float attackSpeed {get; private set;}
    public float attackRange {get; private set;}
    public TowerRange towerRange {get; private set;}
    public float timeSinceLastAttack {get; private set;}

    protected virtual void Update()
    {
        this.timeSinceLastAttack += Time.deltaTime;
        TryAttack(GetTargets());
    }

    public virtual void TryAttack(List<GameObject> targets)
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
            Attack(target);
        }
    }

    public virtual bool Attack(GameObject target)
    {
        // if within attack speed, skip
        if (this.timeSinceLastAttack <  1f / this.attackSpeed)
        {
            return false;
        }
        target.GetComponent<Enemy>().TakeDamage(this.damage);
        this.timeSinceLastAttack = 0;
        return true;
    }
    
    public abstract List<GameObject> GetTargets();
    public virtual bool CanAttackTarget(GameObject target)
    {
        return true;
    }

    public override void Selected()
    {
        base.Selected();
        towerRange.SetVisualizerActive(true);
    }

    public override void Deselected()
    {
        base.Deselected();
        towerRange.SetVisualizerActive(false);
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        GameObject towerRangeGO = Instantiate(Resources.Load<GameObject>("Prefabs/AttackTowerRange"), this.gameObject.transform);
        this.towerRange = towerRangeGO.GetComponent<TowerRange>();
        this.damage = towerBlueprint.baseStats["damage"];
        this.attackSpeed = towerBlueprint.baseStats["attackSpeed"];
        this.attackRange = towerBlueprint.baseStats["towerRange"];
        this.timeSinceLastAttack = 1f / this.attackSpeed;
        towerRange.SetRadius(towerBlueprint.baseStats["towerRange"]);
        towerRange.SetVisualizerActive(false);
    }
}