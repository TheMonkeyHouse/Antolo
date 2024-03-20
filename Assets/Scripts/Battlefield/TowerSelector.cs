using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerSelector : MonoBehaviour
{
    private TowerBlueprint towerBlueprint;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private TMP_Text priceText;
    private bool isSelected;

    void Awake()
    {
        isSelected = false;
        BattlefieldEventManager.instance.Deselect += Deselect;
    }

    public void Deselect()
    {
        isSelected = false;
    }

    public void OnClick()
    {
        if (!isSelected)
        {
            BattlefieldEventManager.instance.OnTowerBlueprintSelected(towerBlueprint);
        }
        else
        {
            BattlefieldEventManager.instance.OnDeselect();
        }
        isSelected = !isSelected;
    }

    public void UpdateButton(TowerBlueprint towerBlueprint)
    {
        this.towerBlueprint = towerBlueprint;
        this.buttonText.text = towerBlueprint.towerName;
        this.priceText.text = ((int) towerBlueprint.baseStats["energyCost"]).ToString();
    }
}
