using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public string description;
    public string towerType;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private HealthBar healthBar;
    public float health {get; private set;}
    public float damage {get; private set;}
    public float attackSpeed {get; private set;}
    public float attackRange {get; private set;}
    

    private void Awake()
    {
    }
    private void Update()
    {

    }

    public void TakeDamage(float dmg)
    {
        StartCoroutine(DamageAnimator());
        health = health - dmg;
        if (health <= 0)
        {
            Die();
            return;
        }
        this.healthBar.UpdateCurrentHealth(health);
    }

    private IEnumerator DamageAnimator()
    {
        sr.color = new Color(1, 0, 0 ,0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        sr.color = new Color(1, 1, 1 ,1);
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }

    public virtual void Initialize(TowerBlueprint towerBlueprint)
    {
        this.towerName = towerBlueprint.towerName;
        this.description = towerBlueprint.description;
        this.towerType = towerBlueprint.towerType;
        this.health = towerBlueprint.baseStats["health"];
        this.damage = towerBlueprint.baseStats["damage"];
        this.attackSpeed = towerBlueprint.baseStats["attackSpeed"];
        this.attackRange = towerBlueprint.baseStats["attackRange"];
        healthBar.InitHealthBar(this.health);
    }
}
