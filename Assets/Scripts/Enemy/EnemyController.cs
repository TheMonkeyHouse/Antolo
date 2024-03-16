using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject enemyPrefab;
    public List<GameObject> enemiesOnScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyPrefab, transform);
        newEnemy.GetComponent<Enemy>().Initialize(Enemies.enemyBlueprints["BasicEnemy"]);
        enemiesOnScreen.Add(newEnemy);
    }
}
