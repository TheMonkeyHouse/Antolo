using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackingTower : Tower
{
    public float damage {get; private set;}
    public float attackSpeed {get; private set;}
    public float attackRange {get; private set;}
    [SerializeField] public TowerRange towerRange;
    private float timeSinceLastAttack;

    void Awake()
    {
        this.towerRange = GetComponentInChildren<TowerRange>();
    }

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

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.damage = towerBlueprint.baseStats["damage"];
        this.attackSpeed = towerBlueprint.baseStats["attackSpeed"];
        this.attackRange = towerBlueprint.baseStats["towerRange"];
        this.timeSinceLastAttack = 1f / this.attackSpeed;
        towerRange.SetRadius(towerBlueprint.baseStats["towerRange"]);
    }
}