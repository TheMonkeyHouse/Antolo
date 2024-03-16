using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float speed;
    public float size;
    private HealthBar healthBar;
    private SpriteRenderer sr;

    // targeting
    // ability
    // bool isFlying
    // sprite

    private List<Vector3> pathing;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        GetPath();
    }

    private void Update()
    {
        // move towards center
        transform.position = Vector3.MoveTowards(transform.position, pathing[0], speed*Time.deltaTime);
    }

    private void GetPath()
    {
        // choose target by probability of inverse distance to center
        // for now just attack base
        pathing = new List<Vector3>();
        pathing.Add(new Vector3(BattlefieldController.instance.width/2f, BattlefieldController.instance.height/2f, 0));
    }

    public void TakeDamage(float dmg)
    {
        StartCoroutine(DamageAnimator());
        this.health = this.health - dmg;
        if (this.health <= 0)
        {
            Die();
            return;
        }
        this.healthBar.UpdateCurrentHealth(this.health);
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
        healthBar.InitHealthBar(health);
        GetPath();
    }
}
