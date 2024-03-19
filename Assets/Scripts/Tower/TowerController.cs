using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject homebaseTowerPrefab;
    [SerializeField] private GameObject machineGunTowerPrefab;
    [SerializeField] private GameObject sniperTowerPrefab;
    [SerializeField] private GameObject basicWallPrefab;
    [SerializeField] private GameObject spikeTrapPrefab;
    private Dictionary<string, GameObject> prefabDict;
    

    void Awake()
    {
        prefabDict = new Dictionary<string, GameObject>(){
            { "Tower"  , towerPrefab },
            { "Homebase" , homebaseTowerPrefab },
            { "Machine Gun Tower" , machineGunTowerPrefab },
            { "Sniper Tower" , sniperTowerPrefab },
            { "Basic Wall" , basicWallPrefab },
            { "Spike Trap" , spikeTrapPrefab }
        };
        
        BattlefieldEventManager.instance.SetHomebase += SetHomebase;
        BattlefieldEventManager.instance.TowerPlaced += PlaceTower;
        BattlefieldEventManager.instance.WallPlaced += PlaceWall;
    }

    void SetHomebase(Vector3Int location)
    {
        GameObject homebase = Instantiate(homebaseTowerPrefab, gameObject.transform);
        homebase.transform.position = new Vector3(location.x, location.y, 0);
        homebase.GetComponent<Tower>().Initialize(Towers.towerBlueprints["Homebase"], location);
    }

    void PlaceTower(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        GameObject newTowerGO = Instantiate(prefabDict[towerBlueprint.towerName], gameObject.transform);
        newTowerGO.transform.position = new Vector3(location.x, location.y, 0);
        newTowerGO.GetComponent<Tower>().Initialize(towerBlueprint, location);
    }

    void PlaceWall(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        GameObject newWallGO = Instantiate(prefabDict[towerBlueprint.towerName], gameObject.transform);
        newWallGO.transform.position = new Vector3(location.x, location.y, 0);
        newWallGO.GetComponent<Wall>().Initialize(towerBlueprint, location);
    }
}
