using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEAttackingTower : Tower {
    private float timeSinceLastAttack;
    [SerializeField] private TowerRange  towerRange;
    public float damage {get; private set;}
    public float attackSpeed {get; private set;}
    public float attackRange {get; private set;}
    
    void Awake()
    {
        this.towerRange = GetComponentInChildren<TowerRange>();
    }
    void Update()
    {
        List<GameObject> targets = towerRange.GetTargets();
        this.timeSinceLastAttack += Time.deltaTime;

        // attack
        if (this.timeSinceLastAttack >  1f / this.attackSpeed)
        {
            this.Attack(targets);
            this.timeSinceLastAttack = 0;
        }
    }

    private void Attack(List<GameObject> targets)
    {
        // animation + other effects
        foreach(GameObject target in targets)
        {
            if (target == null)
            {
                continue;
            }
            target.GetComponent<Enemy>().TakeDamage(this.damage);
        }
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.damage = towerBlueprint.baseStats["damage"];
        this.attackSpeed = towerBlueprint.baseStats["attackSpeed"];
        this.attackRange = towerBlueprint.baseStats["attackRange"];
        this.timeSinceLastAttack = 1f / this.attackSpeed;
        towerRange.SetRadius(towerBlueprint.baseStats["attackRange"]);
    }
}