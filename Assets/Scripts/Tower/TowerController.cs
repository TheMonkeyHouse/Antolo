using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject attackingTowerPrefab;

    void Awake()
    {
        BattlefieldEventManager.instance.TowerPlaced += PlaceTower;
    }

    void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        GameObject newTowerGO;
        Tower newTower;
        switch (towerBlueprint.towerType)
        {
            case "AttackingTower":
                newTowerGO = Instantiate(attackingTowerPrefab, gameObject.transform);
                newTower = newTowerGO.GetComponent<AttackingTower>();
                break;
            default:
                newTowerGO = Instantiate(towerPrefab, gameObject.transform);
                newTower = newTowerGO.GetComponent<Tower>();
                break;
        }
        newTowerGO.transform.position = new Vector3(location.x, location.y, 0);
        newTower.Initialize(towerBlueprint);
    }
}
