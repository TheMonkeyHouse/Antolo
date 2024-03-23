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
    private float timeSinceLastAttack;

    void Update()
    {
        this.timeSinceLastAttack += Time.deltaTime;
        TryAttack(GetTargets());
    }

    public virtual void TryAttack(List<GameObject> targets)
    {
        // if within attack speed, skip
        if (this.timeSinceLastAttack <  1f / this.attackSpeed)
        {
            return;
        }
        
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

    public virtual void Attack(GameObject target)
    {
        target.GetComponent<Enemy>().TakeDamage(this.damage);
        this.timeSinceLastAttack = 0;
    }
    
    public abstract List<GameObject> GetTargets();
    public virtual bool CanAttackTarget(GameObject target)
    {
        return true;
    }

    public override void Select()
    {
        base.Select();
        towerRange.SetVisualizerActive();
    }

    public override void Deselect()
    {
        base.Deselect();
        towerRange.SetVisualizerDeactive();
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
    }
}