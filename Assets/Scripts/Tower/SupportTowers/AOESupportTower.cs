using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AOESupportTower : Tower {
    private float timeSinceLastSupport;
    [SerializeField] private TowerRange towerRange;
    public float supportSpeed {get; private set;}
    public float towerRangeRadius {get; private set;}
    
    void Awake()
    {
        this.towerRange = GetComponentInChildren<TowerRange>();
    }
    void Update()
    {
        List<GameObject> targets = towerRange.GetTargets();
        this.timeSinceLastSupport += Time.deltaTime;

        // attack
        if (this.timeSinceLastSupport >  1f / this.supportSpeed)
        {
                    // animation + other effects
            foreach(GameObject target in targets)
            {
                if (target == null)
                {
                    continue;
                }
                Support(target);
            }
            this.timeSinceLastSupport = 0;
        }
    }

    public abstract void Support(GameObject target);
    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        GameObject towerRangeGO = Instantiate(Resources.Load<GameObject>("Prefabs/SupportTowerRange"), this.gameObject.transform);
        this.towerRange = towerRangeGO.GetComponent<TowerRange>();
        this.supportSpeed = towerBlueprint.baseStats["supportSpeed"];
        this.towerRangeRadius = towerBlueprint.baseStats["towerRange"];
        this.timeSinceLastSupport = 0;
        towerRange.SetRadius(towerRangeRadius);
    }
}