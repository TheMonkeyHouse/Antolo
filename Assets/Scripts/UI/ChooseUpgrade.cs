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
    [SerializeField] private LineRenderer lr;

    private void Awake()
    {
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
    }

    public void UpdateDisplay()
    {
        towerNameText.text = towerBlueprint.towerName;
        Vector3 lineTo = BattlefieldController.instance.player.IntToScreenPoint(choiceFor);
        lr.SetPosition(1, new Vector3(lineTo.x,lineTo.y,1));
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