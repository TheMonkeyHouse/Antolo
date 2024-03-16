using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackingTower : Tower {
    private float timeSinceLastAttack;
    
    [SerializeField]
    private GameObject towerSprite;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D col){
        if (!col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        enemiesInRange.Add(col.gameObject);
    }
    private void OnTriggerExit2D(Collider2D col){
        if (!col.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        enemiesInRange.Remove(col.gameObject);
    }

    void Update()
    {
        GameObject target = GetTarget();
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
        if (this.timeSinceLastAttack >  1f / this.stats["attackSpeed"])
        {
            this.Attack(target);
            this.timeSinceLastAttack = 0;
        }
    }

    private GameObject GetTarget()
    {
        // targeting function, for now furthest along the track, should be first in list
        if (enemiesInRange.Count == 0)
        {
            return null;
        }
        return enemiesInRange[0];
    }

    private void Attack(GameObject target)
    {
        // animation + other effects
        target.GetComponent<Enemy>().TakeDamage(this.stats["damage"]);
    }
}