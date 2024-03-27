using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    public static GridController instance {get; private set;}
    
    private Grid grid;
    [SerializeField] private Tilemap interactiveMap;
    [SerializeField] private Tile hoverTile;
    [SerializeField] private Tile hoverTileGood;
    [SerializeField] private Tile hoverTileBad;
    [SerializeField] private GameObject ghostTower;

    public TowerBlueprint selectedTower {get; private set;}
    public bool deleteTowerSelected {get; private set;}
    private Vector3Int previousMousePos = new Vector3Int();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
        BattlefieldEventManager.instance.TowerBlueprintSelected += SelectTower;
        BattlefieldEventManager.instance.DeleteTowerSelected += DeleteTowerSelected;
        BattlefieldEventManager.instance.Deselect += Deselect;
    }
    private void Start(){
        grid = gameObject.GetComponent<Grid>();
    }
    void Update(){
        
        Vector3Int mousePos = GetMousePosition();
        ghostTower.transform.position = mousePos;

        SetGhostTowerActivity(mousePos);
        
        // Mouse over -> highlight tile
        if (!mousePos.Equals(previousMousePos)) {
            
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            
            // update if on map
            if (BattlefieldController.instance.IsInGrid(mousePos))
            {
                interactiveMap.SetTile(mousePos, GetTile(mousePos));
                previousMousePos = mousePos;
                if (!(selectedTower == null))
                {
                    ghostTower.GetComponent<GhostTower>().SetColor(GetGhostTowerColor(mousePos));
                }
            }
        }

        // left mouse click -> get tile info / place tower
        if (Input.GetMouseButton(0)) {
            
            if (!(selectedTower == null))
            {
                if (isPlaceable(selectedTower, mousePos))
                {
                    if (selectedTower.towerType == "Wall")
                    {
                        BattlefieldEventManager.instance.OnWallPlaced(selectedTower, mousePos);
                    }
                    else
                    {
                        BattlefieldEventManager.instance.OnTowerPlaced(selectedTower, mousePos);
                    }
                    
                }
            }
            
            // deselect
            BattlefieldEventManager.instance.OnDeselect();
            
            // update highlight
            if (BattlefieldController.instance.IsInGrid(mousePos))
            {
                interactiveMap.SetTile(mousePos, GetTile(mousePos));
            }
            
            // display info about tile
        }
    }
    private void SelectTower(TowerBlueprint towerBlueprint)
    {
        ghostTower.GetComponent<GhostTower>().SetBlueprint(towerBlueprint);
        selectedTower = towerBlueprint;
    }
    private void DeleteTowerSelected()
    {
        deleteTowerSelected = true;
    }
    private void Deselect()
    {
        selectedTower = null;
        deleteTowerSelected = false;
    }

    private void SetGhostTowerActivity(Vector3Int mousePos)
    {
        if (selectedTower == null)
        {
            ghostTower.SetActive(false);
            return;
        }
        if (!BattlefieldController.instance.IsInGrid(mousePos))
        {
            ghostTower.SetActive(false);
            return;
        }
        ghostTower.SetActive(true);
    }

    Vector3Int GetMousePosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    Tile GetTile(Vector3Int mousePos)
    {
        if (!(selectedTower == null))
        {
            if (isPlaceable(selectedTower, mousePos))
            {
                return hoverTileGood;
            }
            return hoverTileBad;
        }
        return hoverTile;
    }

    Color GetGhostTowerColor(Vector3Int mousePos)
    {
        if (!isPlaceable(selectedTower, mousePos))
        {
            return Color.red;
        }
        return Color.green;
    }

    bool isPlaceable(TowerBlueprint towerBlueprint, Vector3Int pos)
    {
        return towerBlueprint.isPlaceable(pos);
    }
}
