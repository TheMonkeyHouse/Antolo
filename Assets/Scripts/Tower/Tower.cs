using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public string description;
    public string towerType;
    public Dictionary<string, float> stats {get; private set;}

    private void Update()
    {

    }

    private void GetPath()
    {

    }

    public void Initialize(TowerBlueprint towerBlueprint)
    {
        this.towerName = towerBlueprint.towerName;
        this.description = towerBlueprint.description;
        this.towerType = towerBlueprint.towerType;
        this.stats = towerBlueprint.baseStats;
        GetComponent<CircleCollider2D>().radius = stats["attackRange"];
    }
}
