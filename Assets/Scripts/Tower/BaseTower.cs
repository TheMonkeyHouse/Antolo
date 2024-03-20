using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseTower : MonoBehaviour, IPointerDownHandler
{
    public string towerName {get; private set;}
    public string description {get; private set;}
    [SerializeField] public string towerType {get; private set;}
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private HealthBar healthBar;
    public float maxHP {get; private set;}
    public float health {get; private set;}
    public Vector3Int location  {get; private set;}

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

    public virtual void Die()
    {
    }

    public void Heal(float heal)
    {
        this.health = Mathf.Min(this.maxHP, this.health + heal);
        this.healthBar.UpdateCurrentHealth(health);
    }

    public void HealFull()
    {
        this.health = this.maxHP;
        this.healthBar.UpdateCurrentHealth(health);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + towerName);
        if (BattlefieldController.instance.player.deleteTowerSelected)
        {
            Die();
            return;
        }
    }

    public virtual void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        BattlefieldEventManager.instance.WaveCleared += HealFull;
        this.towerName = towerBlueprint.towerName;
        this.description = towerBlueprint.description;
        this.towerType = towerBlueprint.towerType;
        this.health = towerBlueprint.baseStats["health"];
        this.maxHP = towerBlueprint.baseStats["health"];
        this.location = location;
        healthBar.InitHealthBar(this.health);
    }

    
}
