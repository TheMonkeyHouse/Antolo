using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// a wave is initialized with a rating
// wave contains a list of possible enemies to spawn, along with their prob
// each enemy has a rating.
// wave will keep spawning enemies until wave rating is reached

// class to represent a weighted list to choose a random enemy to spawn from

public class Wave{
    public Queue<EnemyBlueprint> enemiesToSpawn {get; private set;}
    public float waveRating;

    public Wave(float waveRating, EnemyBlueprint[] possibleChoices, float[] weights)
    {
        this.waveRating = waveRating;
        this.enemiesToSpawn = new Queue<EnemyBlueprint>();

        // ensure that the weights match possible choices
        if (weights.Length != possibleChoices.Length)
        {
            weights = new float[possibleChoices.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = 1.0f;
            }
        }

        // keep inserting random enemies to spawn
        while (waveRating > 0)
        {
            // enemy rating = health * speed? (for now until effects)
            EnemyBlueprint enemyToAdd =  possibleChoices[Util.GetRandomWeightedIndex(weights)];
            waveRating -= enemyToAdd.baseStats["rating"];
            this.enemiesToSpawn.Enqueue(enemyToAdd);
        }
    }

    public IEnumerator WaveSpawner()
    {
        float totalWaveTime = 5*Mathf.Pow(waveRating, 0.5f) + 5; // wave time scales with square root of wave rating
        float enemyTimeDelta = totalWaveTime/enemiesToSpawn.Count;
        while(enemiesToSpawn.Count > 1)
        {
            SpawnEnemy();
            yield return new WaitForSecondsRealtime(Random.Range(0.5f*enemyTimeDelta, enemyTimeDelta)/enemiesToSpawn.Peek().baseStats["speed"]);
        }
        SpawnEnemy();
        BattlefieldEventManager.instance.OnWaveFinishedSpawning();
    }

    public void SpawnEnemy()
    {
        if (this.enemiesToSpawn.Count <= 0)
        {
            return;
        }
        EnemyBlueprint enemyToSpawn = this.enemiesToSpawn.Dequeue();
        BattlefieldEventManager.instance.OnEnemySpawned(enemyToSpawn);
    }
}