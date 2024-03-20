using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeleteTowerButton : MonoBehaviour
{
    public bool isSelected {get; private set;}

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
            isSelected = true;
            BattlefieldEventManager.instance.OnDeleteTowerSelected();
        }
        else
        {
            BattlefieldEventManager.instance.OnDeselect();
        }
        isSelected = !isSelected;
    }

}