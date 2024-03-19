using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanAttackingTower : Tower {
    private float timeSinceLastAttack;
    [SerializeField]
    private TowerRange towerRange;
    [SerializeField]
    private GameObject towerSprite;

    public float damage {get; private set;}
    public float attackSpeed {get; private set;}
    public float attackRange {get; private set;}
    public float rotationSpeed {get; private set;}
    
    void Awake()
    {
        this.towerRange = GetComponentInChildren<TowerRange>();
    }
    void Update()
    {
        List<GameObject> targets = towerRange.GetTargets();
        // if no targets, then idle
        
        if (targets.Count == 0)
        {
            return;
        }

        GameObject target = targets[0];
        
        this.timeSinceLastAttack += Time.deltaTime;

        // rotate towards
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x -transform.position.x ) * Mathf.Rad2Deg - 90;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        towerSprite.gameObject.transform.rotation = Quaternion.RotateTowards(towerSprite.gameObject.transform.rotation, targetRotation, rotationSpeed*Time.deltaTime);

        // shoot
        if (this.timeSinceLastAttack >  1f / this.attackSpeed && targetRotation == towerSprite.gameObject.transform.rotation)
        {
            this.Attack(target);
            this.timeSinceLastAttack = 0;
        }
    }

    private void Attack(GameObject target)
    {
        // animation + other effects
        target.GetComponent<Enemy>().TakeDamage(this.damage);
    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.damage = towerBlueprint.baseStats["damage"];
        this.attackSpeed = towerBlueprint.baseStats["attackSpeed"];
        this.attackRange = towerBlueprint.baseStats["attackRange"];
        this.rotationSpeed = towerBlueprint.baseStats["rotationSpeed"];
        this.timeSinceLastAttack = 1f / this.attackSpeed;
        towerRange.SetRadius(towerBlueprint.baseStats["attackRange"]);
    }
}