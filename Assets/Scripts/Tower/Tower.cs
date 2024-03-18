using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName {get; private set;}
    public string description {get; private set;}
    public string towerType {get; private set;}
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private HealthBar healthBar;
    public float health {get; private set;}
    public Vector3Int location  {get; private set;}
    

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
        BattlefieldEventManager.instance.OnTowerDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    public virtual void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        this.towerName = towerBlueprint.towerName;
        this.description = towerBlueprint.description;
        this.towerType = towerBlueprint.towerType;
        this.health = towerBlueprint.baseStats["health"];
        this.location = location;
        healthBar.InitHealthBar(this.health);
    }
}
