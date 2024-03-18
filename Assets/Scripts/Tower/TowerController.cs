using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject homebaseTowerPrefab;
    [SerializeField] private GameObject machineGunTowerPrefab;
    [SerializeField] private GameObject sniperTowerPrefab;

    void Awake()
    {
        BattlefieldEventManager.instance.SetHomebase += SetHomebase;
        BattlefieldEventManager.instance.TowerPlaced += PlaceTower;
    }

    void SetHomebase(Vector3Int location)
    {
        GameObject homebase = Instantiate(homebaseTowerPrefab, gameObject.transform);
        homebase.transform.position = new Vector3(location.x, location.y, 0);
        homebase.GetComponent<Tower>().Initialize(Towers.towerBlueprints["Homebase"], location);
    }

    void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        GameObject newTowerGO;
        Tower newTower;
        switch (towerBlueprint.towerName)
        {
            case "Homebase":
                newTowerGO = Instantiate(homebaseTowerPrefab, gameObject.transform);
                newTower = newTowerGO.GetComponent<Tower>();
                break;
            case "Machine Gun Tower":
                newTowerGO = Instantiate(machineGunTowerPrefab, gameObject.transform);
                newTower = newTowerGO.GetComponentInChildren<HitscanAttackingTower>();
                break;
            case "Sniper Tower":
                newTowerGO = Instantiate(sniperTowerPrefab, gameObject.transform);
                newTower = newTowerGO.GetComponentInChildren<HitscanAttackingTower>();
                break;
            default:
                newTowerGO = Instantiate(towerPrefab, gameObject.transform);
                newTower = newTowerGO.GetComponent<Tower>();
                break;
        }
        newTowerGO.transform.position = new Vector3(location.x, location.y, 0);
        newTower.Initialize(towerBlueprint, location);
    }
}
