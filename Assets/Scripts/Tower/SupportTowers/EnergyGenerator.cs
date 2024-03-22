using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : Tower
{
    public float energyAmount {get; private set;}
    public float generationSpeed {get; private set;}
    private bool isGenerating;
    private float timeSinceLastGeneration;
    private void Update()
    {
        this.timeSinceLastGeneration += Time.deltaTime;

        // generate energy
        if (!isGenerating)
        {
            return;
        }
        if (this.timeSinceLastGeneration >  1f / this.generationSpeed)
        {
            BattlefieldController.instance.player.RecieveEnergy((int) energyAmount);
            this.timeSinceLastGeneration = 0;
        }
    }
    private void StartNewWave()
    {
        isGenerating = true;
        timeSinceLastGeneration = 0.0f;
    }
    private void WaveFinishedSpawning()
    {
        isGenerating = false;
    }

    private void InitIsGenerating()
    {
        if (BattlefieldController.instance.currentPhase == BattlefieldPhase.WaveSpawningPhase)
        {
            isGenerating = true;
        }
        isGenerating = false;
    }
    public override void Initialize(TowerBlueprint towerBlueprint, Vector3Int location)
    {
        base.Initialize(towerBlueprint, location);
        this.energyAmount = towerBlueprint.baseStats["energyAmount"];
        this.generationSpeed = towerBlueprint.baseStats["generationSpeed"];
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        BattlefieldEventManager.instance.WaveFinishedSpawning += WaveFinishedSpawning;
        InitIsGenerating();
    }
}