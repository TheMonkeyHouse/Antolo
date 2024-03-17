using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private HealthBar healthBar;
    private SpriteRenderer sr;
    bool isAttacking;
    private GameObject currentTarget;
    private float timeSinceLastAttack;

    // stats
    public float health {get; private set;}
    public float speed {get; private set;}
    public float damage {get; private set;}
    public float size {get; private set;}
    public float attackSpeed {get; private set;}


    // targeting
    // ability
    // bool isFlying
    // sprite

    private List<Vector3> pathing;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        isAttacking = false;
        GetPath();
    }

    private void Update()
    {
        // if not attacking, move towards center
        if (!isAttacking)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathing[0], speed*Time.deltaTime);
            return;
        }

        // if no target, then stop attacking
        if (currentTarget == null)
        {
            isAttacking = false;
            return;
        }

        this.timeSinceLastAttack += Time.deltaTime;

        // attack
        if (this.timeSinceLastAttack >  1f / attackSpeed)
        {
            this.Attack(currentTarget);
            this.timeSinceLastAttack = 0;
        }
        
    }

    private void GetPath()
    {
        // choose target by probability of inverse distance to center
        // for now just attack base
        pathing = new List<Vector3>();
        pathing.Add(new Vector3(BattlefieldController.instance.width/2f, BattlefieldController.instance.height/2f, 0));
    }

    // check for tower attack
    private void OnTriggerEnter2D(Collider2D col){
        if (!col.gameObject.CompareTag("Tower"))
        {
            return;
        }
        currentTarget = col.gameObject;
        isAttacking = true;
    }

    private void Attack(GameObject target)
    {
        // animation + other effects
        target.GetComponent<Tower>().TakeDamage(damage);
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

    public void Initialize(EnemyBlueprint enemyBlueprint)
    {
        this.health = enemyBlueprint.baseStats["health"];
        this.speed = enemyBlueprint.baseStats["speed"];
        this.size = enemyBlueprint.baseStats["size"];
        this.damage = enemyBlueprint.baseStats["damage"];
        this.attackSpeed = enemyBlueprint.baseStats["attackSpeed"];
        healthBar.InitHealthBar(health);
        GetPath();
    }
}
