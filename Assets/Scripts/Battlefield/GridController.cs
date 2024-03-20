using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class GridController : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tile hoverTile = null;
    [SerializeField] private Tile hoverTileGood = null;
    [SerializeField] private Tile hoverTileBad = null;



    private Vector3Int previousMousePos = new Vector3Int();

    // Start is called before the first frame update
    void Start(){
        grid = gameObject.GetComponent<Grid>();
    }

    // Update is called once per frame
    void Update(){
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos)) {
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            // update if on map
            if (BattlefieldController.instance.IsInGrid(mousePos))
            {
                interactiveMap.SetTile(mousePos, GetTile(mousePos));
                previousMousePos = mousePos;
            }
        }

        // left mouse click -> get tile info / place tower
        if (Input.GetMouseButton(0)) {
            
            TowerBlueprint selectedTower = BattlefieldController.instance.player.selectedTower;
            
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

    Vector3Int GetMousePosition(){
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }

    Tile GetTile(Vector3Int mousePos)
    {
        if (!(BattlefieldController.instance.player.selectedTower == null))
        {
            if (isPlaceable(BattlefieldController.instance.player.selectedTower, mousePos))
            {
                return hoverTileGood;
            }
            return hoverTileBad;
        }
        return hoverTile;
    }

    bool isPlaceable(TowerBlueprint towerBlueprint, Vector3Int pos)
    {
        if (!BattlefieldController.instance.IsInGrid(pos))
        {
            return false;
        }
        if (BattlefieldController.instance.state[pos.x,pos.y].type == CellType.Tower)
        {
            return false;
        }
        if (BattlefieldController.instance.state[pos.x,pos.y].type == CellType.Wall && towerBlueprint.towerType == "Wall")
        {
            return false;
        }
        if (BattlefieldController.instance.player.energy < towerBlueprint.baseStats["energyCost"])
        {
            return false;
        }
        return true;
    }
}
