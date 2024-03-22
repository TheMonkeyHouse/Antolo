using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour, IPointerDownHandler
{
    public string enemyName {get; private set;}
    private HealthBar healthBar;
    [SerializeField] private GameObject floatingTextPrefab;
    private SpriteRenderer sr;
    private List<GameObject> currentTargets;
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
        currentTargets = new List<GameObject>();
        sr = GetComponentInChildren<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBar>();
        BattlefieldEventManager.instance.TowerDestroyed += TowerDestroyed;
        BattlefieldEventManager.instance.WallDestroyed += TowerDestroyed;
        GetPath();
    }

    private void Update()
    {
        GameObject target = GetTarget();
        // if not attacking, move towards center
        if (target == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathing[0], speed*Time.deltaTime);
            return;
        }

        this.timeSinceLastAttack += Time.deltaTime;

        // attack
        if (this.timeSinceLastAttack >  1f / attackSpeed)
        {
            this.Attack(target);
            this.timeSinceLastAttack = 0;
        }
        
    }

    private void GetPath()
    {
        // choose target by probability of inverse distance to center
        // for now just attack base
        pathing = new List<Vector3>();
        pathing.Add(new Vector3(BattlefieldController.instance.width/2, BattlefieldController.instance.height/2, 0));
    }

    // check for tower attack
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Homebase"))
        {
            currentTargets.Add(col.gameObject);
        }
        if (col.gameObject.CompareTag("Tower"))
        {
            currentTargets.Add(col.gameObject);
        }
        if (col.gameObject.CompareTag("Wall"))
        {
            currentTargets.Add(col.gameObject);
        }
    }

    private GameObject GetTarget()
    {
        if (currentTargets.Count == 0)
        {
            return null;
        }
        // target wall > tower > base
        foreach (GameObject target in currentTargets)
        {
            if (target.CompareTag("Wall"))
            {
                return target;
            }
        }
        foreach (GameObject target in currentTargets)
        {
            if (target.CompareTag("Tower"))
            {
                return target;
            }
        }
        return currentTargets[0];
    }

    private void Attack(GameObject target)
    {
        // animation + other effects
        target.GetComponent<BaseTower>().TakeDamage(damage);
    }

    public void TakeDamage(float dmg)
    {
        StartCoroutine(DamageAnimator());
        health = health - dmg;
        GameObject floatingText = Instantiate(floatingTextPrefab, this.gameObject.transform);
        floatingText.GetComponent<FloatingText>().Initialize(0.5f, dmg.ToString(), Color.red);
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
        BattlefieldEventManager.instance.OnEnemyDestroyed(this.gameObject);
        Destroy(this.gameObject);
    }

    private void TowerDestroyed(GameObject tower)
    {
        currentTargets.Remove(tower);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + enemyName);
    }

    public void Initialize(EnemyBlueprint enemyBlueprint)
    {
        this.enemyName = enemyBlueprint.enemyName;
        this.health = enemyBlueprint.baseStats["health"];
        this.speed = enemyBlueprint.baseStats["speed"];
        this.size = enemyBlueprint.baseStats["size"];
        this.damage = enemyBlueprint.baseStats["damage"];
        this.attackSpeed = enemyBlueprint.baseStats["attackSpeed"];
        healthBar.InitHealthBar(health);
        GetPath();
    }
}
