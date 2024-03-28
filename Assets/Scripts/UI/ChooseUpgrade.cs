using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChooseUpgrade : MonoBehaviour
{
    private int choiceFor;
    private TowerBlueprint towerBlueprint;
    [SerializeField] TMP_Text towerNameText;
    [SerializeField] LineRenderer lr;

    private void Awake()
    {
        lr.startColor = Color.black;
        lr.endColor = Color.black;
        lr.startWidth = 0.01f;
        lr.endWidth = 0.01f;
        lr.positionCount = 2;
        lr.SetPosition(1, new Vector3(10,10,0));
    }

    public void UpdateDisplay()
    {
        towerNameText.text = towerBlueprint.towerName;
        lr.SetPosition(0, new Vector3(-10,-10,0));
    }
    public void SetUpgrade(int choiceFor, TowerBlueprint towerBlueprint)
    {
        this.choiceFor = choiceFor;
        this.towerBlueprint = towerBlueprint;
        UpdateDisplay();
    }
    public void OnClick()
    {
        BattlefieldEventManager.instance.OnUpgradeSelected(choiceFor, towerBlueprint);
    }
}