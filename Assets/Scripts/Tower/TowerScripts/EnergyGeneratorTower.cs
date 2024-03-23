using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGeneratorTower : Tower
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
            GenerateEnergy((int) energyAmount);
            this.timeSinceLastGeneration = 0;
        }
    }
    public override void Die()
    {
        BattlefieldEventManager.instance.StartNewWave -= StartNewWave;
        BattlefieldEventManager.instance.WaveFinishedSpawning -= WaveFinishedSpawning;
        base.Die();
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

    private void GenerateEnergy(int energy)
    {
        CreateFloatingText(0.5f, energy.ToString(), Color.yellow);
        BattlefieldController.instance.player.RecieveEnergy(energy);
    }

    private void InitIsGenerating()
    {
        if (BattlefieldController.instance.currentPhase == BattlefieldPhase.WaveSpawningPhase)
        {
            isGenerating = true;
            return;
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