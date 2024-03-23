using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject homebaseTowerPrefab;
    

    void Awake()
    {
        BattlefieldEventManager.instance.SetHomebase += SetHomebase;
        BattlefieldEventManager.instance.TowerPlaced += PlaceTower;
        BattlefieldEventManager.instance.WallPlaced += PlaceTower;
    }

    void SetHomebase(Vector3Int location)
    {
        GameObject homebase = Instantiate(homebaseTowerPrefab, gameObject.transform);
        homebase.transform.position = new Vector3(location.x, location.y, 0);
        homebase.GetComponent<Tower>().Initialize(Towers.towerBlueprints["Homebase"], location);
    }

    void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        GameObject newTowerGO = Instantiate(towerPrefab, gameObject.transform);
        newTowerGO.transform.position = new Vector3(location.x, location.y, 0);
        System.Type newTowerScriptType = System.Type.GetType (towerBlueprint.towerID + ",Assembly-CSharp");
        BaseTower baseTowerScript = newTowerGO.AddComponent(newTowerScriptType) as BaseTower;
        baseTowerScript.Initialize(towerBlueprint, location);
    }
}
