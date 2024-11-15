using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseTower : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerDownHandler
{
    public string towerName {get; private set;}
    public string description {get; private set;}
    public string towerType {get; private set;}
    [SerializeField] public SpriteRenderer sr;
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private HealthBar healthbar;
    [SerializeField] private GameObject healthbarRenderer;
    public float maxHP {get; private set;}
    public float health {get; private set;}
    public Vector3Int location  {get; private set;}

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
        this.healthbar.UpdateCurrentHealth(health);
    }

    private IEnumerator DamageAnimator()
    {
        sr.color = new Color(1, 0, 0 ,0.5f);
        yield return new WaitForSecondsRealtime(0.1f);
        sr.color = new Color(1, 1, 1 ,1);
    }

    public virtual void Die()
    {
        BattlefieldEventManager.instance.WaveCleared -= HealFull;
        Destroy(this.gameObject);
    }

    public void Heal(float heal)
    {
        float newHealth = Mathf.Min(this.maxHP, this.health + heal);
        float healAmount = newHealth - health;
        if(healAmount == 0)
        {
            return;
        }
        CreateFloatingText(0.5f, healAmount.ToString(), Color.green);
        this.health = newHealth;
        this.healthbar.UpdateCurrentHealth(health);
    }

    public void CreateFloatingText(float lifetime, string text, Color color)
    {
        GameObject floatingText = Instantiate(floatingTextPrefab, this.gameObject.transform);
        floatingText.GetComponent<FloatingText>().Initialize(lifetime, text, color);
    }

    public void HealFull()
    {
        Heal(this.maxHP - this.health);
    }

    public void SetHealthBarActive(bool b)
    {
        healthbarRenderer.SetActive(b);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        if (GridController.instance.deleteTowerSelected)
        {
            Die();
            return;
        }

        if (GridController.instance.selectedTower != null)
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(gameObject, eventData);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Selected();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Deselected();
    }

    public virtual void Selected()
    {
        sr.color = Color.blue;
        Debug.Log("Selected: " + towerName);
    }

    public virtual void Deselected()
    {
        sr.color = Color.white;
        Debug.Log("Deselected" + towerName);
    }

    public virtual void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        BattlefieldEventManager.instance.WaveCleared += HealFull;
        this.towerName = towerBlueprint.towerName;
        this.description = towerBlueprint.description;
        this.towerType = towerBlueprint.towerType;
        this.health = towerBlueprint.baseStats["health"];
        this.maxHP = health;
        this.location = location;
        sr = GetComponentInChildren<SpriteRenderer>();
        healthbar = GetComponentInChildren<HealthBar>();
        healthbarRenderer = transform.Find("HealthbarRenderer").gameObject;
        floatingTextPrefab = Resources.Load<GameObject>("Prefabs/FloatingText");
        Sprite sprite = Towers.towerSprites[towerBlueprint.towerID];
        if (!(sprite == null))
        {
            sr.sprite = Towers.towerSprites[towerBlueprint.towerID];
        } 
        healthbar.InitHealthBar(this.health);
    }

    
}
