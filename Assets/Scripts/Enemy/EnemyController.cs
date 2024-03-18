using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public List<GameObject> enemiesOnScreen;
    
    private void Awake()
    {
        BattlefieldEventManager.instance.EnemyDestroyed += EnemyDestroyed;
        BattlefieldEventManager.instance.WaveFinishedSpawning += WaveFinishedSpawning;
        BattlefieldEventManager.instance.EnemySpawned += SpawnEnemy;
    }

    private Vector3 GetRandomSpawnPoint()
    {
        // get random spawnpoint outside grid

        // get random height
        float randomHeight = Random.Range(0.0f,1.0f)*BattlefieldController.instance.height;
        // get random width
        float randomWidth = Random.Range(0.0f,1.0f)*BattlefieldController.instance.width;

        // choose random axis
        float axisChoice = Random.Range(0.0f,1.0f);

        if (axisChoice <= 0.25)
        {
            return new Vector3(-1,randomHeight,0);
        }
        if (axisChoice <= 0.5)
        {
            return new Vector3(BattlefieldController.instance.width + 1,randomHeight,0);
        }
        if (axisChoice <= 0.75)
        {
            return new Vector3(randomWidth,-1,0);
        }
        else
        {
            return new Vector3(randomWidth, BattlefieldController.instance.height + 1,0);
        }
        
    }

    private void EnemyDestroyed(GameObject enemy)
    {
        this.enemiesOnScreen.Remove(enemy);
    }

    public void SpawnEnemy(EnemyBlueprint enemyBlueprint)
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.transform.Translate(GetRandomSpawnPoint());
        newEnemy.GetComponent<Enemy>().Initialize(enemyBlueprint);
        enemiesOnScreen.Add(newEnemy);
    }

    private void WaveFinishedSpawning()
    {
        StartCoroutine(CheckWaveFinised());
    }

    private IEnumerator CheckWaveFinised()
    {
        while(enemiesOnScreen.Count > 0)
        {
            yield return null;
        }
        BattlefieldEventManager.instance.OnWaveCleared();
    }
}
