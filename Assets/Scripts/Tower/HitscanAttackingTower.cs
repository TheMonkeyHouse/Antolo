using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitscanAttackingTower : Tower {
    private float timeSinceLastAttack;
    [SerializeField]
    private TowerRange towerRange;
    [SerializeField]
    private GameObject towerSprite;
    
    void Awake()
    {
        this.towerRange = GetComponentInChildren<TowerRange>();
    }
    void Update()
    {
        GameObject target = towerRange.GetTarget();
        this.timeSinceLastAttack += Time.deltaTime;
        
        // if no target, then idle
        if (target == null)
        {
            return;
        }
        
        // rotate sprite
        Vector3 dir = gameObject.transform.position - target.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
        towerSprite.gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // shoot
        if (this.timeSinceLastAttack >  1f / this.attackSpeed)
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

    public override void Initialize(TowerBlueprint towerBlueprint)
    {
        base.Initialize(towerBlueprint);
        towerRange.SetRadius(towerBlueprint.baseStats["attackRange"]);
    }
}