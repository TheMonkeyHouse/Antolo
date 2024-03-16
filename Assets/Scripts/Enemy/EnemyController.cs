using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject enemyPrefab;
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
        newEnemy.GetComponent<Enemy>().Initialize(5, 2.0f, 0.5f);
    }
}
