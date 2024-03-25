using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingHitscanAttackingTower : HitscanAttackingTower
{
    private Image reloaderDisplay;
    private int magSize;
    private float reloadTime;
    private float remainingReloadTime;
    private int currentShotsLeft;
    private bool isReloading;

    protected override void Update()
    {
        if (isReloading)
        {
            remainingReloadTime -= Time.deltaTime;
            reloaderDisplay.fillAmount = Mathf.InverseLerp(0, reloadTime, remainingReloadTime);
            if (remainingReloadTime <= 0)
            {
                EndReload();
            }
        }
        base.Update();
    }
        
    public override void Die()
    {
        BattlefieldEventManager.instance.WaveCleared -= EndReload;
        base.Die();
    }

    private void StartReload()
    {
        isReloading = true;
        remainingReloadTime = reloadTime;
    }

    private void EndReload()
    {
        isReloading = false;
        currentShotsLeft = magSize;
        remainingReloadTime = 0;
        reloaderDisplay.fillAmount = Mathf.InverseLerp(0, reloadTime, remainingReloadTime);
    }

    public override bool Attack(GameObject target)
    {
        if (isReloading)
        {
            return false;
        }
        if(!base.Attack(target))
        {
            return false;
        }
        this.currentShotsLeft--;
        if (currentShotsLeft <= 0)
        {
            StartReload();
        }
        return true;

    }

    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        GameObject reloadIndicatorGO = Instantiate(Resources.Load<GameObject>("Prefabs/ReloadIndicator"), this.gameObject.transform);
        reloaderDisplay = reloadIndicatorGO.GetComponentInChildren<Image>();
        BattlefieldEventManager.instance.WaveCleared += EndReload;
        this.magSize = (int) towerBlueprint.baseStats["magSize"];
        this.reloadTime = towerBlueprint.baseStats["reloadTime"];
        this.currentShotsLeft = magSize;
        this.isReloading = false;
        this.remainingReloadTime = 0.0f;
        reloaderDisplay.fillAmount = 0.0f;
        base.Initialize(towerBlueprint, location);
    }
}