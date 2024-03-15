using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType
{
    Homebase,
    Ground,
    Wall,
    Tower,
}

public class Cell
{
    public Vector3Int position;
    public CellType type;

}
