using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostTower : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private GameObject towerRangeVisualizer;

    public void SetBlueprint(TowerBlueprint towerBlueprint)
    {
        towerRangeVisualizer.SetActive(false);
        SetSprite(Towers.towerSprites[towerBlueprint.towerID]);
        if (towerBlueprint.baseStats.ContainsKey("towerRange"))
        {
            towerRangeVisualizer.SetActive(true);
            towerRangeVisualizer.transform.localScale = new Vector3(2*towerBlueprint.baseStats["towerRange"], 2*towerBlueprint.baseStats["towerRange"], 1);
        }   
    }

    private void SetSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            sr.sprite = Towers.towerSprites["UnknownTower"];
        }
        sr.sprite = sprite;
    }

    public void SetColor(Color color)
    {
        sr.color = color;
    }
}